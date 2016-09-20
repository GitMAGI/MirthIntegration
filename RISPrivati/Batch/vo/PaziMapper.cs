using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class PaziMapper
    {
        public static List<Pazi> DataRowToVO(DataRowCollection rows)
        {
            List<Pazi> pazis = new List<Pazi>();

            foreach (DataRow row in rows)
            {
                pazis.Add(DataRowToVO(row));
            }

            return pazis;
        }

        public static Pazi DataRowToVO(DataRow row)
        {
            Pazi pazi = new Pazi();

            pazi.PAZIIDID = row["PAZIIDID"] != DBNull.Value ? (int?)row["PAZIIDID"] : null;
            pazi.PAZIREPA = row["PAZIREPA"] != DBNull.Value ? (int?)row["PAZIREPA"] : null;
            pazi.PAZINOME = row["PAZINOME"] != DBNull.Value ? (string)row["PAZINOME"] : null;
            pazi.PAZICOGN = row["PAZICOGN"] != DBNull.Value ? (string)row["PAZICOGN"] : null;
            pazi.PAZISESS = row["PAZISESS"] != DBNull.Value ? (string)row["PAZISESS"] : null;
            pazi.PAZICOFI = row["PAZICOFI"] != DBNull.Value ? (string)row["PAZICOFI"] : null;
            pazi.PAZICOPS = row["PAZICOPS"] != DBNull.Value ? (string)row["PAZICOPS"] : null;
            pazi.PAZICAPS = row["PAZICAPS"] != DBNull.Value ? (string)row["PAZICAPS"] : null;
            pazi.PAZIDATA = row["PAZIDATA"] != DBNull.Value ? (DateTime?)row["PAZIDATA"] : null;
            pazi.PAZITELE = row["PAZITELE"] != DBNull.Value ? (string)row["PAZITELE"] : null;
            pazi.PAZITEL2 = row["PAZITEL2"] != DBNull.Value ? (string)row["PAZITEL2"] : null;
            pazi.PAZICOMU = row["PAZICOMU"] != DBNull.Value ? (string)row["PAZICOMU"] : null;
            pazi.PAZIPROV = row["PAZIPROV"] != DBNull.Value ? (string)row["PAZIPROV"] : null;
            pazi.PAZIVIAA = row["PAZIVIAA"] != DBNull.Value ? (string)row["PAZIVIAA"] : null;
            pazi.PAZIBRR1 = row["PAZIBRR1"] != DBNull.Value ? (int?)row["PAZIBRR1"] : null;
            pazi.PAZIBRR2 = row["PAZIBRR2"] != DBNull.Value ? (int?)row["PAZIBRR2"] : null;
            pazi.PAZIBRR3 = row["PAZIBRR3"] != DBNull.Value ? (int?)row["PAZIBRR3"] : null;
            pazi.PAZIBRR4 = row["PAZIBRR4"] != DBNull.Value ? (int?)row["PAZIBRR4"] : null;
            pazi.PAZIBRR5 = row["PAZIBRR5"] != DBNull.Value ? (int?)row["PAZIBRR5"] : null;
            pazi.PAZIREGI = row["PAZIREGI"] != DBNull.Value ? (string)row["PAZIREGI"] : null;
            pazi.PAZIMEDI = row["PAZIMEDI"] != DBNull.Value ? (string)row["PAZIMEDI"] : null;
            pazi.PAZIASLL = row["PAZIASLL"] != DBNull.Value ? (string)row["PAZIASLL"] : null;
            pazi.PAZISTCI = row["PAZISTCI"] != DBNull.Value ? (string)row["PAZISTCI"] : null;
            pazi.PAZICOND = row["PAZICOND"] != DBNull.Value ? (string)row["PAZICOND"] : null;
            pazi.PAZIPOSI = row["PAZIPOSI"] != DBNull.Value ? (string)row["PAZIPOSI"] : null;
            pazi.PAZIRAMO = row["PAZIRAMO"] != DBNull.Value ? (string)row["PAZIRAMO"] : null;
            pazi.PAZITITO = row["PAZITITO"] != DBNull.Value ? (string)row["PAZITITO"] : null;
            pazi.PAZICAPP = row["PAZICAPP"] != DBNull.Value ? (string)row["PAZICAPP"] : null;
            pazi.PAZICTNZ = row["PAZICTNZ"] != DBNull.Value ? (string)row["PAZICTNZ"] : null;
            pazi.PAZIRESI = row["PAZIRESI"] != DBNull.Value ? (string)row["PAZIRESI"] : null;
            pazi.PAZICIRC = row["PAZICIRC"] != DBNull.Value ? (string)row["PAZICIRC"] : null;
            pazi.PAZIMADR = row["PAZIMADR"] != DBNull.Value ? (int?)row["PAZIMADR"] : null;
            pazi.PAZIRELO = row["PAZIRELO"] != DBNull.Value ? (DateTime?)row["PAZIRELO"] : null;
            pazi.PAZIISTI = row["PAZIISTI"] != DBNull.Value ? (int?)row["PAZIISTI"] : null;
            pazi.PAZIISCA = row["PAZIISCA"] != DBNull.Value ? (string)row["PAZIISCA"] : null;
            pazi.HLTPROCESS = row["HLTPROCESS"] != DBNull.Value ? (int?)row["HLTPROCESS"] : null;
            pazi.pazieni = row["pazieni"] != DBNull.Value ? (string)row["pazieni"] : null;
            pazi.paziteam = row["paziteam"] != DBNull.Value ? (string)row["paziteam"] : null;
            pazi.codicepowerlab = row["codicepowerlab"] != DBNull.Value ? (string)row["codicepowerlab"] : null;
            pazi.pazimecd = row["pazimecd"] != DBNull.Value ? (string)row["pazimecd"] : null;
            pazi.pazisorg = row["pazisorg"] != DBNull.Value ? (string)row["pazisorg"] : null;
            pazi.paziidext = row["paziidext"] != DBNull.Value ? (string)row["paziidext"] : null;
            pazi.paziturn = row["paziturn"] != DBNull.Value ? (int?)row["paziturn"] : null;
            pazi.paziperi = row["paziperi"] != DBNull.Value ? (int?)row["paziperi"] : null;
            pazi.pazimaster = row["pazimaster"] != DBNull.Value ? (int?)row["pazimaster"] : null;
            pazi.pazimergedata = row["pazimergedata"] != DBNull.Value ? (DateTime?)row["pazimergedata"] : null;
            pazi.pazidistr = row["pazidistr"] != DBNull.Value ? (string)row["pazidistr"] : null;
            pazi.paziaslcode = row["paziaslcode"] != DBNull.Value ? (string)row["paziaslcode"] : null;

            return pazi;
        }
    }
}
