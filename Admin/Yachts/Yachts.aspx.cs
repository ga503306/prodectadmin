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


public partial class Admin_Yachts_Yachts : System.Web.UI.Page
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

    #region Button

    protected void ins_Click(object sender, EventArgs e)
    {
        Response.Redirect("Yachts_ins.aspx?type=yachts");
    }

    protected void Del_Click(string Yachtsno)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM Yachts where Yachtsno=@Yachtsno ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Yachtsno", Yachtsno);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = Yachts();
            Grid_Yachts.DataSource = dt2;
            Grid_Yachts.DataBind();
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗','國家尚未全刪除')</script>", false);
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
            Response.Redirect("Yachts_edit.aspx?type=yachts&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_Yachts.Rows[indexid].Cells[1].Text;
            Del_Click(id);
            del_img(id);
        }
    }

    protected void Grid_Yachts_PreRender(object sender, EventArgs e)
    {
        Grid_Yachts.UseAccessibleHeader = true;
        Grid_Yachts.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void Grid_Yachts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = 0;
        index = DB_fountion.tablenametoindex(Grid_Yachts, e, "是否最新");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[index].Text)
            {
                case "1":
                    e.Row.Cells[index].Text = "是";
                    break;
                case "0":
                    e.Row.Cells[index].Text = "否";
                    break;
                case "&nbsp":
                    break;
                default:
                    break;
            }
           
          
        }
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