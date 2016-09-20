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
        public int InsertPaziente(Pazi data){
            int affetcedRows = -1;

            string tabName = "PAZI";

            affetcedRows = DBSQL.InsertOperation(GRConnectionString, tabName, data);

            return affetcedRows;
        }

        public Pazi GetPazienteByFilter(string nome, string cognome, string cf, DateTime dataNascita)
        {
            Pazi data = null;

            string tabName = "PAZI";

            Dictionary<string, DBSQL.QueryCondition> conditions = new Dictionary<string, DBSQL.QueryCondition>();
            conditions.Add("nome", new DBSQL.QueryCondition{
                Key = "PAZINOME",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.And,
                Value = nome
            });
            conditions.Add("cognome", new DBSQL.QueryCondition
            {
                Key = "PAZICOGN",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.And,
                Value = cognome
            });
            conditions.Add("codicefiscale", new DBSQL.QueryCondition
            {
                Key = "COFI",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.And,
                Value = cf
            });
            conditions.Add("datanascita", new DBSQL.QueryCondition
            {
                Key = "DATA",
                Op = DBSQL.Op.Equal,
                Conj = DBSQL.Conj.None,
                Value = dataNascita
            });

            DataTable data_ = DBSQL.SelectOperation(GRConnectionString, tabName, conditions);

            log.Info(string.Format("Query Executed! Retrieved {0} records!", data_.Rows.Count));

            if (data_ != null)
            {
                if (data_.Rows.Count == 1)
                {
                    data = PaziMapper.DataRowToVO(data_.Rows[0]);
                    log.Info(string.Format("{0} Records mapped to {1}", 1, typeof(Pazi).ToString()));
                }
            }

            return data;
        }
    }
}
