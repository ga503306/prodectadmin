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


public partial class Admin_Album_Album_edit : System.Web.UI.Page
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
                if ((Request.QueryString["id"] != null) && (Request.QueryString["id"].ToString() != ""))
                {
                    id.Value = Request.QueryString["id"];
                    DBinit();
                }

            }
        }
    }

    #region Data
    protected void DBinit()
    {
        try
        {

            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value))
            {
                //新增資料夾
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value);
            }
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("../../sqlimages/Album/" + id.Value));
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

            rpt_complaint.DataSource = dt;
            rpt_complaint.DataBind();
        }
        catch (Exception ex)
        {
            DB_string.log("DBinit:", ex.ToString());
        }

    }
    #endregion

    #region Button
    protected void upimg_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value))
            {
                //新增資料夾
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/") + @"/sqlimages/Album/" + id.Value);
            }
            string ext = System.IO.Path.GetExtension(FileUpload1.FileName);
            String FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
            String SavePath = Server.MapPath("/") + @"/sqlimages/Album/" + id.Value + "/" + FileName;
            this.FileUpload1.SaveAs(SavePath);
        }

        DBinit();
    }
    #endregion

    #region Repeater
    protected void rpt_complaint_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            int index = ((RepeaterItem)((Button)e.CommandSource).NamingContainer).ItemIndex;
            string FilePath = Server.MapPath("/") + @"/sqlimages/Album/";
            string File = e.CommandArgument.ToString();
            DeleteFile(File, FilePath);
            DBinit();//上傳圖片
        }
    }
    #endregion

    #region Other
    public void DeleteFile(string filename, string addr)
    {
        string path = addr + filename;
        try
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        catch
        {

        }
    }
    #endregion

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Yachts/Yachts_edit.aspx?type=yachts&id=" + id.Value);
    }
}