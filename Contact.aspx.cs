using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Region_sel();
            Yachts_sel();

        }
    }

    #region Data
    protected void Region_sel()
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
            foreach (DataRow dr_ in dt.Rows)
            {
                dl_Region.Items.Add(new ListItem(dr_["Region"].ToString(), dr_["R_no"].ToString()));
            }
        }
        catch (Exception ex)
        {
            DB_string.log("Contact_dl:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    protected void Yachts_sel()
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
            foreach (DataRow dr_ in dt.Rows)
            {
                if (dr_["Isnew"].ToString() == "1")
                    dl_Yachts.Items.Add(new ListItem(dr_["Modal"].ToString() + " " + dr_["Modal_n"].ToString() +" (New Building)", dr_["Yachtsno"].ToString()));
                else
                    dl_Yachts.Items.Add(new ListItem(dr_["Modal"].ToString() + " " + dr_["Modal_n"].ToString(), dr_["Yachtsno"].ToString()));
            }
        }
        catch (Exception ex)
        {
            DB_string.log("Contact_dl:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion

    #region Button
    protected void btn_submit_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~/sqlimages/Mail/email.xml"));
        string xpathChiefComplaint = "/root/首頁-聯繫我們-title";
        XmlNode xnChiefComplaint = doc.SelectSingleNode(xpathChiefComplaint);

        string nodeValue = xnChiefComplaint.InnerText;
        ////寄信
        //using (var mySmtp = new System  .Net.Mail.SmtpClient("smtp.gmail.com", 587))
        //{
        //    mySmtp.Credentials = new System.Net.NetworkCredential("ga203306@gmail.com", "38xxx5438");
        //    mySmtp.EnableSsl = true;
        //    mySmtp.Send("ga203306@gmail.com",
        //    "ga203306@yahoo.com.tw",
        //    "線上填寫表單:",
        //    "Name " + name.Value + "\n" +
        //    "Email " + email.Value + "\n" +
        //    "Phone " + phone.Value + "\n" +
        //    "Region " + dl_Region.SelectedItem.Text + "\n" +
        //    "Yachts " + dl_Yachts.SelectedItem.Text + "\n" +
        //    "Comments " + comments.Value + "\n"
        //   );
        //}
        //ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal({title: '已寄出',text: '',type: \"success\",},function() { window.location = \"Default.aspx\";});</script>", false);

    }
    #endregion


}