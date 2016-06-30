using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using LxbXml;

namespace K3Import
{
    public class ReadK3XML
    {
        private string _xmlfilename;
        public ReadK3XML(string filename) 
        {
            _xmlfilename = filename;
        }

        public DataTable ReadToTable()
        {
            try
            {
                //var columnNames = XMLHelper.GetXmlNodeListByXpath(_xmlfilename,"//");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return null;
        }
        private void ReadToTable(out DataTable table)
        {
            table = new DataTable();
            //table
        }
        private DataRow prepairTableRow()
        {
            return null;
        }
        private DataRow prepairTableRow()
        {
            //DataRowBuilder drb = new DataRowBuilder();            
            DataRow dr = new DataRow();


        }

        private DataTable PrepairTableColumn()
        {
            return PrepairTableColumn(_xmlfilename);
        } 
        private DataTable PrepairTableColumn(string xmlfilename)
        {
            //DataTable dt = new DataTable();
            //datacol
            DataTable dt = new DataTable();
            var nodelist = XMLHelper.GetXmlNodeListByXpath(xmlfilename, "//s:Schema//s:ElementType");
            //DataColumn[] colunms = new DataColumn()[nodelist.Count];
            foreach (XmlNode node in nodelist)
            {
                if (node.Name == "s:AttributeType")
                {
                    dt.Columns.Add(node["name"].Value + node["rs:name"].Value);
                    //dt.Columns.a
                }
            }
            return dt;
        }
        
        //private DataTable
    }
}
