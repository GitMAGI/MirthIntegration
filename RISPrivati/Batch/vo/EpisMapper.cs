using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class EpisMapper
    {
        public static List<Epis> DataRowToVO(DataRowCollection rows)
        {
            List<Epis> episs = new List<Epis>();

            foreach (DataRow row in rows)
            {
                episs.Add(DataRowToVO(row));
            }

            return episs;
        }

        public static Epis DataRowToVO(DataRow row)
        {
            Epis epis = new Epis();

            epis.EPISIDID = row["EPISIDID"] != DBNull.Value ? (int?)row["EPISIDID"] : null;
            epis.EPISPAZI = row["EPISPAZI"] != DBNull.Value ? (int?)row["EPISPAZI"] : null;
            epis.EPISTIPO = row["EPISTIPO"] != DBNull.Value ? (int?)row["EPISTIPO"] : null;
            epis.EPISDAIN = row["EPISDAIN"] != DBNull.Value ? (DateTime?)row["EPISDAIN"] : null;
            epis.EPISDAFI = row["EPISDAFI"] != DBNull.Value ? (DateTime?)row["EPISDAFI"] : null;
            epis.EPISCHIU = row["EPISCHIU"] != DBNull.Value ? (bool?)row["EPISCHIU"] : null;
            epis.EPISREPA = row["EPISREPA"] != DBNull.Value ? (int?)row["EPISREPA"] : null;
            epis.EPISREAP = row["EPISREAP"] != DBNull.Value ? (int?)row["EPISREAP"] : null;
            epis.EPISLETT = row["EPISLETT"] != DBNull.Value ? (int?)row["EPISLETT"] : null;
            epis.EPISPREC = row["EPISPREC"] != DBNull.Value ? (int?)row["EPISPREC"] : null;
            epis.EPISCART = row["EPISCART"] != DBNull.Value ? (int?)row["EPISCART"] : null;
            epis.EPISISTI = row["EPISISTI"] != DBNull.Value ? (int?)row["EPISISTI"] : null;
            epis.EPISISCA = row["EPISISCA"] != DBNull.Value ? (string)row["EPISISCA"] : null;
            epis.EPISCKCK = row["EPISCKCK"] != DBNull.Value ? (int?)row["EPISCKCK"] : null;
            epis.EPISSCRE = row["EPISSCRE"] != DBNull.Value ? (bool?)row["EPISSCRE"] : null;
            epis.EPISSCDT = row["EPISSCDT"] != DBNull.Value ? (DateTime?)row["EPISSCDT"] : null;
            epis.EPISSCPE = row["EPISSCPE"] != DBNull.Value ? (string)row["EPISSCPE"] : null;
            epis.EPISSnot = row["EPISSnot"] != DBNull.Value ? (string)row["EPISSnot"] : null;
            epis.SEND_TO_RIS = row["SEND_TO_RIS"] != DBNull.Value ? (int?)row["SEND_TO_RIS"] : null;
            epis.EPISOLDPAZI = row["EPISOLDPAZI"] != DBNull.Value ? (int?)row["EPISOLDPAZI"] : null;
            epis.episintens = row["episintens"] != DBNull.Value ? (int?)row["episintens"] : null;
            epis.EPISDATAINTENS = row["EPISDATAINTENS"] != DBNull.Value ? (DateTime?)row["EPISDATAINTENS"] : null;
            epis.EPISUTENINTES = row["EPISUTENINTES"] != DBNull.Value ? (string)row["EPISUTENINTES"] : null;
            
            return epis;
        }
    }
}
