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

public partial class Admin_News_News : System.Web.UI.Page
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

                DataTable dt = News();
                Grid_News.DataSource = dt;
                Grid_News.DataBind();
            }
        }
    }

    #region Data
    public static DataTable News()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from News ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("News:", ex.ToString());
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
        Response.Redirect("News_ins.aspx?type=news&action=ins");
    }

    protected void Del_Click(string Newsno)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM News where Newsno=@Newsno ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Newsno", Newsno);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = News();
            Grid_News.DataSource = dt2;
            Grid_News.DataBind();
            del_img(Newsno);
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("News_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_News_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Session["type"] = "edit";
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_News.Rows[indexid].Cells[1].Text;
            Response.Redirect("News_edit.aspx?type=news&action=edit&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_News.Rows[indexid].Cells[1].Text;
            Del_Click(id);
            del_img(id);
        }
    }

    protected void Grid_News_PreRender(object sender, EventArgs e)
    {
        Grid_News.UseAccessibleHeader = true;
        Grid_News.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

    #region Other
    protected void del_img(string Newsno)
    {
        try { 
            //刪除
            String DelPath = Server.MapPath("~/sqlimages/News/" + Newsno);
            Directory.Delete(DelPath,true);
        }
        catch
        {

        }
    } 
    #endregion
}