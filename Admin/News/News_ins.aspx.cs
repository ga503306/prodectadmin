﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_News_News_ins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("../../login.aspx");
            }
            else
            {

            }
        }
    }
    //#region Data

    //#endregion

    #region Button
    protected void save_Check(object sender, EventArgs e)
    {
        if (Title_.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('*號欄位不可為空值')</script>", false);
        }
        else
        {
            save_Click();
        }
    }

    protected void save_Click()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        DataTable dt = new DataTable();
        DataTable serial = new DataTable();
        try
        {
          
            string CmdString = @"";
            CmdString = @"insert into News (Newsno,Title,Info,Context,Inday,Img,Priority) 
                              values (@Newsno,@Title,@Info,@Context,@Inday,@Img,@Priority)";

            serial = DB_fountion.GetNo("Newsno", "News");

            Update_img(DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());//新增圖片

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Newsno", DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());
            cmd.Parameters.AddWithValue("Title", Title_.Value);
            cmd.Parameters.AddWithValue("Info", Info.Value);
            cmd.Parameters.AddWithValue("Context", Context_.Value);
            cmd.Parameters.AddWithValue("Inday", Inday_.Value);
            cmd.Parameters.AddWithValue("Img", img_temp.Value);
            cmd.Parameters.AddWithValue("Priority", Priority.SelectedValue);

            cmd.ExecuteNonQuery();
          
        }
        catch (Exception ex)
        {
            DB_string.log("News_ins:", ex.ToString());
        }
        finally
        {
            Conn.Close();
            //Response.Redirect("News.aspx?type=news");
            Response.Redirect("News_edit.aspx?type=news&id=" + DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());
            //ScriptManager.RegisterStartupScript(Page, GetType(), "success", @"<script> swal({title: '新增成功',text: '跳向編輯頁',},function() {document.location.href = 'News_edit.aspx?type=news&id="+ DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString()+"';});</script>", false);
        }
    }

    #endregion

    #region Other

    protected void Update_img(string Newsno)
    {
        if (FileUploadimg.HasFile)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~") + @"/sqlimages/News/" + Newsno))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + @"/sqlimages/News/" + Newsno);


            FileUpload test = FileUploadimg;
            string ext = System.IO.Path.GetExtension(test.FileName);
            String filename = "Img" + ext;
            String SavePath = "";
            String SavePath_min = "";
            img_temp.Value = filename;//存回資料庫
            SavePath = Server.MapPath("~/sqlimages/News/" + Newsno + "/" + filename);
            SavePath_min = Server.MapPath("~/sqlimages/News/" + Newsno);
            img.ImageUrl = "~/sqimages/News/" + Newsno + "/" + filename;
            DB_fountion.GenerateThumbnailImage(filename, FileUploadimg.FileContent, SavePath_min, "min_", 800, 600);
            FileUploadimg.SaveAs(SavePath);
        }
    }
    #endregion

}