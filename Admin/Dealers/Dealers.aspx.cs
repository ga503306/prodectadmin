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


public partial class Admin_Dealers_Dealers : System.Web.UI.Page
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

                DataTable dt = Dealers();
                Grid_Dealers.DataSource = dt;
                Grid_Dealers.DataBind();
            }
        }
    }

    #region Data
    public static DataTable Dealers()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from DealersR ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Dealers:", ex.ToString());
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
        Response.Redirect("Dealers_ins.aspx?type=dealers");
    }

    protected void Del_Click(string R_no)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM DealersR where R_no=@R_no ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("R_no", R_no);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = Dealers();
            Grid_Dealers.DataSource = dt2;
            Grid_Dealers.DataBind();
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗','國家尚未全刪除')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_Dealers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Dealers.Rows[indexid].Cells[1].Text;
            Response.Redirect("Dealers_edit.aspx?type=dealers&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Dealers.Rows[indexid].Cells[1].Text;
            Del_Click(id);
            del_img(id);
        }
    }

    protected void Grid_Dealers_PreRender(object sender, EventArgs e)
    {
        Grid_Dealers.UseAccessibleHeader = true;
        Grid_Dealers.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

    #region Other
    protected void del_img(string R_no)
    {
        try
        {
            //刪除
            String DelPath = Server.MapPath("~/sqlimages/Album/" + R_no);
            Directory.Delete(DelPath, true);
        }
        catch
        {

        }
    }
    #endregion
}