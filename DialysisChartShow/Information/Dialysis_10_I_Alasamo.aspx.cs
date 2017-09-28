using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;
using System.IO;
using Dialysis_Chart_Show.tools;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_10_I_Alasamo : BaseForm
    {
        public string MySqlString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sel_date.Text = Request.QueryString["sel_date"];
            }
        }

        protected void btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_06_07_Alasamo_List.aspx");
        }

        protected void SaveFile_Click(object sender, DirectEventArgs e)
        {
            // 先確認有選擇檔案
            if (UploadDoc.FileBytes.Length == 0)
            {
                Response.Write("沒有上傳的檔案！");
                Response.End();
            }
            using (Stream fs = UploadDoc.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    try
                    {
                        string sSQL = "INSERT INTO Files(Name,Body,Size,SaveDateTime) VALUES (";
                        sSQL += "'" + UploadDoc.PostedFile.FileName + "',";
                        sSQL += bytes + ",";
                        sSQL += UploadDoc.PostedFile.ContentLength.ToString() + ",";
                        sSQL += "'" + DateTime.Now.ToString() + "')";
                        db.Excute(sSQL);
                        Common._NotificationShow("新增成功");
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow(ex.Message.ToString());
                    }
                }
            }
        }

        protected void DownFile_Click(object sender, DirectEventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(txt_2.Text))
            {
                Response.Write("請輸入下載ID編號");
                Response.End();
            }
            else
            {
                id = Convert.ToInt16(txt_2.Text);
            }

            try
            {

                string sSQL = "SELECT * FROM pat_monthdoc WHERE uid=" + id;
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
                    Response.AddHeader("Accept-Language", "cmn-Hans-CN");
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
                            string query = "INSERT INTO pat_monthdoc(Name, Content, Size, SaveDateTime, pat_ic) VALUES (@FileName, @Content, @FileSize, @UploadDateTime, @PAT_IC)";
                            using (MySqlCommand cmd = new MySqlCommand(query))
                            {
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@FileName", DateTime.Now.ToString("yyyyMM_") + filename);
                                cmd.Parameters.AddWithValue("@Content", bytes);
                                cmd.Parameters.AddWithValue("@FileSize", bytes.Length.ToString());
                                cmd.Parameters.AddWithValue("@UploadDateTime", DateTime.Now);
                                cmd.Parameters.AddWithValue("@PAT_IC", _PAT_IC);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                Common._NotificationShow("上传成功");
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("上传失败:" + ex.Message.ToString());
            }

        }

    }
}