using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace K3Import
{
    public class ReadK3XML
    {
        public ColumnInfoIterator ciiterator;

        private string _xmlfilename;
        public ReadK3XML(string filename) 
        {
            _xmlfilename = filename;
        }

        public DataTable ReadToTable(string businessname)
        {
            //DataTable table = ReadToTable(businessname, delegate
            //{

            //});
            //table.TableName = businessname;
            return ReadToTable(businessname, delegate {
                return GetXmlNodeListByXpath(_xmlfilename, "//xml//rs:data//rs:insert//z:row");
            });
                      
        }
        private DataTable ReadToTable(string businessname, Func<XmlNodeList> provider)
        {            
            using (XmlNodeList xmlnodelist = provider())
            {
                DataTable dt = PrepairTableColumn();
                //String[] columnvalue = new String[dt.Columns.Count]();
                //int limit = 100;

                foreach (XmlNode xmlnode in xmlnodelist)
                {
                    if (xmlnode.Attributes["c6"].Value.IndexOf(businessname) > 0)
                    {
                        DataRow dr = dt.NewRow();
                        foreach (ColunmInfo ci in ciiterator)
                        {
                            dr[ci.ColumnName] = xmlnode.Attributes[ci.ColumnKey] == null ? "" : xmlnode.Attributes[ci.ColumnKey].Value;
                        }
                        dt.Rows.Add(dr);
                        //limit--;
                    }
                    //if (limit == 0) { break; }
                }
                return dt;
            }
            //return null;
        }

        private DataTable PrepairTableColumn()
        {
            return PrepairTableColumn(_xmlfilename);
        }
        private DataTable PrepairTableColumn(string xmlfilename)
        {
            DataTable dt = new DataTable();
            ciiterator = GetTableColumnInfo(xmlfilename, delegate {
                return GetXmlNodeListByXpath(xmlfilename, "//xml//s:Schema//s:ElementType//s:AttributeType");
            });

            foreach (ColunmInfo ci in ciiterator)
            {
                dt.Columns.Add(ci.ColumnName);
            }
            return dt;
        }
        private ColumnInfoIterator GetTableColumnInfo(string xmlfilename,Func<XmlNodeList> provider)
        {
            //DataTable dt = new DataTable();
            //datacol
            //DataTable dt = new DataTable();
            //using (var nodelist = XMLHelper.GetXmlNodeListByXpath(xmlfilename, "//xml//s:Schema//s:ElementType"))
            using (var nodelist=provider())
            {
                //DataColumn[] colunms = new DataColumn()[nodelist.Count];
                ColumnInfoIterator cii = new ColumnInfoIterator();
                foreach (XmlNode node in nodelist)
                {
                    if (node.LocalName == "AttributeType")
                    {
                        cii.Add(new ColunmInfo { ColumnKey = node.Attributes["name"].Value, ColumnName = node.Attributes["rs:name"].Value });
                        //dt.Columns.Add(node["name"].Value + node["rs:name"].Value);
                        //dt.Columns.a
                    }
                }
                return cii;
            }
           // return dt;
        }

        private XmlNodeList GetXmlNodeListByXpath(string filename, string xpath)
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(filename);

            XmlNamespaceManager xmlns = new XmlNamespaceManager(xd.NameTable);            

            XmlNode xn =xd.SelectSingleNode("xml");

            foreach (XmlAttribute arr in xn.Attributes)
            {
                xmlns.AddNamespace(arr.LocalName, arr.Value);
            }


            return xn.SelectNodes(xpath, xmlns);
        }
    }
}
