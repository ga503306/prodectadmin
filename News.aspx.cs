using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News : System.Web.UI.Page
{
    private const int PageSize = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = News_sel();
            Rpt_News.DataSource = dt;
            Rpt_News.DataBind();
        }
    }
    #region Data
    public DataTable News_sel()
    {
        //分頁用
        int currentPage = Request["page"] == null ? 1 : int.Parse(Request["page"]);
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"with tstation as (
                              select row_number() over(order by inday desc) as rownumber,
                              * 
                              from News 
                              )select *from tstation where rownumber>=@start  and rownumber <=@end ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.Add("@start", SqlDbType.Int);
            cmd.Parameters["@start"].Value = ((currentPage - 1) * PageSize) + 1;
            cmd.Parameters.Add("@end", SqlDbType.Int);
            cmd.Parameters["@end"].Value = currentPage * PageSize;
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            //分頁
            SqlCommand totalcommand;
                totalcommand = new SqlCommand(@"select count('x') from News ", Conn);
           
            SqlDataAdapter totalAdapter = new SqlDataAdapter(totalcommand);
            DataTable totalTable = new DataTable();
            totalAdapter.Fill((totalTable));
            int total = Convert.ToInt32(totalTable.Rows[0][0]);
            Pagination1.totalitems = total;
            Pagination1.limit = PageSize;
            Pagination1.targetpage = "News.aspx";
            //技巧:利用這種方式才可以呼叫usercontrol裡的public method
            Pagination1.showPageControls();

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
}