using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Yachts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //左邊選單
            DataTable dt = Shiptype_sel();
            Rpt_Shiptype.DataSource = dt;
            Rpt_Shiptype.DataBind();

            if (Request.QueryString["id"] != null)
                id.Value = Request.QueryString["id"];
            else
                Default_sel();//第一次進
            //輪播
            DBinit();
            ViewFile(id.Value);
            Context_sel();//內容
        }
    }

    #region Data
    public void DBinit()
    {
        try
        {

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value))
            {
                //新增資料夾
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value);
            }
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("/sqlimages/Album/" + id.Value));
            FileInfo[] fi = di.GetFiles();

            DataTable dt = new DataTable();
            DataColumn dcFilename = new DataColumn("strFilename", Type.GetType("System.String"));
            dt.Columns.Add(dcFilename);

            Object[] data = new object[1];

            foreach (FileInfo file in fi)
            {
                data[0] = id.Value + "/" + file.Name;
                dt.Rows.Add(data);
            }

            Rpt_Yachts.DataSource = dt;
            Rpt_Yachts.DataBind();
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_DBinit:", ex.ToString());
        }
    }

    public static DataTable Shiptype_sel()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @" select Yachtsno,Modal,Modal_n,Isnew from Yachts ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_Shiptype_sel:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }

    public void Default_sel()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @" select top 1 Yachtsno from Yachts ";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);

            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            if (dt.Rows.Count > 0)
                id.Value = dt.Rows[0]["Yachtsno"].ToString();
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_Default_sel:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

    public void Context_sel()
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @" select Modal,Modal_n,Overview,Files from Yachts where Yachtsno = @id";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("id", id.Value);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            if (dt.Rows.Count > 0) { 
                context.InnerHtml = dt.Rows[0]["Overview"].ToString();
                ship_name.InnerText = dt.Rows[0]["Modal"].ToString() + " " + dt.Rows[0]["Modal_n"].ToString();
                ship_name_nav.InnerText = dt.Rows[0]["Modal"].ToString() + " " + dt.Rows[0]["Modal_n"].ToString();
            }
        }
        catch (Exception ex)
        {
            DB_string.log("Yachts_Context_sel:", ex.ToString());
        }
        finally
        {
            Conn.Close();
        }
    }

    protected void ViewFile(string id)
    {
        string BeginsavePath = @"~/sqlimages/File" + id + "/";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~") + @"\sqlimages\File\" + id))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + @"\sqlimages\File\" + id);
        }


        DataTable dt = new DataTable();
        dt.Columns.Add("FileName", typeof(String));
        dt.Columns.Add("FilePath", typeof(String));
        dt.Columns.Add("FileMapPath", typeof(String));
        dt.Columns.Add("addr", typeof(String));

        foreach (string FilePath in System.IO.Directory.GetFileSystemEntries(
                 Server.MapPath("~") + @"/sqlimages/File/" + id + "/"))
        {
            string[] File = FilePath.Split('/');
            int indexid = File.GetUpperBound(0);
            string FileName = File[indexid];
            string addr = @"sqlimages\File\" + id + "\\";
            string FileMapPath = @"sqlimages\File\" + id + "\\" + FileName;

            dt.Rows.Add(FileName, FilePath, FileMapPath, addr);
        }
        ViewState["ViewPath"] = dt;
        Rpt_File.DataSource = dt;
        Rpt_File.DataBind();
    }
    #endregion

    #region Other
    protected string isnew(string state)
    {
        string result = "";
        switch (state)
        {
            case "1":
                result = " (New Building)";
                break;
            case "0":
                result = "";
                break;
            default:
                break;
        }
        return result;
    } 
    #endregion
}