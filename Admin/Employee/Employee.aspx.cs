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

public partial class Admin_Employee_Employee : System.Web.UI.Page
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

                DataTable dt = Employee();
                ViewState["Data"] = dt;
                Grid_Employee.DataSource = dt;
                Grid_Employee.DataBind();
            }
        }
    }

    #region Data
    public static DataTable Employee()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from Employee  
                          left join [Group] on Group_value = Auth";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Employee:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Button

    protected void ins_Click(object sender, EventArgs e)
    {
        Response.Redirect("Employee_ins.aspx?type=basic");
    }

    protected void Del_Click(string Username)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM Employee where Username=@Username ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Username", Username);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = Employee();
            Grid_Employee.DataSource = dt2;
            Grid_Employee.DataBind();

            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("Employee_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_Employee_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Employee.Rows[indexid].Cells[1].Text;
            Response.Redirect("Employee_edit.aspx?type=basic&action=edit&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Employee.Rows[indexid].Cells[1].Text;
            Del_Click(id);
        }
    }

    protected void Grid_Employee_PreRender(object sender, EventArgs e)
    {
        Grid_Employee.UseAccessibleHeader = true;
        Grid_Employee.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void Grid_Employee_RowDataBound(object sender, GridViewRowEventArgs e)
    { //改寫法 沒用到
    }
    #endregion

}