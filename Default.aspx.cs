using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //新聞
            DataTable dt = News_sel();
            Rpt_News.DataSource = dt;
            Rpt_News.DataBind();
            //遊艇大圖+文字
            DataTable dt_w = Shipw_sel();
            Rpt_shipw.DataSource = dt_w;
            Rpt_shipw.DataBind();
            //遊艇小圖
            Rpt_ships.DataSource = dt_w;
            Rpt_ships.DataBind();
        }
    }

    #region Data
    public static DataTable News_sel()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select top 3 * from News order by  Priority desc, Inday desc ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Defalut_News:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }

    public static DataTable Shipw_sel()
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
            DB_string.log("Default_Yachts:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    protected string isnew(string state)
    {
        string result = "";
        switch (state)
        {
            case "1":
                result = "";
                break;
            case "0":
                result = "display:none;";
                break;
            default:
                break;
        }
        return result;
    }
}
