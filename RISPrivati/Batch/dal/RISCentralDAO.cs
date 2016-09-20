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
        public List<RISCentral> GetRISCentralAll()
        {
            List<RISCentral> data = null;

            string tabName = "VW_EsamiRadiologiciRISCasilino";

            DataTable data_ = DBSQL.SelectOperation(HltDesktopConnectionString, tabName);

            log.Info(string.Format("Query Executed! Retrieved {0} records!", data_.Rows.Count));

            if (data_ != null)
            {
                if (data_.Rows.Count == 1)
                {
                    data = RISCentralMapper.DataRowToVO(data_.Rows);
                    log.Info(string.Format("{0} Records mapped to {1}", data.Count, typeof(RISCentral).ToString()));
                }
            }

            return data;
        }
    
        public List<RISCentral> GetRISCentralNotIn(List<Guid> list){
            List<RISCentral> data = null;

            string tabName = "VW_EsamiRadiologiciRISCasilino";

            string query = "SELECT * FROM " + tabName + " WHERE IDRichiesta not in (" +
                string.Join(", ", list.ToArray())
                + ")";

            DataTable data_ = DBSQL.ExecuteQuery(HltDesktopConnectionString, query);

            log.Info(string.Format("Query Executed! Retrieved {0} records!", data_.Rows.Count));

            if (data_ != null)
            {
                if (data_.Rows.Count == 1)
                {
                    data = RISCentralMapper.DataRowToVO(data_.Rows);
                    log.Info(string.Format("{0} Records mapped to {1}", data.Count, typeof(RISCentral).ToString()));
                }
            }

            return data;
        }
    }
}
