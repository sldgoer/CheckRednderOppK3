using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using K3Import;
using System.Data;
using LxbXml;

namespace CheckRenderOppK3
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadK3XML rk3xml = new ReadK3XML(@"D:\201412.xml");

            DataTable dt = rk3xml.ReadToTable("汇缴");

            Console.WriteLine("Total rows count:{0}", dt.Rows.Count);
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                Console.Write(dt.Columns[c].Caption + "|");
            }
            Console.WriteLine("");
            for (int r = 0; r < 100; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    Console.Write(dt.Rows[r][c] + "|");

                }
                Console.WriteLine("");

            }
            Console.ReadKey();

            //for (int i = 0; i <= 100; i++)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    Console.Write("{0}%", i);
            //    Thread.Sleep(200);
            //}
                
        }
    }
}
