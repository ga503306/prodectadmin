using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class api_auth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //儲存
    [WebMethod]
    public static string Save(List<Data> postData, DataSolo G_no)
    {
        string result = "";
        DB_data.AuthDel(G_no.ID);
        foreach (Data _postData in postData)
        {
            if (DB_data.AuthBtnsave(G_no.ID,_postData.ID) == "{\"Type\": \"失敗\"}")
            {
                result = "{\"Type\": \"失敗\"}";
                return result;
            }
        }
        result = "{\"Type\": \"成功\"}";
        return result;
    }
    //初始化
    [WebMethod]
    public static string Init(DataSolo G_no)
    {
        string result = "";
        result = DB_data.Init(G_no.ID);
        return result;
    }
    //確認權限 matserpager用
    [WebMethod]
    public static string getAuth()
    {
        string result = "";
        result = DB_data.getAuth();
        return result;
    }
    public class Data
    {
        public String ID { get; set; }
    }
    public class DataSolo
    {
        public String ID { get; set; }
    }
}