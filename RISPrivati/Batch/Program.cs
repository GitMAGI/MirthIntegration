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

            log.Info("Starting procedure...");

            DataAccessLayer.DAL dal = new DataAccessLayer.DAL();

            try
            {
                //1. Lettura Tabella dove presente lo stato della richiesta
                //List<RISMirth> rissM = dal.GetRISMirthAll();
                //List<Guid> mirthg = rissM.Select(p=>p.)
                //2. Lettura Vista Gro -- select * from [eurosanita].[VW_EsamiRadiologiciRISCasilino] -- where IDRichiesta non è preso in carico

                //3. Verifica Anagrafica Paziente
                List<RISCentral> rissC = new List<RISCentral>();
                foreach (RISCentral risC in rissC)
                {
                    string name = risC.Nome;
                    string surname = risC.Cognome;
                    string cf = risC.CodiceFiscale;
                    DateTime? birth = risC.DataNascita;
                    if(name!=null && surname!=null && birth!=null && cf!= null){
                        Pazi paz = dal.GetPazienteByFilter(name, surname, cf, birth.Value);
                        if (paz != null)
                        {
                            //4. In caso non esista, creare il pazeinte in GR
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
