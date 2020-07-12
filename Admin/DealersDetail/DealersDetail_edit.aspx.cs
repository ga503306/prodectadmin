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


public partial class Admin_DealersDetail_DealersDetail_edit : System.Web.UI.Page
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
                if ((Request.QueryString["id"] != null) && (Request.QueryString["id"].ToString() != ""))
                {
                    id.Value = Request.QueryString["id"];
                    DealearD(id.Value);
                }


            }
        }
    }
    #region Data
    public void DealearD(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @" select * from DealersD d 
                           join DealersC c on d.C_no = c.C_no and d.R_no = c.R_no
                           join DealersR r on d.R_no = r.R_no 
                           where d.D_no=@D_no ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("D_no", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            R_no.Value = dt.Rows[0]["R_no"].ToString();
            Region.Value = dt.Rows[0]["Region"].ToString();
            List_country(R_no.Value);//先產生下拉的值
            C_no.SelectedValue = dt.Rows[0]["C_no"].ToString();
            D_no.Value = dt.Rows[0]["D_no"].ToString();
            Info.Value = dt.Rows[0]["Info"].ToString();
            //圖
            if (!string.IsNullOrEmpty(dt.Rows[0]["Img"].ToString()))
            {
                Random rand = new Random();
                img.ImageUrl = "~/sqlimages/Dealers/" + dt.Rows[0]["D_no"].ToString() + "/min_" + dt.Rows[0]["Img"].ToString() + "?" + rand.Next(1000).ToString();
                img_temp.Value = dt.Rows[0]["Img"].ToString();
            }
        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

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

        DataTable dt = new DataTable();
        try
        {
            Update_img(id.Value);//新增圖片

            string CmdString = @"";

            CmdString = @"update DealersD set 
                              R_no=@R_no,C_no=@C_no,Info=@Info,Img=@Img
                              where D_no=@id";


            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            cmd.Parameters.AddWithValue("R_no", R_no.Value);
            cmd.Parameters.AddWithValue("C_no", C_no.SelectedValue);
            cmd.Parameters.AddWithValue("Info", Info.Value);
            cmd.Parameters.AddWithValue("Img", img_temp.Value);
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            DB_string.log("DealersDetail_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
            Response.Redirect("DealersDetail.aspx?type=dealers");
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
            CmdString = @"delete from DealersD where d_no=@id ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            Conn.Open();
            cmd.ExecuteNonQuery();
            del_img(id.Value);


        }
        catch (Exception ex)
        {
            DB_string.log("DealersDetail_edit_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
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