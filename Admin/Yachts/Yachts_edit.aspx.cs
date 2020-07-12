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

public partial class Admin_Yachts_Yachts_edit : System.Web.UI.Page
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
                    Yachts(id.Value);
                }

            }
        }
    }
    #region Datas
    public void Yachts(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from Yachts where Yachtsno=@Yachtsno ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Yachtsno", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            Yachtsno.Value = dt.Rows[0]["Yachtsno"].ToString();
            Modal.Value = dt.Rows[0]["Modal"].ToString();
            Modal_n.Value = dt.Rows[0]["Modal_n"].ToString();
            Overview.Value = dt.Rows[0]["Overview"].ToString();
            Layout.Value = dt.Rows[0]["Layout"].ToString();
            Specification.Value = dt.Rows[0]["Specification"].ToString();
            Isnew.SelectedValue = dt.Rows[0]["Isnew"].ToString();

        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region  Button
    protected void save_Check(object sender, EventArgs e)
    {
        if (Modal.Value == "" || Modal_n.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('*號欄位不可為空值')</script>", false);
        }
        else
        {
            save_Click();
            Response.Redirect("Yachts.aspx?type=yachts");
        }
      
    }

    protected void save_Click()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();

        try
        {
            string CmdString = @"";

            CmdString = @"update Yachts set Modal=@Modal, Modal_n=@Modal_n, Overview=@Overview, Layout=@Layout, Specification=@Specification, Isnew=@Isnew
                          where Yachtsno=@Yachtsno";

            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("Yachtsno", id.Value);
            cmd.Parameters.AddWithValue("Modal", Modal.Value);
            cmd.Parameters.AddWithValue("Modal_n", Modal_n.Value);
            cmd.Parameters.AddWithValue("Overview", Overview.Value);
            cmd.Parameters.AddWithValue("Layout", Layout.Value);
            cmd.Parameters.AddWithValue("Specification", Specification.Value);
            cmd.Parameters.AddWithValue("Isnew", Isnew.SelectedValue);
            cmd.ExecuteNonQuery();

            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Yachts_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
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
            CmdString = @"delete from Yachts where Yachtsno=@id ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            Conn.Open();
            cmd.ExecuteNonQuery();
            del_img(id.Value);
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
            Response.Redirect("Yachts.aspx?type=yachts");
        }
    }

    protected void btn_album_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Album/Album_edit.aspx?type=yachts&id=" + id.Value);
    }
    #endregion

    #region Other
    protected void del_img(string id)
    {
        try
        {
            //刪除
            String DelPath = Server.MapPath("~/sqlimages/Album/" + id);
            Directory.Delete(DelPath, true);
        }
        catch
        {

        }
    }
    #endregion

}