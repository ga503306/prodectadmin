using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        DataTable UserData = DB_string.Query登入(Username.Value, Password.Value);
        if (UserData != null && UserData.Rows.Count > 0)
        {
            ////產生一個Cookie物件
            //HttpCookie cookie = new HttpCookie("CH");
            ////設定多值(這裡只能以字串的方式設定)
            //cookie.Values.Add("1", UserData.Rows[0]["empno"].ToString());
            //cookie.Values.Add("2", UserData.Rows[0]["empname"].ToString());
            ////設定過期日(這裡只能針對全體cookie物件設定過期日)
            //cookie.Expires = DateTime.Now.AddYears(50);
            ////寫到用戶端
            //Response.Cookies.Add(cookie);

            //Session["ECOCOuserID"] = ECOCOuserID;
            Session["Username"] = UserData.Rows[0]["Username"].ToString();
            Session["Password"] = UserData.Rows[0]["Password"].ToString();
            Response.Redirect("Admin/Default/Default.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>alert_swal();</script>", false);
        }
    }
}