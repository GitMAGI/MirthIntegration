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
        public int InsertIdentityMapping(IdentityMapping data)
        {
            int affetcedRows = -1;

            string tabName = "IdentityMapping";

            affetcedRows = DBSQL.InsertOperation(GRConnectionString, tabName, data);

            return affetcedRows;
        }

        public IdentityMapping GetIdentityMappingByPK(int idGR, string idCentral)
        {
            IdentityMapping data = null;

            string tabName = "IdentityMapping";

            Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>();
            conditions.Add("idGR", new DBSQL.QueryCondition
            {
                Key = "idunico",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.And,
                Value = idGR
            });
            conditions.Add("idCentral", new DBSQL.QueryCondition
            {
                Key = "idext",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.None,
                Value = idCentral
            });

            DataTable data_ = DBSQL.SelectOperation(GRConnectionString, tabName, conditions);

            log.Info(string.Format("Query Executed! Retrieved {0} records!", data_.Rows.Count));

            if (data_ != null)
            {
                if (data_.Rows.Count == 1)
                {
                    data = IdentityMappingMapper.DataRowToVO(data_.Rows[0]);
                    log.Info(string.Format("{0} Records mapped to {1}", 1, typeof(IdentityMapping).ToString()));
                }
            }

            return data;
        }
    }
}
