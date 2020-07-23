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

public partial class Admin_File_File_edit : System.Web.UI.Page
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
                id.Value = Request.QueryString["id"];
                //Session["savePath"] = @"~/sqimages/LCMaterials/" + Session["sqkeyid"].ToString() + "/";
                ViewFile(id.Value);
            }
        }
    }

    #region data
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
        dt.Columns.Add("FileName_full", typeof(String));

        foreach (string FilePath in System.IO.Directory.GetFileSystemEntries(
                 Server.MapPath("~") + @"/sqlimages/File/" + id + "/"))
        {
            string[] File = FilePath.Split('/');
            int indexid = File.GetUpperBound(0);
            string FileName = File[indexid];
            string FileName_full = File[indexid];
            string addr = @"sqlimages\File\" + id + "\\";
            string FileMapPath = @"sqlimages\File\" + id + "\\" + FileName;

            dt.Rows.Add(FileName, FilePath, FileMapPath, addr, FileName_full);
        }
        ViewState["ViewPath"] = dt;
        Rpt_File.DataSource = dt;
        Rpt_File.DataBind();
    }
    #endregion

    #region button
    protected void save_Click(object sender, EventArgs e)
    {
        string savePath = @"~/sqlimages/File/" + id.Value + "/";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~") + @"\sqlimages\File\" + id.Value))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + @"\sqlimages\File\" + id.Value);
        }

        if (SaveFile.HasFile)
        {
            //string fileName = Server.HtmlEncode(SaveFile.FileName);
            string fileName = string.Empty;
            fileName = SaveFile.FileName;
            string extension = System.IO.Path.GetExtension(fileName);

            if (string.IsNullOrEmpty(Custom_name.Value))
                fileName = SaveFile.FileName;
            else
                fileName = Custom_name.Value + extension;

            if ((extension == ".docx") || (extension == ".xls") || (extension == ".jpg") || (extension == ".jpeg") || (extension == ".png") || (extension == ".gif") || (extension == ".doc") ||
                (extension == ".xlsx") || (extension == ".ppt") || (extension == ".pptx") || (extension == ".ppsx") || (extension == ".pdf") || (extension == ".txt") || (extension == ".pst") ||
                (extension == ".csv") || (extension == ".xml"))
            {
                savePath += fileName;
                SaveFile.SaveAs(Server.MapPath(savePath));
                ScriptManager.RegisterStartupScript(Page, GetType(), "alert", "<script>swal('檔案上傳成功')</script>", false);
                //Response.Redirect("Labor contract.aspx?type=hrm");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "alert2", "<script>swal('不支援此檔案類型')</script>", false);
            }
            ViewFile(id.Value);
        }

    }


    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Yachts/Yachts_edit.aspx?type=yachts&id=" + id.Value);
    }
    #endregion

    #region  Repeater

    protected void Rpt_File_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            string FilePath = Server.MapPath(@"~/sqlimages/File/" + id.Value + "/");
            string DeleteFileName = e.CommandArgument.ToString();

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["ViewPath"];
            //for (int number = 0; number < dt.Rows.Count; number++)
            //{
            //    if (dt.Rows[number]["FileName_full"].ToString() == File)
            //    {
            //        string DeleteFileName = dt.Rows[number]["FileName_full"].ToString();
            //        DeleteFile(DeleteFileName, FilePath);
            //    }
            //}
            DeleteFile(DeleteFileName, FilePath);
            ViewFile(id.Value);
        }
    }

    protected void Rpt_File_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["ViewPath"];

            for (int number = 0; number < dt.Rows.Count; number++)
            {
                string FileName = dt.Rows[number]["FileName"].ToString();
                if (FileName.Length > 15)
                {
                    string ext = System.IO.Path.GetExtension(FileName);
                    string str = FileName.Substring(0, 15);
                    FileName = str + "..." + ext;
                    dt.Rows[number]["FileName"] = FileName;
                }
            }
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
}