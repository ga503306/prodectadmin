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


public partial class Admin_Dealers_Dealers_edit : System.Web.UI.Page
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
                    Dealers(id.Value);
                    Detail(id.Value);
                }

            }
        }
    }

    #region Data
    public void Dealers(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from DealersR where R_no=@R_no ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("R_no", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            R_no.Value = dt.Rows[0]["R_no"].ToString();
            Region.Value = dt.Rows[0]["Region"].ToString();

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

    protected void Detail(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * 
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

    protected void del_row(string id_)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"delete from DealersC where R_no=@R_no and C_no=@C_no";
            SqlCommand cmd = new SqlCommand(CmdString, Conn,tran);
            cmd.Parameters.AddWithValue("R_no", id.Value);
            cmd.Parameters.AddWithValue("C_no", id_);
            cmd.ExecuteNonQuery();
            tran.Commit();

        }
        catch (Exception ex)
        {
            tran.Rollback();
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

            CmdString = @"update DealersR set Region=@Region where R_no=@R_no";

            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("R_no", id.Value);
            cmd.Parameters.AddWithValue("Region", Region.Value);
            cmd.ExecuteNonQuery();

            //DataTable dtD = new DataTable();
            //dtD = (DataTable)ViewState["Detail"];
            //detail 存檔
            //if (detailtype.Value == "true")
            //{

            //    string DelString = @"";
            //    DelString = @"delete from DealersC where R_no=@R_no";
            //    SqlCommand delcmd = new SqlCommand(DelString, Conn, tran);
            //    delcmd.Parameters.AddWithValue("R_no", id.Value);
            //    delcmd.ExecuteNonQuery();

            //    string InsString = @"";
            //    int serial = 0;
            //    //拼字串
            //    foreach (DataRow rw in dtD.Rows)
            //    {

            //        InsString += @"insert into DealersC (R_no, Country) 
            //                       values (@R_no,@Country" + serial + @");
            //                      ";
            //        serial++;
            //    }

            //    SqlCommand inscmd = new SqlCommand(InsString, Conn, tran);
            //    //丟參數
            //    serial = 0;
            //    foreach (DataRow rw in dtD.Rows)
            //    {

            //        inscmd.Parameters.AddWithValue("Country" + serial, rw["Country"].ToString());
            //        serial++;
            //    }


            //    inscmd.Parameters.AddWithValue("R_no", id.Value);
            //    //inscmd.Parameters.AddWithValue("C_no", rw["C_no"].ToString());
            //    if (InsString == null)
            //        inscmd.ExecuteNonQuery();




            //}
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Dealers_edit:", ex.ToString());
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

        ScriptManager.RegisterStartupScript(Page, GetType(), "function", "<script>edit('" + R_no.Value + "','','');</script>", false);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalshow", "$('#modal-normal').modal('show');", true);
    }

    protected void Enter_Click(object sender, EventArgs e)
    {
        string state = DetailState.Value;
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["Detail"];
        if (state == "ins")
        {
            //detailtype.Value = "true";

            //DataRow dr = dt.NewRow();

            //dr["R_no"] = R_no.Value;
            //dr["C_no"] = 0;
            //dr["Country"] = Country_detail.Value;

            //dt.Rows.Add(dr);

            //ViewState["Detail"] = dt;
            //Grid_DealersC.DataSource = dt;
            //Grid_DealersC.DataBind();
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {


                string InsString = @"";
                InsString += @"insert into DealersC (R_no, Country) 
                                values (@R_no,@Country)";
                SqlCommand inscmd = new SqlCommand(InsString, Conn, tran);
                inscmd.Parameters.AddWithValue("R_no", id.Value);
                inscmd.Parameters.AddWithValue("Country", Country_detail.Value);
                inscmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                DB_string.log("Dealers_edit_add:", ex.ToString());
            }
            finally
            {
                Conn.Close();
            }
            Detail(id.Value);


        }
        else
        {
            //detailtype.Value = "true";

            //int indexid = 0;
            //indexid = Convert.ToInt32(index_detail.Value);

            //dt.Rows[indexid]["R_no"] = R_no.Value;
            ////dt.Rows[indexid]["C_no"] = C_no_detail.Value;
            //dt.Rows[indexid]["Country"] = Country_detail.Value;
            //Grid_DealersC.DataSource = dt;
            //Grid_DealersC.DataBind();
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {


                string InsString = @"";
                InsString += @"update DealersC set Country=@Country where R_no=@R_no and C_no=@C_no";
                SqlCommand inscmd = new SqlCommand(InsString, Conn, tran);
                inscmd.Parameters.AddWithValue("R_no", id.Value);
                inscmd.Parameters.AddWithValue("C_no", C_no_detail.Value);
                inscmd.Parameters.AddWithValue("Country", Country_detail.Value);
                inscmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                DB_string.log("Dealers_edit_update:", ex.ToString());
            }
            finally
            {
                Conn.Close();
            }
            Detail(id.Value);

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('hide');", true);

    }

    protected void Close_Click(object sender, EventArgs e)
    {
        //DB_fountion.clearvalue(Page, "cpno_deail,cdno_detail,cdname_detail,description_detail");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('hide');", true);
    }

    protected void del_Click(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"DELETE FROM DealersR where R_no=@R_no";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("R_no", id.Value);
            Conn.Open();
            cmd.ExecuteNonQuery();
            Conn.Close();
            Response.Redirect("Dealers.aspx?type=dealers");
        }
        catch (Exception ex)
        {
            DB_string.log("Dealers_edit_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗','地區尚未全刪除')</script>", false);
        }
        finally
        {

        }
       
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

            ScriptManager.RegisterStartupScript(Page, GetType(), "function", "<script>edit('" + index + "','" + Lstr[0] + "','" + Lstr[1] + "','" + Lstr[2] + "');</script>", false);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closepup", "$('#modal-normal').modal('show');", true);
        }

        if (e.CommandName == "Del")
        {
            //detailtype.Value = "true";

            //int indexid = Convert.ToInt16(e.CommandArgument.ToString());
            //DataTable dt = new DataTable();
            //dt = (DataTable)ViewState["Detail"];
            //dt.Rows.RemoveAt(indexid);
            //ViewState["Detail"] = dt;
            //Grid_DealersC.DataSource = dt;
            //Grid_DealersC.DataBind();
            string str = e.CommandArgument.ToString();
            string[] Lstr = str.Split(',');
            del_row(Lstr[1]);//C_no
            Detail(id.Value);
        }
    }

    protected void Grid_DealersC_PreRender(object sender, EventArgs e)
    {
        Grid_DealersC.UseAccessibleHeader = true;
        Grid_DealersC.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    #endregion
}