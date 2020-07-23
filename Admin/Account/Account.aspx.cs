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

public partial class Admin_Account_Account : System.Web.UI.Page
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

            }
        }
    }

    protected void pwd_save_Click()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();

        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"update Employee  set Password=@pwd where Username=@Username ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn, tran);
            cmd.Parameters.AddWithValue("Username", Session["Username"].ToString());
            string des_Password = DB_fountion.EncryptDES(npwd.Value);//加密
            cmd.Parameters.AddWithValue("pwd", des_Password);

            cmd.ExecuteNonQuery();
            tran.Commit();
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert_success", "<script>swal('修改成功')</script>", false);
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Account:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

    #region Button
    protected void save_Check(object sender, EventArgs e)
    {
        pwd_save_Check();
    } 
    #endregion

    #region Other
    protected void pwd_save_Check()
    {
        string result_checkpwd = checkpwd();
        string result_checknewpwd = checknewpwd();
        if (result_checkpwd == "OK" && result_checknewpwd == "OK")
        {
            pwd_save_Click();
        }
        else if (result_checkpwd != "OK")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('" + result_checkpwd + "')</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('" + result_checknewpwd + "')</script>", false);
        }
    }

    //判斷帳號的舊密碼是否輸入正確
    protected string checkpwd()
    {
        string result;
        SqlConnection ConnSel = new SqlConnection();
        ConnSel.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        string SelCmdString = @"";
        SelCmdString = @"select 'x' from Employee where Username=@Username and Password=@pwd";
        SqlCommand Selcmd = new SqlCommand(SelCmdString, ConnSel);
        string des_Password = DB_fountion.EncryptDES(pwd.Value);//加密
        Selcmd.Parameters.AddWithValue("Username", Session["Username"].ToString());
        Selcmd.Parameters.AddWithValue("pwd", des_Password);

        ConnSel.Open();
        SqlDataReader dr = Selcmd.ExecuteReader(CommandBehavior.CloseConnection);
        dt.Load(dr);
        if (dt != null && dt.Rows.Count > 0)
            result = "OK";
        else
            result = "舊密碼錯誤";
        ConnSel.Close();
        return result;
    }

    //判斷新密碼與確認新密碼
    protected string checknewpwd()
    {
        string result = "";
        if (string.IsNullOrEmpty(npwd.Value))
            result += "新密碼未填 ";
        if (string.IsNullOrEmpty(chknpwd.Value))
            result += "確認新密碼未填 ";

        if (!string.IsNullOrEmpty(chknpwd.Value) && !string.IsNullOrEmpty(chknpwd.Value))
        {
            if (npwd.Value == chknpwd.Value)
                result = "OK";
            else
                result = "新密碼與確認新密碼不相同";
        }
        return result;
    }

    #endregion
}