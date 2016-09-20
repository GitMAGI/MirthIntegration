using Batch.vo;
using System;
using System.Collections.Generic;
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
    }
}
