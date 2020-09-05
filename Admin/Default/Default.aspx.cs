using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Admin_NewFolder1_Default : System.Web.UI.Page
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
                //List<Weather> weathers = new List<Weather>();
                //int index = 0;

                //string state = ""; //英文描述
                //string state_chs = ""; //中文描述
                //string locationName = ""; //中文地名
                ////var weather = weathers.Where(x => x.時間 == "2019").FirstOrDefault();
                ////weathers.Add(new Weather { 時間 = xnlD[0].InnerText, 溫度 = xnlD[1].InnerText, 露點溫度 = "" });
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load("C:\\Users\\ga503\\Downloads\\F-D0047-065.xml");
                //XmlNamespaceManager xnm = new XmlNamespaceManager(xmlDoc.NameTable);
                ////xnm.RemoveNamespace("xmlns", "urn:cwb:gov:tw:cwbcommon:0.1");
                //xnm.AddNamespace("pf", "urn:cwb:gov:tw:cwbcommon:0.1");

                //XmlNode xn = xmlDoc.SelectSingleNode("//pf:cwbopendata/pf:dataset/pf:locations", xnm);
                //XmlNodeList xnlA = xn.ChildNodes;
                //foreach (XmlNode xnA in xnlA)
                //{
                //    XmlElement xeB = (XmlElement)xnA;
                //    XmlNodeList xnlB = xeB.ChildNodes;
                //    foreach (XmlNode xnB in xnlB)
                //    {
                //        if (xnB.Name == "locationName")
                //        {
                //            XmlElement xeC = (XmlElement)xnB;
                //            XmlNodeList xnlC = xeC.ChildNodes;
                //            locationName = xnlC[0].InnerText;//地名暫存
                //        }
                //        if (xnB.Name == "weatherElement")
                //        {
                //            XmlElement xeC = (XmlElement)xnB;
                //            XmlNodeList xnlC = xeC.ChildNodes;
                //            foreach (XmlNode xnC in xnlC)
                //            {
                //                if (xnC.Name == "elementName")
                //                {
                //                    state = xnC.InnerText;
                //                }
                //                else if (xnC.Name == "description")
                //                {
                //                    state_chs = xnC.InnerText;
                //                }
                //                else
                //                {
                //                    if (xnC.Name == "time" && state == "T")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        weathers.Add(new Weather { 地點 = locationName, 時間 = xnlD[0].InnerText, 溫度 = xnlD[1].InnerText });
                //                    }
                //                    if (xnC.Name == "time" && state == "Td")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].露點溫度 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "RH")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].相對溼度 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "PoP6h")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].六小時降雨機率 = xnlD[2].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "PoP12h")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].十二小時降雨機率 = xnlD[2].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "WD")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].風向 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "WS")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].風速 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "CI")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].舒適度指數 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "AT")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].體感溫度 = xnlD[1].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "Wx")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].天氣現象 = xnlD[2].ChildNodes[0].InnerText;
                //                    }
                //                    if (xnC.Name == "time" && state == "WeatherDescription")
                //                    {
                //                        XmlElement xeD = (XmlElement)xnC;
                //                        XmlNodeList xnlD = xeD.ChildNodes;
                //                        index = weathers.FindIndex(x => (x.地點 == locationName) && (x.時間 == xnlD[0].InnerText));
                //                        weathers[index].天氣預報綜合描述 = xnlD[2].ChildNodes[0].InnerText;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}

                //StringBuilder sb = new StringBuilder("<table><tr><th>時間</th><th>溫度</th><th>露點溫度</th><th>相對濕度</th></tr>");
                //foreach (Weather wt in weathers)
                //{
                //    sb.Append("<tr> <td>" + wt.時間 + " </td> <td>" + wt.溫度 + " </td> <td>" + wt.露點溫度 + " </td><td>" + wt.相對溼度 + " </td> </tr>");
                //}
                //sb.Append("</table>");


                //test.InnerHtml = sb.ToString();

            }
        }
    }
}

