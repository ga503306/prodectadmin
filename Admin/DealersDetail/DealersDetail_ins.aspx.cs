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


public partial class Admin_DealersDetail_DealersDetail_ins : System.Web.UI.Page
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
                Btn_DealersR.Attributes["onclick"] = "opene('" + R_no.ClientID + "','" + Region.ClientID + "');return false;";


            }
        }
    }

    #region Data
    public void List_country(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @" select * from DealersC where R_no=@R_no";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("R_no", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            ((DropDownList)this.Master.FindControl("ContentPlaceHolder1").FindControl(("C_no"))).Items.Clear();
            while (dr.Read())
            {
                ((DropDownList)this.Master.FindControl("ContentPlaceHolder1").FindControl(("C_no"))).Items.Add(new ListItem(dr[2].ToString(), dr[1].ToString()));
            }

        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_List_country:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Button
    protected void list_Click(object sender, EventArgs e)
    {
        List_country(R_no.Value);
    }

    protected void save_Check(object sender, EventArgs e)
    {
        if (Region.Value == "" || C_no.SelectedValue == "")
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
        SqlTransaction tran = Conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            string selCmdString = @"";
            selCmdString = @"select IDENT_CURRENT('DealersD')+1 ";
            SqlCommand selcmd = new SqlCommand(selCmdString, Conn, tran);
            SqlDataReader dr = selcmd.ExecuteReader();
            while (dr.Read())
                Update_img(dr[0].ToString());//新增圖片 dr[0] 圖片下一個新增的sql identity序號
            dr.Close();
            string CmdString = @"";
            CmdString = @"INSERT INTO DealersD (R_no, C_no, Info,Img)
                                        VALUES (@R_no,@C_no,@Info,@Img); ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("R_no", R_no.Value);
            cmd.Parameters.AddWithValue("C_no", C_no.SelectedValue);
            cmd.Parameters.AddWithValue("Info", Info.Value);
            cmd.Parameters.AddWithValue("Img", img_temp.Value);
            cmd.ExecuteNonQuery();
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("DealersDetail_ins:", ex.ToString());
        }
        finally
        {
            Conn.Close();
            Response.Redirect("DealersDetail.aspx?type=dealers");
        }
    }

    #endregion

    #region Other
    protected void Update_img(string d_no)
    {
        if (FileUploadimg.HasFile)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~") + @"/sqlimages/dealers/" + d_no))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + @"/sqlimages/dealers/" + d_no);

            FileUpload test = FileUploadimg;
            string ext = System.IO.Path.GetExtension(test.FileName);
            String filename = "Img" + ext;
            String SavePath = "";
            String SavePath_min = "";
            img_temp.Value = filename;//存回資料庫
            SavePath = Server.MapPath("~/sqlimages/dealers/" + d_no + "/" + filename);
            SavePath_min = Server.MapPath("~/sqlimages/dealers/" + d_no);
            img.ImageUrl = "~/sqimages/dealers/" + d_no + "/" + filename;
            DB_fountion.GenerateThumbnailImage(filename, FileUploadimg.FileContent, SavePath_min, "min_", 192, 144);
            FileUploadimg.SaveAs(SavePath);
        }
    }

    protected void del_img(string d_no)
    {
        if (!string.IsNullOrEmpty(img_temp.Value))
        {
            try
            {
                //刪除
                String DelPath = Server.MapPath("~/sqlimages/dealers/" + d_no);
                Directory.Delete(DelPath, true);
            }
            catch
            {

            }
        }
    }
    #endregion
}