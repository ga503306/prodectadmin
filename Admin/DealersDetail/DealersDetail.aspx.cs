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

public partial class Admin_DealersDetail_DealersDetail : System.Web.UI.Page
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

                DataTable dt = DealerD();
                Grid_DealerD.DataSource = dt;
                Grid_DealerD.DataBind();
            }
        }
    }

    #region Data
    public static DataTable DealerD()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from DealersD d 
                           join DealersC c on d.C_no = c.C_no and d.R_no = c.R_no
                           join DealersR r on d.R_no = r.R_no 
                           ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("DealerD:", ex.ToString());
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
        Response.Redirect("DealersDetail_ins.aspx?type=dealers");
    }

    protected void Del_Click(string D_no)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM DealersD where D_no=@D_no ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("D_no", D_no);
            Conn.Open();
            cmd.ExecuteNonQuery();

            DataTable dt2 = DealerD();
            Grid_DealerD.DataSource = dt2;
            Grid_DealerD.DataBind();
            del_img(D_no);
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除成功')</script>", false);
        }
        catch (Exception ex)
        {
            DB_string.log("DealerD_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Gridview
    protected void Grid_DealerD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Session["type"] = "edit";
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_DealerD.Rows[indexid].Cells[1].Text;
            Response.Redirect("DealersDetail_edit.aspx?type=dealers&id=" + id);
        }
        if (e.CommandName == "Del")
        {
            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            string id = Grid_DealerD.Rows[indexid].Cells[1].Text;
            Del_Click(id);
            del_img(id);
        }
    }

    protected void Grid_DealerD_PreRender(object sender, EventArgs e)
    {
        Grid_DealerD.UseAccessibleHeader = true;
        Grid_DealerD.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

    #region Other
    protected void del_img(string D_no)
    {
        try
        {
            //刪除
            String DelPath = Server.MapPath("~/sqlimages/DealerD/" + D_no);
            Directory.Delete(DelPath, true);
        }
        catch
        {

        }
    }
    #endregion
}