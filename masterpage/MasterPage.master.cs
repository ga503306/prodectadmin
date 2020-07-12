using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterpage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("../../login.aspx");
        }
        else
        {
            Username.Value = Session["Username"].ToString();
            Sel_sidebar();
        }
    }
    #region Data
    public void Sel_sidebar()
    {
        //宣告SQL的連線
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @" 
	                             select T_no,T_id,T_name,T_icon,seq from [table]
                                 
	                             except
                                 
	                             select T_no,T_id,T_name,T_icon,seq from [table] 
                                 join GroupDetail gd on t_id= Group_view
                                 join [group] g on gd.G_no = g.G_no and gd.Group_name = g.Group_name
	                             where Group_value = @Group_value
                                 order by seq
                                ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Group_value", Session["Auth"]);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            Rpt_sidebar.DataSource = dt;
            Rpt_sidebar.DataBind();

        }
        catch (Exception ex)
        {
            DB_string.log("Sel_sidebar:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }


    #endregion

    #region Repeater
    protected void Rpt_sidebar_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            string t_no = drv["T_no"].ToString();

            Repeater childRepeater = (Repeater)e.Item.FindControl("Rpt_sidebar_detail");
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
            DataTable dt = new DataTable();
            try
            {
                string CmdString = @"  select * from tabledetail where t_no = @t_no order by seq ";
                SqlCommand cmd = new SqlCommand(CmdString, Conn);
                cmd.Parameters.AddWithValue("t_no", t_no);
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(dr);

                childRepeater.DataSource = dt;
                childRepeater.DataBind();

            }
            catch (Exception ex)
            {
                DB_string.log("Sel_sidebar_detail:", ex.ToString());
            }
            finally
            {
                Conn.Close();
            }

        }
    }
    #endregion
}
