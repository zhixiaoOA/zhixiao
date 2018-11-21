using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ZX.DAL
{
    public class Pmts
    {
        public List<SqlParameter> ListPmts = new List<SqlParameter>();
        public void ClearPmts()
        {
            ListPmts.Clear();
        }
        public void Add(string key, object value, System.Data.ParameterDirection direction = System.Data.ParameterDirection.Input)
        {
            ListPmts.Add(new SqlParameter(key, value));
            if (direction != System.Data.ParameterDirection.Input)
            {
                ListPmts[ListPmts.Count - 1].Direction = direction;
            }
        }

        public SqlParameter[] ToArray()
        {
            return ListPmts.ToArray();
        }
    }
}
