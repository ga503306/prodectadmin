﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

public class DB_data
{
    //權限群組 刪除--auth_edit.aspx--del
    public static string AuthDel(string G_no)
    {
        string str_json = "";

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();
        try
        {

            string DelCmdString = @"";
            DelCmdString = @"delete from GroupDetail where G_no=@G_no ";
            SqlCommand Delcmd = new SqlCommand(DelCmdString, Conn, tran);
            Delcmd.Parameters.AddWithValue("G_no", G_no);
            Delcmd.ExecuteNonQuery();
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("AuthDel:", ex.ToString());
            str_json = "{\"Type\": \"失敗\"}";
        }
        finally
        {
            Conn.Close();
        }

        return str_json;
    }
    //權限群組 checkbox儲存--auth_edit.aspx--btn_save
    public static string AuthBtnsave(string G_no, string Group_view)
    {
        string str_json = "";

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        SqlTransaction tran = Conn.BeginTransaction();
        try
        {
            string InsCmdString = @"";
            InsCmdString = @"insert GroupDetail (G_no,Group_name,Group_view)
                                         values (@G_no,(select Group_name from [Group] where G_no = @G_no),@Group_view)";

            SqlCommand Inscmd = new SqlCommand(InsCmdString, Conn, tran);
            Inscmd.Parameters.AddWithValue("G_no", G_no);
            Inscmd.Parameters.AddWithValue("Group_view", Group_view);
            Inscmd.ExecuteNonQuery();
            str_json = "{\"Type\": \"成功\"}";
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            DB_string.log("Default_sub:", ex.ToString());
            str_json = "{\"Type\": \"失敗\"}";
        }
        finally
        {
            Conn.Close();
        }

        return str_json;
    }

    //權限群組 初始化--auth_edit.aspx--init
    public static string Init(string G_no)
    {
        string str_json = "";

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        DataTable dt = new DataTable();
        try
        {

            string SelCmdString = @"";
            SelCmdString = @"select Group_view from GroupDetail where G_no=@G_no ";
            SqlCommand Selcmd = new SqlCommand(SelCmdString, Conn);
            Selcmd.Parameters.AddWithValue("G_no", G_no);
            Selcmd.ExecuteNonQuery();
            SqlDataReader dr = Selcmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            str_json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            str_json = "{\"Type\": \"成功\",\"detail\":" + str_json + "}";
        }
        catch (Exception ex)
        {
            DB_string.log("AuthInit:", ex.ToString());
            str_json = "{\"Type\": \"失敗\"}";
        }
        finally
        {
            Conn.Close();
        }

        return str_json;
    }

    //確認權限 --masterpage.aspx--getAuth
    public static string getAuth()
    {
        string str_json = "";
        DataTable dt = new DataTable();
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        try
        {

            string SelCmdString = @"";
            SelCmdString = @"SELECT gd.G_no,gd.Group_name,gd.Group_view
                             FROM [project].[dbo].[Group] g 
                             join GroupDetail gd 
                             on gd.G_no = g.G_no and gd.Group_name = g.Group_name
                             where g.Group_value = @Group_value";
            SqlCommand Selcmd = new SqlCommand(SelCmdString, Conn);
            Selcmd.Parameters.AddWithValue("Group_value", HttpContext.Current.Session["Auth"]);
            Selcmd.ExecuteNonQuery();
            SqlDataReader dr = Selcmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            str_json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
            str_json = "{\"Type\": \"成功\",\"detail\":" + str_json + "}";
        }
        catch (Exception ex)
        {
            DB_string.log("AuthDel:", ex.ToString());
            str_json = "{\"Type\": \"失敗\"}";
        }
        finally
        {
            Conn.Close();
        }

        return str_json;
    }

    //首頁 寄信--contact.aspx--sendmail
    public static string sendmail(string name, string email, string phone, string dl_Region, string dl_Yachts, string comments)
    {
        string result = "";
        try
        {
            //xml拿標題跟內容範本
            XmlDocument doc = new XmlDocument();
            doc.Load(System.Web.HttpContext.Current.Server.MapPath("~/sqlimages/Mail/email.xml"));
            string xpathChiefComplaint = "/root/首頁-聯繫我們-title";
            XmlNode xnChiefComplaint = doc.SelectSingleNode(xpathChiefComplaint);
            string title = xnChiefComplaint.InnerText;

            doc.Load(System.Web.HttpContext.Current.Server.MapPath("~/sqlimages/Mail/email.xml"));
            xpathChiefComplaint = "/root/首頁-聯繫我們";
            xnChiefComplaint = doc.SelectSingleNode(xpathChiefComplaint);
            string Content = xnChiefComplaint.InnerText;
            Content = Content.Replace("@Name@", name).Replace("@Email@", email).Replace("@Phone@", phone)
                 .Replace("@Region@", dl_Region).Replace("@Yachts@", dl_Yachts).Replace("@Comments@", comments);
            //寄信
            using (var mySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
            {
                mySmtp.Credentials = new System.Net.NetworkCredential("ga203306@gmail.com", "38xxx5438");
                mySmtp.EnableSsl = true;
                MailMessage msg = new MailMessage();
                //收件者，以逗號分隔不同收件者 ex "test@gmail.com,test2@gmail.com"
                //msg.To.Add(string.Join(",", MailList.ToArray()));
                msg.To.Add("ga203306@yahoo.com.tw");
                msg.From = new MailAddress("ga203306@gmail.com", title, System.Text.Encoding.UTF8);
                //郵件標題 
                msg.Subject = title;
                //郵件標題編碼  
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                //郵件內容
                msg.Body = Content;
                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                msg.Priority = MailPriority.Normal;//郵件優先級 
                mySmtp.Send(msg);
                //mySmtp.Send("ga203306@gmail.com",
                //"ga203306@yahoo.com.tw",
                //title, Content
                //"Name " + name + "\n" +
                //"Email " + email + "\n" +
                //"Phone " + phone + "\n" +
                //"Region " + dl_Region + "\n" +
                //"Yachts " + dl_Yachts + "\n" +
                //"Comments " + comments + "\n"
                //);
            }
            result = "成功";
        }
        catch (Exception ex)
        {
            DB_string.log("寄信失敗:", ex.ToString());
            result = "失敗";
        }
        finally
        {
        }

        return result;
    }

}

