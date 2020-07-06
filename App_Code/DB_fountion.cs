using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class DB_fountion
{
    public static DataTable SerialNumber(string sqkeyid, string dbtable, string no)//流水號 日期+三碼 sqkeyid,table名,no欄位名稱
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"Select RIGHT(REPLICATE('0', 8) + CAST(MAX(RIGHT(" + no + ",2))+1 as NVARCHAR), 3) as no from " + dbtable + " where substring(" + no + ",1,8)= '" + DateTime.Now.ToString("yyyyMMdd") + "'";
            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //ExecureScalar
            dt.Load(dr);
            if (string.IsNullOrEmpty(dt.Rows[0]["no"].ToString()))
            {
                dt.Columns["no"].ReadOnly = false;
                dt.Rows[0]["no"] = "001";
            }
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("INSserial:", ex.ToString());
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
}