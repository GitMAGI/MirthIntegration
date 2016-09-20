using Batch.vo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial class DAL
    {
        public List<RISMirth> GetRISMirthAll()
        {
            List<RISMirth> data = null;

            string tabName = null;

            DataTable data_ = DBSQL.SelectOperation(GRConnectionString, tabName);

            log.Info(string.Format("Query Executed! Retrieved {0} records!", data_.Rows.Count));

            if (data_ != null)
            {
                if (data_.Rows.Count == 1)
                {
                    data = RISMirthMapper.DataRowToVO(data_.Rows);
                    log.Info(string.Format("{0} Records mapped to {1}", data.Count, typeof(RISMirth).ToString()));
                }
            }

            return data;
        }
    }
}
