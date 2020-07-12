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

public partial class Admin_Album_Album : System.Web.UI.Page
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

                DataTable dt = Yachts();
                Grid_Yachts.DataSource = dt;
                Grid_Yachts.DataBind();
            }
        }
    }

    #region Data
    public static DataTable Yachts()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from Yachts ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_Yachts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Yachts.Rows[indexid].Cells[1].Text;
            Response.Redirect("Album_edit.aspx?type=yachts&id=" + id);
        }
    }

    protected void Grid_Yachts_PreRender(object sender, EventArgs e)
    {
        Grid_Yachts.UseAccessibleHeader = true;
        Grid_Yachts.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}