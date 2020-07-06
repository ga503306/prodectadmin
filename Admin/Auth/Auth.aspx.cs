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

public partial class Admin_Auth_Auth : System.Web.UI.Page
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

                DataTable dt = Auth();
                ViewState["Data"] = dt;
                Grid_Auth.DataSource = dt;
                Grid_Auth.DataBind();
            }
        }
    }

    #region Data
    public static DataTable Auth()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from Group ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Auth:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_Auth_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Session["type"] = "edit";
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            Session["id"] = Grid_Auth.Rows[indexid].Cells[1].Text;
            Response.Redirect("Auth_edit.aspx?type=basic");
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Auth.Rows[indexid].Cells[1].Text;
            Del_Click(id);
        }
    }

    protected void Grid_Employee_PreRender(object sender, EventArgs e)
    {
        Grid_Auth.UseAccessibleHeader = true;
        Grid_Auth.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}