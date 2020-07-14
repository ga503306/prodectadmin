using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dealers : System.Web.UI.Page
{
    private const int PageSize = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Region_sel();
            Rpt_Region.DataSource = dt;
            Rpt_Region.DataBind();
            if (Request.QueryString["id"] != null)
            {
                DataTable dtD = new DataTable();
                dtD = Detail(Request.QueryString["id"]);
                Rpt_Detail.DataSource = dtD;
                Rpt_Detail.DataBind();
            }
            else
            {
                DataTable dtD = new DataTable();
                dtD = Detail("");
                Rpt_Detail.DataSource = dtD;
                Rpt_Detail.DataBind();
            }
            //if (Session["R_no"] == null)
            //{
            //    DataTable dtD = new DataTable();
            //    dtD = Detail("");
            //    Rpt_Detail.DataSource = dtD;
            //    Rpt_Detail.DataBind();
            //}
            //else
            //{
            //    DataTable dtD = new DataTable();
            //    dtD = Detail(Session["R_no"].ToString());
            //    Rpt_Detail.DataSource = dtD;
            //    Rpt_Detail.DataBind();
            //}
        }
    }

    #region Data
    public static DataTable Region_sel()
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
            DB_string.log("DealersR:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }

    public DataTable Detail(string id)
    {
        //分頁用
        int currentPage = Request["page"] == null ? 1 : int.Parse(Request["page"]);

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
         
            string CmdString = @"";
            if (id == "")
                //CmdString = @"select * from DealersD d  
                //              join DealersR r on d.R_no = r.R_no      
                //              join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                //              where d.R_no=(select top 1 R_no from DealersR) ";
                CmdString = @"with tstation as (
                              select row_number() over(order by d.R_no asc) as rownumber,
                              d.R_no,d.C_no,d.D_no,D.Info,D.Img,
							  c.Country,
							  r.Region 
                              from DealersD d 
                              join DealersR r on d.R_no = r.R_no 
                              join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                              where d.R_no=(select top 1 R_no from DealersR))select *from tstation where rownumber>=@start and rownumber <=@end";
            else
                //CmdString = @"select * from DealersD d 
                //              join DealersR r on d.R_no = r.R_no 
                //              join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                //              where d.R_no=@id ";
                CmdString = @"with tstation as (
                              select row_number() over(order by d.R_no asc) as rownumber,
                              d.R_no,d.C_no,d.D_no,D.Info,D.Img,
							  c.Country,
							  r.Region 
                              from DealersD d 
                              join DealersR r on d.R_no = r.R_no 
                              join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                              where d.R_no=@id)select *from tstation where rownumber>=@start and rownumber <=@end";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id);
            //分頁
            cmd.Parameters.Add("@start", SqlDbType.Int);
            cmd.Parameters["@start"].Value = ((currentPage - 1) * PageSize) + 1;
            cmd.Parameters.Add("@end", SqlDbType.Int);
            cmd.Parameters["@end"].Value = currentPage * PageSize;


            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            title.InnerText = dt.Rows[0]["Region"].ToString(); //title

            //分頁
            SqlCommand totalcommand;
            if (id == "")
            {
                totalcommand = new SqlCommand(@"select count('x') from DealersD d 
                                                       join DealersR r on d.R_no = r.R_no 
                                                       join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                                                       where d.R_no=(select top 1 R_no from DealersR) ", Conn);
            }
            else
            {

            
                totalcommand = new SqlCommand(@"select count('x') from DealersD d 
                                                       join DealersR r on d.R_no = r.R_no 
                                                       join DealersC c on c.R_no = r.R_no and c.C_no = d.C_no
                                                       where d.R_no=@id ", Conn);
             }
            totalcommand.Parameters.AddWithValue("id", id);
            SqlDataAdapter totalAdapter = new SqlDataAdapter(totalcommand);
            DataTable totalTable = new DataTable();
            totalAdapter.Fill((totalTable));
            int total = Convert.ToInt32(totalTable.Rows[0][0]);
            Pagination1.totalitems = total;
            Pagination1.limit = PageSize;
            Pagination1.targetpage = "Dealers.aspx?id=" + id;
            //技巧:利用這種方式才可以呼叫usercontrol裡的public method
            Pagination1.showPageControls();

            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_Detail:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    protected void Rpt_region_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string str = e.CommandArgument.ToString();
            string[] Lstr = str.Split(',');
            title.InnerText = Lstr[1]; //title
            Response.Redirect("Dealers.aspx?id=" + Lstr[0]);
            //Session["R_no"] = Lstr[0];
            //DataTable dt = new DataTable();
            //dt = Detail(Lstr[0]);
            //Rpt_Detail.DataSource = dt;
            //Rpt_Detail.DataBind();
        }
    }
}