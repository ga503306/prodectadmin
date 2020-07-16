<%@ WebHandler Language="C#" Class="sendmail" %>

using System;
using System.Web;
using System.IO;
using Newtonsoft.Json;

public class sendmail : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string json = "";
        string result = "";
        using (var reader = new StreamReader(context.Request.InputStream))
        {
            json = reader.ReadToEnd();
        }

        if (!string.IsNullOrEmpty(json))
        {
            Data data = JsonConvert.DeserializeObject<Data>(json);
            result = DB_data.sendmail(data.name, data.email, data.phone, data.dl_Region, data.dl_Yachts, data.comments);
            context.Response.Write(result);
        }


    }
    public class Data
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string dl_Region { get; set; }
        public string dl_Yachts { get; set; }
        public string comments { get; set; }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}