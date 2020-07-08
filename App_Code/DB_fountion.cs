using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class DB_fountion
{
    public static DataTable GetNo(string nokey, string ap)
    {
        //宣告SQL的連線
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select REPLICATE('0',7- Len(cast(isnull(max(substring(" + nokey + ",9,7)),0)+1 as varchar(7)) )) + cast(isnull(max(substring(" + nokey + ",9,7)),0)+1 as varchar(7)) as sno from [" + ap + "]";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("GetNo:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    //清page上value ex: DB_fountion.clearvalue(Page,"value欄位名(逗號分開)");
    //DB_fountion.clearvalue(Page,"beginday_school,endday_school,schoolname_school,department_school,state_school,memo_work");
    public static void clearvalue(Page page, string str)
    {
        string[] idname = str.Split(',');
        foreach (string s in idname)
        {
            if (page.Master.FindControl("ContentPlaceHolder1").FindControl((s)) == null)
                continue;

            switch (page.Master.FindControl("ContentPlaceHolder1").FindControl((s)).GetType().Name)
            {
                case "HtmlTextArea":
                    ((HtmlTextArea)(page.Master.FindControl("ContentPlaceHolder1").FindControl((s)))).Value = "";
                    break;
                case "DropDownList":
                    ((DropDownList)(page.Master.FindControl("ContentPlaceHolder1").FindControl((s)))).SelectedValue = "";
                    break;
                case "HtmlInputGenericControl":
                    ((HtmlInputGenericControl)(page.Master.FindControl("ContentPlaceHolder1").FindControl((s)))).Value = "";
                    break;
                case "HtmlInputText":
                    ((HtmlInputText)(page.Master.FindControl("ContentPlaceHolder1").FindControl((s)))).Value = "";
                    break;
                default:
                    break;
            }
        }
    }
    //gird 用表頭找inedx
    public static int tablenametoindex(GridView gridview, GridViewRowEventArgs e, string tablename)
    {
        int index = -1;
        TableCell Cell;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (index = 0; index < gridview.HeaderRow.Cells.Count; index++)
            {
                Cell = gridview.HeaderRow.Cells[index];
                if (Cell.Text.ToString() == tablename)
                    break;
            }
        }
        return index;
    }

    //加密
    public static String EncryptDES(String original)
    {
        try
        {
            String key = "SSSSSSSS"; //必須8碼
            String iv = "87654231"; //必須8碼
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);
            byte[] s = Encoding.ASCII.GetBytes(original);
            ICryptoTransform desencrypt = des.CreateEncryptor();
            return (BitConverter.ToString(desencrypt.TransformFinalBlock(s, 0, s.Length)).Replace("-", string.Empty));
        }
        catch
        {
            return original;
        }
    }

    public static string gridbind(string tablename, string keystring, string value)
    {
        string str_json = "";
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        Conn.Open();
        DataTable dt = new DataTable();
        try
        {

            string SelCmdString = @"";
            SelCmdString = @"select value_string from Parameter where Table_name=@tablename and Key_string=@key_string and Value=@value ";
            SqlCommand Selcmd = new SqlCommand(SelCmdString, Conn);
            Selcmd.Parameters.AddWithValue("tablename", tablename);
            Selcmd.Parameters.AddWithValue("key_string", keystring);
            Selcmd.Parameters.AddWithValue("value", value);
            Selcmd.ExecuteNonQuery();
            SqlDataReader dr = Selcmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            str_json = dt.Rows[0]["value_string"].ToString();
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

}