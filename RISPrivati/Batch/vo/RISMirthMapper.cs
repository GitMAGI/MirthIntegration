using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class RISMirthMapper
    {
        public static List<RISMirth> DataRowToVO(DataRowCollection rows)
        {
            List<RISMirth> riss = new List<RISMirth>();

            foreach (DataRow row in rows)
            {
                riss.Add(DataRowToVO(row));
            }

            return riss;
        }

        public static RISMirth DataRowToVO(DataRow row)
        {
            RISMirth ris = new RISMirth();

            return ris;
        }
    }
}
