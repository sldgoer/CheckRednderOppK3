using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace JMGJJFactByLing
{
    public class jmgjjfact
    {
        LinqToJMGJJFactDataContext jmgjjcontext;
        public jmgjjfact(string connectionstring)
        {
            jmgjjcontext = new LinqToJMGJJFactDataContext(connectionstring);
        }

        public DataTable GetRenderTable(DateTime? date)
        {
            DataTable dt = new DataTable();

            var render = (from r in jmgjjcontext.t_account_renders
                          //where SqlMethods.DateDiffMonth(r.pay_month,paymonth)==0
                          join a in jmgjjcontext.t_accounts
                          on r.a_id equals a.Id
                              //select new { name=a.Name,account=a.Account,rendermoney=r.u_money+r.i_money,payMonth=r.pay_month}
                          into temp
                          from res in temp
                          where SqlMethods.DateDiffMonth(r.date, date) == 0 && (r.i_money + r.u_money) > 0
                          select new { name = res.Name, account = res.Account, rendermoney = res.u_money + res.i_money, date = r.date }).ToList();
            
            dt.Columns.Add("name");
            dt.Columns.Add("account");
            dt.Columns.Add("rendermoney");
            dt.Columns.Add("date");

            foreach (var o in render)
            {
                DataRow dr =dt.NewRow();
                dr["name"]=o.name.ToString();
                dr["account"]=o.account.ToString();
                dr["rendermoney"] = Math.Round(o.rendermoney.GetValueOrDefault(0), 2, MidpointRounding.AwayFromZero);//Math.Round(decimal.Parse(o.rendermoney.ToString() == null ? "0.00" : o.rendermoney.ToString()), 2);
                dr["date"]=DateTime.Parse(o.date.ToString());

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public LinqToJMGJJFactDataContext GetContext()
        {
            return jmgjjcontext;
        }
    }
}
