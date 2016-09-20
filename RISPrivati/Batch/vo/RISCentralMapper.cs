using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class RISCentralMapper
    {
        public static List<RISCentral> DataRowToVO(DataRowCollection rows)
        {
            List<RISCentral> riss = new List<RISCentral>();

            foreach (DataRow row in rows)
            {
                riss.Add(DataRowToVO(row));
            }

            return riss;
        }

        public static RISCentral DataRowToVO(DataRow row)
        {
            RISCentral ris = new RISCentral();

            ris.IdRichiesta = row["IdRichiesta"] != DBNull.Value ? (Guid?)row["IdRichiesta"] : null;
            ris.IdPaziente = row["IdPaziente"] != DBNull.Value ? (int?)row["IdPaziente"] : null;
            ris.IdEpisodio = row["IdEpisodio"] != DBNull.Value ? (int?)row["IdEpisodio"] : null;
            ris.CodConvenzione = row["CodConvenzione"] != DBNull.Value ? (string)row["CodConvenzione"] : null;
            ris.CodComuneNascita = row["CodComuneNascita"] != DBNull.Value ? (string)row["CodComuneNascita"] : null;
            ris.DescComuneNascita = row["DescComuneNascita"] != DBNull.Value ? (string)row["DescComuneNascita"] : null;
            ris.Nome = row["Nome"] != DBNull.Value ? (string)row["Nome"] : null;
            ris.Cognome = row["Cognome"] != DBNull.Value ? (string)row["Cognome"] : null;
            ris.DataNascita = row["DataNascita"] != DBNull.Value ? (DateTime?)row["DataNascita"] : null;
            ris.CodiceFiscale = row["CodiceFiscale"] != DBNull.Value ? (string)row["CodiceFiscale"] : null;
            ris.IdEsame = row["IdEsame"] != DBNull.Value ? (int?)row["IdEsame"] : null;
            ris.CodEsame = row["CodEsame"] != DBNull.Value ? (string)row["CodEsame"] : null;
            ris.DescrizioneEsame = row["DescrizioneEsame"] != DBNull.Value ? (string)row["DescrizioneEsame"] : null;
            ris.DataPrenotazione = row["DataPrenotazione"] != DBNull.Value ? (DateTime?)row["DataPrenotazione"] : null;
            
            return ris;
        }
    }
}
