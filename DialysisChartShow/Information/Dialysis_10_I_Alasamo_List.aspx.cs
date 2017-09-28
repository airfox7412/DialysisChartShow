using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.Web.Hosting;
using System.IO;
using MySql.Data.MySqlClient;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_10_I_Alasamo_List : BaseForm
    {
        public string MySqlString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }            
        }

        protected void show_grid()
        {
            string sSQL = "SELECT uid, Month, Name, Size, SaveDateTime FROM pat_monthdoc ";
            sSQL += "WHERE uid>0 ";
            sSQL += "ORDER BY uid DESC";
            DataTable dt = db.Query(sSQL);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string uid = e.ExtraParams["uid"].ToString();
            X.Redirect("Dialysis_10_J_Alasamo.aspx?uid=" + uid);
        }

        protected void Btn_Add_Click(object sender, DirectEventArgs e)
        {
            YearMonth.Text = DateTime.Now.ToString("yyyy-MM");
            AddWindow.Show();
        }

        protected void DownFile_Click(object sender, DirectEventArgs e)
        {
            string uid = e.ExtraParams["uid"].ToString();
            try
            {
                string sSQL = "SELECT * FROM pat_monthdoc WHERE uid=" + uid;
                DataTable dt = db.Query(sSQL);

                if (dt.Rows.Count > 0)
                {
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Expires = 0;
                    Response.Buffer = true;

                    // Getxxx效能較佳
                    string Month = dt.Rows[0]["Month"].ToString();
                    string Name = dt.Rows[0]["Name"].ToString();
                    // 進行AddHeader設定
                    Response.AddHeader("Accept-Language", "zh-CN");
                    // 設定輸出檔案及類型
                    Response.AddHeader("content-disposition", "attachment; filename=\"" + Month + "_" + Name + "\"");
                    Response.ContentType = "Application/octet-stream";
                    // 進行「二進位」輸出，使用BinaryWrite方法
                    Response.BinaryWrite((byte[])dt.Rows[0]["Content"]);
                    // 將緩衝輸出
                    Response.Flush();
                    Response.Close();
                }
                Response.End();
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void UploadFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string filename = Path.GetFileName(UploadDoc.PostedFile.FileName);
                string contentType = UploadDoc.PostedFile.ContentType;
                using (Stream fs = UploadDoc.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        using (MySqlConnection con = new MySqlConnection(MySqlString))
                        {
                            string query = "INSERT INTO pat_monthdoc(Month, Name, Content, Size, SaveDateTime, User) ";
                            query += "VALUES (@Month, @FileName, @Content, @FileSize, @SaveDateTime, @UserName)";
                            using (MySqlCommand cmd = new MySqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Month", YearMonth.Text);
                                cmd.Parameters.AddWithValue("@FileName", DateTime.Now.ToString("yyyyMM_") + filename);
                                cmd.Parameters.AddWithValue("@Content", bytes);
                                cmd.Parameters.AddWithValue("@FileSize", bytes.Length.ToString());
                                cmd.Parameters.AddWithValue("@SaveDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                cmd.Parameters.AddWithValue("@UserName", _USER_NAME);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                Common._NotificationShow("上传成功");
                AddWindow.Close();
                show_grid();
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("上传失败:" + ex.Message.ToString());
            }
        }

        protected void DownTmpFile_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string sSQL = "SELECT * FROM pat_monthdoc WHERE uid=1";
                DataTable dt = db.Query(sSQL);

                if (dt.Rows.Count > 0)
                {
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Expires = 0;
                    Response.Buffer = true;

                    // Getxxx效能較佳
                    string Name = dt.Rows[0]["Name"].ToString();
                    // 進行AddHeader設定
                    Response.AddHeader("Accept-Language", "zh-CN");
                    // 設定輸出檔案及類型
                    Response.AddHeader("content-disposition", "attachment; filename=\"" + Name + "\"");
                    Response.ContentType = "Application/octet-stream";
                    // 進行「二進位」輸出，使用BinaryWrite方法
                    Response.BinaryWrite((byte[])dt.Rows[0]["Content"]);
                    // 將緩衝輸出
                    Response.Flush();
                    Response.Close();
                }
                Response.End();
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
        
        protected void UploadWin(object sender, DirectEventArgs e)
        {
            upload_uid.Text = e.ExtraParams["uid"].ToString();
            UYearMonth.Text = e.ExtraParams["month"].ToString();
            UpdateWindow.Show();
        }

        protected void UpdateFile_Click(object sender, DirectEventArgs e)
        {
            string uid = upload_uid.Text;
            try
            {
                string filename = Path.GetFileName(FileUploadField1.PostedFile.FileName);
                string contentType = FileUploadField1.PostedFile.ContentType;
                using (Stream fs = FileUploadField1.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        using (MySqlConnection con = new MySqlConnection(MySqlString))
                        {
                            string query = "UPDATE pat_monthdoc SET Month=@Month, Name=@FileName, Content=@Content, Size=@FileSize, SaveDateTime=@SaveDateTime, User=@UserName ";
                            query += "WHERE uid=" + uid;
                            using (MySqlCommand cmd = new MySqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@Month", UYearMonth.Text);
                                cmd.Parameters.AddWithValue("@FileName", filename);
                                cmd.Parameters.AddWithValue("@Content", bytes);
                                cmd.Parameters.AddWithValue("@FileSize", bytes.Length.ToString());
                                cmd.Parameters.AddWithValue("@SaveDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                cmd.Parameters.AddWithValue("@UserName", _USER_NAME);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                Common._NotificationShow("上传成功");
                UpdateWindow.Close();
                show_grid();
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("上传失败:" + ex.Message.ToString());
            }
        }

        //protected void SaveFile_Click(object sender, DirectEventArgs e)
        //{
        //    // 先確認有選擇檔案
        //    if (UploadDoc.FileBytes.Length == 0)
        //    {
        //        Response.Write("沒有上傳的檔案！");
        //        Response.End();
        //    }
        //    using (Stream fs = UploadDoc.PostedFile.InputStream)
        //    {
        //        using (BinaryReader br = new BinaryReader(fs))
        //        {
        //            byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //            try
        //            {
        //                string sSQL = "INSERT INTO Files(Name,Body,Size,SaveDateTime) VALUES (";
        //                sSQL += "'" + UploadDoc.PostedFile.FileName + "',";
        //                sSQL += bytes + ",";
        //                sSQL += UploadDoc.PostedFile.ContentLength.ToString() + ",";
        //                sSQL += "'" + DateTime.Now.ToString() + "')";
        //                db.Excute(sSQL);
        //                Common._NotificationShow("新增成功");
        //            }
        //            catch (Exception ex)
        //            {
        //                Common._ErrorMsgShow(ex.Message.ToString());
        //            }
        //        }
        //    }
        //}
    }
}