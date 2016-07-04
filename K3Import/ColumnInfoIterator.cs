using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K3Import
{
    public class ColumnInfoIterator:IEnumerable<ColunmInfo>
    {
        List<ColunmInfo> columns = new List<ColunmInfo>();
        public void Add(ColunmInfo columninfo)
        {
            columns.Add(columninfo);
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public IEnumerator<ColunmInfo> GetEnumerator()
        {
            foreach (ColunmInfo ci in columns)
            {
                yield return ci;
            }
        }

        public ColunmInfo GetColumnInfoByKey(string key)
        {
            foreach (ColunmInfo ci in columns)
            {
                if (ci.ColumnKey == key)
                {
                    return ci;
                }
            }
            return null;
        }

    }
}
