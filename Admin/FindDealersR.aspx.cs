using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class Admin_FindDealersR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("../login.aspx");
        }

        if (!Page.IsPostBack)
        {
            DBInit();
        }
    }

    #region Data
    protected void DBInit()
    {
        //宣告SQL的連線
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string str = "";
            if (ser1.Text != "")
                str += " and R_no like @ser1";
            if (ser2.Text != "")
                str += " and Region like @ser2";

            string CmdString = @"";
            CmdString = @"select R_no, Region from DealersR  where 1=1 " + str;


            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("ser1", "%" + ser1.Text + "%");
            cmd.Parameters.AddWithValue("ser2", "%" + ser2.Text + "%");
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            ViewState["detail"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            DB_string.log("FindDealersR:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    } 
    #endregion


    #region Button
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DBInit();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>window.close();</" + "script>");
    }
    #endregion

    #region GridView
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "select")
        {
            string str = e.CommandArgument.ToString();
            string[] Lstr = str.Split(',');
            string controla = Request["idControla"];
            string controlb = Request["idControlb"];

            ClientScript.RegisterStartupScript(GetType(), "open", "openA('" +
                         controla + "','" + Lstr[0] + "','" +
                         controlb + "','" + Lstr[1] + "');", true);
            //Lstr [0] R_no [1] Region 
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DBInit();
    }
    #endregion
}