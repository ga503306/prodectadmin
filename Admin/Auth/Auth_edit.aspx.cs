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

                    ViewState["Get_Init"] = Get_Init();//拿已勾選資料
                    Set_Init();

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
            CmdString = @"select * from [Table]  ";

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
    public DataTable Get_Init()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"  select Group_view from GroupDetail where G_no=@G_no ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            cmd.Parameters.AddWithValue("G_no", id.Value);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Auth_edit_init:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Button
    protected void save_Check(object sender, EventArgs e)
    {
        save_Click();
    }

    protected void save_Click()
    {
        List<string> Lstr = new List<string>(); //暫存 勾起來的value
        foreach (GridViewRow gdrw in Grid_Auth.Rows)
        {

            CheckBox Choose = (CheckBox)gdrw.Cells[0].FindControl("Choose");
            if (Choose.Checked == true)
            {
                Lstr.Add(gdrw.Cells[1].Text);
            }
        }

        DB_data.AuthDel(id.Value);//刪明細

        foreach (string str in Lstr)
        {
            DB_data.AuthBtnsave(id.Value, str);
        }

        Response.Redirect("Auth.aspx?type=auth");
    }
    #endregion

    #region Gridview
    protected void Grid_Auth_PreRender(object sender, EventArgs e)
    {

        if (Grid_Auth.Rows.Count > 0)
        {
            Grid_Auth.UseAccessibleHeader = true;
            Grid_Auth.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void Grid_Auth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //int index = 0;
        //index = DB_fountion.tablenametoindex(Grid_Auth, e, "隱藏頁面");

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[index].Text = DB_fountion.gridbind("Parameter", "SidebarID", e.Row.Cells[index].Text);
        //}
    }
    #endregion

    #region Other
    protected void Set_Init()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Get_Init"];
        foreach (DataRow dtrw in dt.Rows)
        {
            foreach (GridViewRow gdrw in Grid_Auth.Rows)
            {
                if (dtrw["Group_view"].ToString() == gdrw.Cells[1].Text)
                {
                    CheckBox Choose = (CheckBox)gdrw.Cells[0].FindControl("Choose");
                    Choose.Checked = true;
                }
            }
        }

    }
    #endregion
}