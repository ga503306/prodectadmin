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


public partial class Admin_Auth_Auth_edit : System.Web.UI.Page
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
                    DataTable dt = Auth();
                    Grid_Auth.DataSource = dt;
                    Grid_Auth.DataBind();
                    del_btn.Visible = true;
                }
                else
                {
                    del_btn.Visible = false;
                }
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
            CmdString = @"select * from [Parameter] where Table_name = 'Parameter' and Key_string = 'SidebarID' ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Auth_edit:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_Auth_PreRender(object sender, EventArgs e)
    {
        Grid_Auth.UseAccessibleHeader = true;
        Grid_Auth.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void Grid_Auth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = 0;
        index = DB_fountion.tablenametoindex(Grid_Auth, e, "隱藏頁面");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[index].Text = DB_fountion.gridbind("Parameter", "SidebarID", e.Row.Cells[index].Text);
        }
    }
    #endregion


}