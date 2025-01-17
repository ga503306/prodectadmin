﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class News_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Request.QueryString["id"] != null) && (Request.QueryString["id"].ToString() != ""))
            {
                id.Value = Request.QueryString["id"];
                DataTable dt = News_sel(id.Value);
                Title_.InnerText = dt.Rows[0]["Title"].ToString();
                Rpt_News.DataSource = dt;
                Rpt_News.DataBind();
            }
        }
    }

    #region Data
    public static DataTable News_sel(string id)
    {
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlString"].ConnectionString;
        DataTable dt = new DataTable();
        try
        {
            string CmdString = @"";
            CmdString = @"select Title,Context from News where Newsno=@Newsno";

            SqlCommand cmd = new SqlCommand(CmdString, Conn);
            cmd.Parameters.AddWithValue("Newsno", id);
            Conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            DB_string.log("News_view:", ex.ToString());
            return null;
        }
        finally
        {
            Conn.Close();
        }
    }
    #endregion
}