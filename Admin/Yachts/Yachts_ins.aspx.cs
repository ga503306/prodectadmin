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

public partial class Admin_Yachts_Yachts_ins : System.Web.UI.Page
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

            CmdString = @"insert into Yachts (Yachtsno,Modal, Modal_n, Overview, Layout, Specification, Isnew) 
                          values (@Yachtsno,@Modal, @Modal_n, @Overview, @Layout, @Specification, @Isnew)";

            DataTable serial = new DataTable();
            serial = DB_fountion.GetNo("Yachtsno", "Yachts");

            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("Yachtsno", DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());
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
            DB_string.log("Yachts_ins:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion
}