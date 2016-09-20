using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class IdentityMappingMapper
    {
        public static List<IdentityMapping> DataRowToVO(DataRowCollection rows)
        {
            List<IdentityMapping> ids = new List<IdentityMapping>();

            foreach (DataRow row in rows)
            {
                ids.Add(DataRowToVO(row));
            }

            return ids;
        }

        public static IdentityMapping DataRowToVO(DataRow row)
        {
            IdentityMapping id = new IdentityMapping();

            id.idunico = row["idunico"] != DBNull.Value ? (int?)row["idunico"] : null;
            id.idext = row["idext"] != DBNull.Value ? (string)row["idext"] : null;
            id.provenienza = row["provenienza"] != DBNull.Value ? (string)row["provenienza"] : null;

            return id;
        }
    }
}
