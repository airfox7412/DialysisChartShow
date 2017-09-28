using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Generic;

public partial class upload : System.Web.UI.Page
{
    /// <summary>
    /// 上傳圖片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string UploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"]; 
            string UploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];   
            string Path = Server.MapPath(UploadFilePath);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            // Get the data
            HttpPostedFile uploadFile = Request.Files["Filedata"];
            string fileName = uploadFile.FileName;

            if (fileName.LastIndexOf(".") > -1)
            {
                string ext = fileName.Substring(fileName.LastIndexOf("."));
                string fileId = fileName;
                uploadFile.SaveAs(Path + fileId);

                string[] A = fileId.Split(new Char[] { '_', '.' });
                Dialysis_Chart_Show.tools.DBMysql db = new Dialysis_Chart_Show.tools.DBMysql();
                string sql = "DELETE FROM zinfo_g_02 WHERE filename='" + fileId + "' ";
                db.Excute(sql);
                sql = "INSERT INTO zinfo_g_02 (pat_ic,no,filename) VALUES('" + A[0] + "','" + A[1] + "','" + fileId + "')";
                db.Excute(sql);

                Response.StatusCode = 200;
                Response.Write(fileId);
            }
            else
            {
                Response.StatusCode = 500;
                Response.Write("An error occured");
                Response.End();
            }
        }
        catch
        {
            // If any kind of error occurs return a 500 Internal Server error
            Response.StatusCode = 500;
            Response.Write("An error occured");
            Response.End();
        }
        finally
        {
            // Clean up
            
            Response.End();
        }
	
	}
}
