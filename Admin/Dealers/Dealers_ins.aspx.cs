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

public partial class Admin_Dealers_Dealers_ins : System.Web.UI.Page
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
                    //id.Value = Request.QueryString["id"];
                    //Dealers(id.Value);
                    Detail("");
                

            }
        }
    }

    #region Data

    protected void Detail(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select country 
                           from DealersC where R_no=@R_no ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("R_no", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            ViewState["Detail"] = dt;
            Grid_DealersC.DataSource = dt;
            Grid_DealersC.DataBind();

        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region  Button
    protected void save_Check(object sender, EventArgs e)
    {
        save_Click();
        Response.Redirect("Dealers.aspx?type=dealers");
    }

    protected void save_Click()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();

        try
        {
            string CmdString = @"";
            DataTable serial = new DataTable();
            serial = DB_fountion.GetNo("R_no", "DealersR");
            id.Value = DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString();
            CmdString = @"insert DealersR (R_no,Region)
                                   values (@R_no,@Region)";
           
            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("R_no", DateTime.Now.ToString("yyyyMMdd") + serial.Rows[0]["sno"].ToString());
            cmd.Parameters.AddWithValue("Region", Region.Value);
            cmd.ExecuteNonQuery();

            DataTable dtD = new DataTable();
            dtD = (DataTable)ViewState["Detail"];
            if (detailtype.Value == "true")
            {

                //foreach (DataRow rw in dtD.Rows)
                //{
                //    string InsString = @"";
                //    InsString = @"insert into DealersC (R_no, Country) 
                //                  values (@R_no,@Country)";
                //    SqlCommand inscmd = new SqlCommand(InsString, Conn, tran);
                //    inscmd.Parameters.AddWithValue("R_no", id.Value);
                //    inscmd.Parameters.AddWithValue("Country", rw["Country"].ToString());
                //    inscmd.ExecuteNonQuery();
                //}
                string InsString = @"";
                int serial_ins = 0;
                //拼字串
                foreach (DataRow rw in dtD.Rows)
                {

                    InsString += @"insert into DealersC (R_no, Country) 
                                   values (@R_no,@Country" + serial + @");
                                  ";
                    serial_ins++;
                }
                SqlCommand inscmd = new SqlCommand(InsString, Conn, tran);
                //丟參數
                serial_ins = 0;
                foreach (DataRow rw in dtD.Rows)
                {

                    inscmd.Parameters.AddWithValue("Country" + serial, rw["Country"].ToString());
                    serial_ins++;
                }

                inscmd.Parameters.AddWithValue("R_no", id.Value);
                //inscmd.Parameters.AddWithValue("C_no", rw["C_no"].ToString());
                if (InsString == null)
                    inscmd.ExecuteNonQuery();
            }
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Dealers_ins:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void addBtn_Click(object sender, EventArgs e)
    {
        DetailState.Value = "ins";
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Detail"];

        ScriptManager.RegisterStartupScript(Page, GetType(), "function", "<script>edit('','','');</script>", false);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalshow", "$('#modal-normal').modal('show');", true);
    }

    protected void Enter_Click(object sender, EventArgs e)
    {
        string state = DetailState.Value;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Detail"];
        if (state == "ins")
        {
            detailtype.Value = "true";

            DataRow dr = dt.NewRow();

            dr["Country"] = Country_detail.Value;

            dt.Rows.Add(dr);

            ViewState["Detail"] = dt;

            Grid_DealersC.DataSource = dt;
            Grid_DealersC.DataBind();

        }
        else
        {
            detailtype.Value = "true";

            int indexid = 0;
            indexid = Convert.ToInt32(index_detail.Value);

            //dt.Rows[indexid]["R_no"] = id.Value;
            //dt.Rows[indexid]["C_no"] = C_no_detail.Value;
            dt.Rows[indexid]["Country"] = Country_detail.Value;


            Grid_DealersC.DataSource = dt;
            Grid_DealersC.DataBind();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('hide');", true);

    }

    protected void Close_Click(object sender, EventArgs e)
    {
        //DB_fountion.clearvalue(Page, "cpno_deail,cdno_detail,cdname_detail,description_detail");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('hide');", true);
    }
    #endregion

    #region GridView

    protected void Grid_DealersC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sel")
        {
            DetailState.Value = "edit";
            string str = e.CommandArgument.ToString();
            string[] Lstr = str.Split(',');
            int index = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;

            ScriptManager.RegisterStartupScript(Page, GetType(), "function", "<script>edit('" + index + "','" + Lstr[0] + "');</script>", false);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('show');", true);
        }

        if (e.CommandName == "Del")
        {
            detailtype.Value = "true";

            int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["Detail"];
            dt.Rows.RemoveAt(indexid);
            ViewState["Detail"] = dt;
            Grid_DealersC.DataSource = dt;
            Grid_DealersC.DataBind();

        }
    }

    protected void Grid_DealersC_PreRender(object sender, EventArgs e)
    {
        Grid_DealersC.UseAccessibleHeader = true;
        Grid_DealersC.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    #endregion
}