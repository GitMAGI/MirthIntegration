using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRISPrivati
{
    class Program
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["GR"].ConnectionString;

            log.Info(string.Format("Setting up ..."));

            string query = "";
            log.Info("Esecuzione Job1: Popolamento tabella GR.dbo.RISPrivatiRichieste.");
            // Esecuzione Job 1            
            query = "[GR].[dbo].[RISPrivatiRichieste_Job1]";
            try
            {
                DataTable resJob1 = DataAccessLayer.DBSQL.ExecuteQuery(connStr, query, true);
                log.Info(string.Format("Esecuzione Job1: Completato con successo!"));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Esecuzione Job1: Errore durante l'esecuzione. Errore:{0}", e.Message));
            }
            

            log.Info("Esecuzione Job2: Allineamento Anagrafica GR [PAZI] e Mapping delle identità ");
            // Esecuzione Job 2
            query = "[GR].[dbo].[RISPrivatiRichieste_Job2]";
            try
            {
                DataTable resJob2 = DataAccessLayer.DBSQL.ExecuteQuery(connStr, query, true);
                log.Info(string.Format("Esecuzione Job2: Completato con successo!"));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Esecuzione Job2: Errore durante l'esecuzione. Errore:{0}", e.Message));
            }

            
            // Esecuzione Job 2.1 Creazione Episodi e Associazione Episodio in RISPrivatiRichieste
            log.Info("Esecuzione Job2.1: Creazione Episodi e Associazione Episodio in RISPrivatiRichieste.");
            query = "select R.RicIDID, R.PazIDID_hlt, IM.idunico from [GR].[dbo].[RISPrivatiRichieste] AS R inner join [GR].[dbo].[IdentityMapping] AS IM ON IM.idext = R.PazIDID_hlt where R.stato = 1";
            try
            {
                DataTable richs = DataAccessLayer.DBSQL.ExecuteQuery(connStr, query);

                foreach (DataRow rich in richs.Rows)
                {
                    int IDRichiesta = rich["RICIdid"] != null ? (int)rich["RICIdid"] : 0;
                    int IdPazGR = rich["idunico"]  != null ? (int)rich["idunico"] : 0;
                    int IdPazHLT = rich["PazIDID_hlt"] != null ? (int)rich["PazIDID_hlt"] : 0;

                    string insertquery = string.Format("insert into [GR].[dbo].[EPIS] (EPISPAZI, EPISDAIN, EPISTIPO, EPISCHIU, EPISREPA) VALUES ({0},' {1}', {2}, {3}, {4}); select @@identity AS episidid;", IdPazGR, DateTime.Now.ToString("yyyy-MM-dd"), 9, 1, 2);
                    log.Info(string.Format("Esecuzione inserimento: Avvio ..."));
                    DataTable insertJob = DataAccessLayer.DBSQL.ExecuteQuery(connStr, insertquery, false);
                    log.Info(string.Format("Esecuzione inserimento: Completato!"));

                    var IDEpisGR = insertJob.Rows[0]["episidid"] != null ? insertJob.Rows[0]["episidid"] : 0;

                    string updatequery = string.Format("update [GR].[dbo].[RISPrivatiRichieste] set [EpisIDID_GR] = {0} where [RicIDID] = {1}", IDEpisGR, IDRichiesta);
                    log.Info(string.Format("Esecuzione aggiornamento: Avvio ..."));
                    int rows = DataAccessLayer.DBSQL.ExecuteNonQuery(connStr, updatequery);
                    log.Info(string.Format("Esecuzione inserimento: Completato! Aggiornate {0} righe!", rows));

                    log.Info(string.Format("Esecuzione Job2.1: Completato con successo!"));
                }
            }
            catch(Exception e)
            {
                log.Error(string.Format("Esecuzione Job2.1: Errore durante l'esecuzione. Errore:{0}", e.Message));
            }           

            

            log.Info("Esecuzione Job3: Setting stato a 2");
            // Esecuzione Job 3
            query = "[GR].[dbo].[RISPrivatiRichieste_Job3]";
            try
            {
                DataTable resJob3 = DataAccessLayer.DBSQL.ExecuteQuery(connStr, query, true);
                log.Info(string.Format("Esecuzione Job3: Completato con successo!"));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Esecuzione Job3: Errore durante l'esecuzione. Errore:{0}", e.Message));
            }


            tw.Stop();
            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
        }
    }
}
