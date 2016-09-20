using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch.vo
{
    public class RISCentral
    {
        public Guid? IdRichiesta { get; set; }
        public int? IdPaziente { get; set; }
        public int? IdEpisodio { get; set; }
        public string CodConvenzione { get; set; }
        public string CodComuneNascita { get; set; }
        public string DescComuneNascita { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime? DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public int? IdEsame { get; set; }
        public string CodEsame { get; set; }
        public string DescrizioneEsame { get; set; }
        public DateTime? DataPrenotazione { get; set; }

    }
}
