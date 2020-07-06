using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// DB_string 的摘要描述
/// </summary>
public class DB_string
{
    public static Boolean log(string log_name, string log_str)
    {
        string file_name = DateTime.Now.ToString("yyyyMMdd");
        string path = System.AppDomain.CurrentDomain.BaseDirectory + "//log//" + file_name + ".txt";


        if (System.IO.File.Exists(path))
        {
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString() + "        " + log_name + "頁面:  錯誤訊息:" + log_str);
                }
            }
        }
        else
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            fileStream.Close();   //切記開了要關,不然會被佔用而無法修改喔!!!

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(DateTime.Now.ToString() + "        " + log_name + "頁面:  錯誤訊息:" + log_str);
            }
        }
        return true;
    }


    public static DataTable Query登入(string Username, string Password)
    {
        //宣告SQL的連線
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"select * from Employee where Username=@Username and Password=@Password ";
            SqlCommand cmd = new SqlCommand(CmdString, Conn); 
            cmd.Parameters.AddWithValue("Username", Username);
            string des_Password = DB_fountion.EncryptDES(Password);//加密
            cmd.Parameters.AddWithValue("Password", des_Password);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            log("Query登入:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
}