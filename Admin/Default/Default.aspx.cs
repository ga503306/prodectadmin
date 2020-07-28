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
                List<Weather> weathers = new List<Weather>();
                List<String> 時間 = new List<String>();
                List<String> 溫度 = new List<String>();
                List<String> 露點溫度 = new List<String>();
                int index = 0;

                string state = ""; //英文描述
                string state_chs = ""; //中文描述
                //var weather = weathers.Where(x => x.時間 == "2019").FirstOrDefault();
                //weathers.Add(new Weather { 時間 = xnlD[0].InnerText, 溫度 = xnlD[1].InnerText, 露點溫度 = "" });
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("C:\\Users\\ga503\\Downloads\\F-D0047-065.xml");
                XmlNamespaceManager xnm = new XmlNamespaceManager(xmlDoc.NameTable);
                //xnm.RemoveNamespace("xmlns", "urn:cwb:gov:tw:cwbcommon:0.1");
                xnm.AddNamespace("pf", "urn:cwb:gov:tw:cwbcommon:0.1");

                XmlNode xn = xmlDoc.SelectSingleNode("//pf:cwbopendata/pf:dataset/pf:locations", xnm);
                XmlNodeList xnlA = xn.ChildNodes;
                foreach (XmlNode xnA in xnlA)
                {
                    XmlElement xeB = (XmlElement)xnA;
                    XmlNodeList xnlB = xeB.ChildNodes;
                    foreach (XmlNode xnB in xnlB)
                    {
                        if (xnB.Name == "weatherElement")
                        {
                            XmlElement xeC = (XmlElement)xnB;
                            XmlNodeList xnlC = xeC.ChildNodes;
                            index = 0; //重置索引
                            foreach (XmlNode xnC in xnlC)
                            {
                                if (xnC.Name == "elementName")
                                {
                                    state = xnC.InnerText;
                                }
                                else if (xnC.Name == "description")
                                {
                                    state_chs = xnC.InnerText;
                                }
                                else
                                {
                                    if (xnC.Name == "time" && state == "T")
                                    {
                                        XmlElement xeD = (XmlElement)xnC;
                                        XmlNodeList xnlD = xeD.ChildNodes;
                                        時間.Add(xnlD[0].InnerText);
                                        溫度.Add(xnlD[1].InnerText);
                                    }
                                    if (xnC.Name == "time" && state == "Td")
                                    {
                                        XmlElement xeD = (XmlElement)xnC;
                                        XmlNodeList xnlD = xeD.ChildNodes;
                                        露點溫度.Add(xnlD[1].InnerText);
                                    }
                                    index++; //索引+1 預計總共24筆
                                }
                            }
                        }

                    }

                    for (int i = 0; i < 時間.Count; i++)
                    {
                        weathers.Add(new Weather { 時間 = 時間[i], 溫度 = 溫度[i], 露點溫度 = 露點溫度[i] });
                    }
                    時間.Clear();
                    溫度.Clear();
                    露點溫度.Clear();
                }

                StringBuilder sb = new StringBuilder("<table><tr><th>時間</th><th>溫度</th><th>露點溫度</th></tr>");
                foreach (Weather wt in weathers)
                {
                    sb.Append("<tr> <td>" + wt.時間 + " </td> <td>" + wt.溫度 + " </td> <td>" + wt.露點溫度 + " </td> </tr>");
                }
                sb.Append("</table>");


                test.InnerHtml = sb.ToString();

            }
        }
    }
}

