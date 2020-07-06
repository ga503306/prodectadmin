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

public partial class Admin_Employee_Employee_edit : System.Web.UI.Page
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

                if ((Session["id"] != null) && (Session["id"].ToString() != ""))
                {
                    Employee(Session["id"].ToString());
                    del_btn.Visible = true;
                }
                else
                {
                    del_btn.Visible = false;
                }
                List();
            }
        }
    }

    #region Data
    public void Employee(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select * from Employee where Username=@Username ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Username", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);

            Username.Value = dt.Rows[0]["Username"].ToString();
            Password.Value = dt.Rows[0]["Password"].ToString();
            Auth.SelectedValue = dt.Rows[0]["Auth"].ToString();

        }
        catch (Exception ex)
        {
            DB_string.log("Employee_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

    //下拉選單預設
    public void List()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        SqlCommand cmdCheck = new SqlCommand(@"select value,value_string from Parameter where Table_name = 'EMployee' and Key_string = 'Auth' ", Conn);
        Conn.Open();
        SqlDataReader dr = cmdCheck.ExecuteReader();
        ((DropDownList)this.Master.FindControl("ContentPlaceHolder1").FindControl(("Auth"))).Items.Clear();
        while (dr.Read())
        {
            ((DropDownList)this.Master.FindControl("ContentPlaceHolder1").FindControl(("Auth"))).Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
        }
    }
    #endregion

    #region Button
    protected void save_Check(object sender, EventArgs e)
    {
        if (Username.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('*號欄位不可為空值')</script>", false);
        }
        else
        {
            save_Click();
        }
    }

    protected void save_Click()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();

        DataTable dt = new DataTable();
        try
        {
            string state = Session["type"].ToString();
            string id = "";

            if ((Session["id"] != null) && (Session["id"].ToString() != ""))
            {
                id = Session["id"].ToString();
            }

            string CmdString = @"";
            if (state == "ins")
            {
                CmdString = @"insert into Employee (Username,Password,Auth) 
                              values (@Username,@Password,@Auth)";

            }
            else
            {//,Password=@Password
                CmdString = @"update Employee set Username=@Username,Auth=@Auth where Username=@id";
            }

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("Username", Username.Value);
            string des_Password = DB_fountion.EncryptDES(Password.Value);//加密
            //cmd.Parameters.AddWithValue("Password", des_Password);
            cmd.Parameters.AddWithValue("Auth", Auth.SelectedValue);

            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            DB_string.log("Employee_edit:", ex.ToString());
        }
        finally
        {
            Conn.Close();
            Response.Redirect("Employee.aspx?type=owner");
        }
    }


    protected void del_Click(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"delete from Employee where Username=@id ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", Session["id"].ToString());
            Conn.Open();
            cmd.ExecuteNonQuery();

            Response.Redirect("Employee.aspx?type=basic");
        }
        catch (Exception ex)
        {
            DB_string.log("Employee_del:", ex.ToString());
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('刪除失敗')</script>", false);
        }
        finally
        {
            Conn.Close();
        }
    } 
    #endregion
}