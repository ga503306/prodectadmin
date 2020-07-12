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
                DB_fountion.GetNo("G_no", "Group");
                DataTable dt = Auth();
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
            CmdString = @"select * from [Group] ";

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

    #region Button

    protected void ins_Click(object sender, EventArgs e)
    {
        //Group_name.Value = "";//清
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clear", "$('#ContentPlaceHolder1_Group_name').val('');", true);//清
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openpup", "$('#modal-ins').modal('show');", true);
        // Response.Redirect("Auth_edit.aspx?type=basic&action=ins");
    }

    protected void Del_Click(string G_no)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM [Group] where G_no=@G_no ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("G_no", G_no);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = Auth();
            Grid_Auth.DataSource = dt2;
            Grid_Auth.DataBind();

            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("Auth_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion
    protected void Enter_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();
        DataTable serial = new DataTable();
        serial = DB_fountion.GetNo("G_no", "Group");
        try
        {
            string InsCmdString = @"";
            InsCmdString = @"INSERT INTO [Group]( G_no, Group_name, Group_value )
                             SELECT  @G_no,@Group_name,isnull(max(Group_value),0)+1 FROM [Group]";

            SqlCommand Inscmd = new SqlCommand(InsCmdString, Conn, tran);
            Inscmd.Parameters.AddWithValue("G_no", DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());
            Inscmd.Parameters.AddWithValue("Group_name", Group_name.Value);
            Inscmd.ExecuteNonQuery();
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Auth_Enter_Click:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-ins').modal('hide');", true);
        //刷新
        dt = Auth();
        Grid_Auth.DataSource = dt;
        Grid_Auth.DataBind();
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-ins').modal('hide');", true);
    }

    #region Gridview
    protected void Grid_Auth_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Session["type"] = "edit";
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Auth.Rows[indexid].Cells[1].Text;
            Response.Redirect("Auth_edit.aspx?type=auth&action=edit&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Auth.Rows[indexid].Cells[1].Text;
            Del_Click(id);
        }
    }

    protected void Grid_Auth_PreRender(object sender, EventArgs e)
    {
        Grid_Auth.UseAccessibleHeader = true;
        Grid_Auth.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}