using Batch.vo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batch
{
    class Program
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            string provenienza = "RISPrivati";

            log.Info("Starting procedure...");

            DataAccessLayer.DAL dal = new DataAccessLayer.DAL();

            try
            {
                //1. Lettura Tabella dove presente lo stato della richiesta
                //List<RISMirth> rissM = dal.GetRISMirthAll();
                //List<Guid> mirthg = rissM.Select(p=>p.)
                //2. Lettura Vista Gro -- select * from [eurosanita].[VW_EsamiRadiologiciRISCasilino] -- where IDRichiesta non è preso in carico

                //3. Verifica Anagrafica Paziente
                List<RISCentral> rissC = new List<RISCentral>(); // Fittizio
                foreach (RISCentral risC in rissC)
                {
                    string name = risC.Nome;
                    string surname = risC.Cognome;
                    string cf = risC.CodiceFiscale;
                    DateTime? birth = risC.DataNascita;
                    if(name!=null && surname!=null && birth!=null && cf!= null){
                        Pazi paz = dal.GetPazienteByFilter(name, surname, cf, birth.Value);
                        if (paz == null)
                        {
                            //4. In caso non esista, creare il pazeinte in GR
                            Pazi nuovoPaziente = new Pazi();
                            nuovoPaziente.PAZINOME = risC.Nome;
                            nuovoPaziente.PAZICOGN = risC.Cognome;
                            nuovoPaziente.PAZICOFI = risC.CodiceFiscale;
                            nuovoPaziente.PAZIDATA = risC.DataNascita;
                            int written = dal.InsertPaziente(nuovoPaziente);
                            if (written != 1)
                            {
                                //ERROR! FATAL
                                continue;
                            }

                            //5. Creare il mappning in IdentityMapping
                            IdentityMapping nuovoMapping = new IdentityMapping();
                            nuovoMapping.idext = risC.IdPaziente.ToString();
                            nuovoMapping.provenienza = provenienza;
                            written = dal.InsertIdentityMapping(nuovoMapping);
                            if (written != 1)
                            {
                                //ERROR! FATAL
                                continue;
                            }
                        }
                        else
                        {
                            // L'anagrafica esiste in GR. Bisogna controllare se esite in IdentintyMapping, se no bisogna crearla
                            int IDPazienteCentral = risC.IdPaziente.Value;
                            int IDPazienteGR = paz.PAZIIDID.Value;
                            IdentityMapping mapping = dal.GetIdentityMappingByPK(IDPazienteGR, IDPazienteCentral.ToString());
                            if(mapping == null)
                            {
                                IdentityMapping nuovoMapping = new IdentityMapping();
                                nuovoMapping.idext = IDPazienteCentral.ToString();
                                nuovoMapping.provenienza = provenienza;
                                int written = dal.InsertIdentityMapping(nuovoMapping);
                                if(written != 1)
                                {
                                    //ERROR! FATAL
                                    continue;
                                }
                            }
                        }
                    }

                }                
                
            }
            catch (Exception)
            {

            }            

            tw.Stop();
            log.Info(string.Format("Procedure Completed! Elapsed time {0}", GeneralPurposeLib.LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
        }
    }
}
