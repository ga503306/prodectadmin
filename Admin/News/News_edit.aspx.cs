using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_News_News_edit : System.Web.UI.Page
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
                if ((Request.QueryString["id"] != null) && (Request.QueryString["id"].ToString() != ""))
                {
                    id.Value = Request.QueryString["id"];
                    News(id.Value);
                }


            }
        }
    }
    #region Data
    public void News(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from News where Newsno=@Newsno ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Newsno", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            Newsno.Value = dt.Rows[0]["Newsno"].ToString();
            Title_.Value = dt.Rows[0]["Title"].ToString();
            Info.Value = dt.Rows[0]["Info"].ToString();
            Context_.Value = dt.Rows[0]["Context"].ToString();
            Inday_.Value = Convert.ToDateTime(dt.Rows[0]["Inday"]).ToString("yyyy-MM-dd");
            //圖
            if (!string.IsNullOrEmpty(dt.Rows[0]["Img"].ToString())) 
            {
                Random rand = new Random();
                img.ImageUrl = "~/sqlimages/News/" + dt.Rows[0]["Newsno"].ToString() + "/" + dt.Rows[0]["Img"].ToString() + "?" + rand.Next(1000).ToString();
                img_temp.Value = dt.Rows[0]["Img"].ToString();
            }
        }
        catch (Exception ex)
        {
            DB_string.log("News_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

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
        try
        {
            Update_img(id.Value);//新增圖片

            string CmdString = @"";

                CmdString = @"update News set 
                              Title=@Title,Info=@Info,Context=@Context,Inday=@Inday,Img=@Img
                              where Newsno=@id";


            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            cmd.Parameters.AddWithValue("Title", Title_.Value);
            cmd.Parameters.AddWithValue("Info", Info.Value);
            cmd.Parameters.AddWithValue("Context", Context_.Value);
            cmd.Parameters.AddWithValue("Inday", Inday_.Value);
            cmd.Parameters.AddWithValue("Img", img_temp.Value);
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            DB_string.log("News_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
            Response.Redirect("News.aspx?type=news");
        }
    }
    protected void del_Click(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"delete from News where Newsno=@id ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            Conn.Open();
            cmd.ExecuteNonQuery();
            del_img(id.Value);
            

        }
        catch (Exception ex)
        {
            DB_string.log("News_edit_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
            Response.Redirect("News.aspx?type=news");
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
            img_temp.Value = filename;//存回資料庫
            SavePath = Server.MapPath("~/sqlimages/News/" + Newsno + "/" + filename);
            img.ImageUrl = "~/sqimages/News/" + Newsno + "/" + filename;
            FileUploadimg.SaveAs(SavePath);
        }
    }

    protected void del_img(string Newsno)
    {
        if (!string.IsNullOrEmpty(img_temp.Value))
        {
            try { 
            //刪除
            String DelPath = Server.MapPath("~/sqlimages/News/" + Newsno );
            Directory.Delete(DelPath,true);
            }
            catch
            {

            }
        }
    }
    #endregion
}