using GeneralPurposeLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    
    public class DBSQL
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static public DataTable ExecuteQuery(string connectionString, string sql, bool? sp = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            //string connectionString = "";
            DataTable dataTable = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                if (sp != null)
                {
                    if (sp.Value)
                        cmd.CommandType = CommandType.StoredProcedure;
                }
                log.Info(string.Format("Opening Connection on '{0}' ...", connectionString));
                connection.Open();
                log.Info(string.Format("Query: {0}", sql));
                log.Info(string.Format("Query execution starting ..."));

                using (var reader = cmd.ExecuteReader())
                {
                    dataTable = new DataTable();
                    dataTable.Load(reader);
                }
                log.Info(string.Format("Query executed! Result count: {0}", dataTable.Rows.Count));
                connection.Close();
                log.Info("Connection Closed!");
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return dataTable;
        }
        static public DataTable ExecuteQueryWithParams(string connectionString, string sql, Dictionary<string, object> atts, bool? sp = null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            //string connectionString = "";
            DataTable dataTable = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                if (sp != null)
                {
                    if (sp.Value)
                        cmd.CommandType = CommandType.StoredProcedure;
                }
                foreach (KeyValuePair<string, object> entry in atts)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
                log.Info(string.Format("Opening Connection on '{0}' ...", connectionString));
                connection.Open();
                log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                log.Info(string.Format("Query execution starting ..."));
                using (var reader = cmd.ExecuteReader())
                {
                    dataTable = new DataTable();
                    dataTable.Load(reader);
                }
                log.Info(string.Format("Query executed! Result count: {0}", dataTable.Rows.Count));
                connection.Close();
                log.Info("Connection Closed!");

            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return dataTable;
        }

        static public int ExecuteNonQuery(string connectionString, string sql)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            int result = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                log.Info(string.Format("Opening Connection on '{0}' ...", connectionString));
                connection.Open();
                log.Info(string.Format("Query: {0}", sql));
                log.Info(string.Format("Query execution starting ..."));
                result = cmd.ExecuteNonQuery();
                log.Info(string.Format("Query executed! Result count: {0}", result));
                connection.Close();
                log.Info("Connection Closed!");
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }
        static public int ExecuteNonQueryWithParams(string connectionString, string sql, Dictionary<string, object> atts)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            log.Info(string.Format("Setting up ..."));
            int result = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {                
                foreach (KeyValuePair<string, object> entry in atts)
                {
                    cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                }
                log.Info(string.Format("Opening Connection on '{0}' ...", connectionString));
                connection.Open();
                log.Info(string.Format("Query: {0}", LibString.SQLCommand2String(cmd)));
                log.Info(string.Format("Query execution starting ..."));
                result = cmd.ExecuteNonQuery();
                log.Info(string.Format("Query executed! Result count: {0}", result));
                connection.Close();
                log.Info("Connection Closed!");
            }

            tw.Stop();

            log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));

            return result;
        }

        public static class Op
        {
            readonly static public string Lower = " < ";
            readonly static public string Greater = " > ";
            readonly static public string LowerEqual = " <= ";
            readonly static public string GreaterEqual = " >= ";
            readonly static public string Equal = " = ";
            readonly static public string NotEqual = " <> ";
            readonly static public string Is = " is ";
            readonly static public string IsNot = " is not ";
            readonly static public string Like = " like ";
        };
        public static class Conj
        {
            readonly static public string And = "and";
            readonly static public string Or = " or ";
            readonly static public string None = "";
        };
        public struct QueryCondition
        {
            public string Key;
            public string Op;
            public object Value;
            public string Conj;
        }
        
        static public DataTable SelectOperation(string connectionString, string tabName, Dictionary<string, QueryCondition> conditions=null)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            DataTable data = null;

            try
            {
                if (conditions != null)
                {
                    string query = "SELECT * FROM " + tabName + " WHERE " +
                        string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                    Dictionary<string, object> pars = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                    {
                        pars[entry.Key] = entry.Value.Value;
                    }

                    log.Info(string.Format("Query: {0}", query));
                    log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                    data = DataAccessLayer.DBSQL.ExecuteQueryWithParams(connectionString, query, pars);
                }
                else
                {
                    string query = "SELECT * FROM " + tabName;

                    log.Info(string.Format("Query: {0}", query));                    

                    data = DataAccessLayer.DBSQL.ExecuteQuery(connectionString, query);
                }
                    

                log.Info(string.Format("Query Executed! Retrieved {0} records!", data.Rows.Count));

                return data;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public int InsertOperation(string connectionString, string tabName, object dataVO)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();

                foreach (var prop in dataVO.GetType().GetProperties())
                {
                    if (prop.GetValue(dataVO, null) != null)
                    {
                        pars[prop.Name] = prop.GetValue(dataVO, null);
                        //Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(chiamata, null));
                    }
                }

                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", pars.Select(x => x.Key).ToArray()) +
                    ") VALUES (" +
                    string.Join(", ", pars.Select(x => "@" + x.Key).ToArray()) +
                    ")";

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = DBSQL.ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }
        }
        static public int MultiInsertOperation(string connectionString, string tabName, List<object> dataVOs)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                List<Dictionary<string, object>> pars_ = new List<Dictionary<string, object>>();

                Dictionary<string, object> into = new Dictionary<string, object>();

                int i = 0;
                foreach (object dataVO in dataVOs)
                {
                    Dictionary<string, object> pars = new Dictionary<string, object>();

                    foreach (var prop in dataVO.GetType().GetProperties())
                    {
                        if (prop.GetValue(dataVO, null) != null)
                        {
                            string keyOfPar = string.Format("{0}{1}", prop.Name, i);
                            pars[keyOfPar] = prop.GetValue(dataVO, null);
                            //Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(chiamata, null));
                            if (i == 0)
                            {
                                into[prop.Name] = prop.GetValue(dataVO, null);
                            }
                        }
                    }
                    i++;
                    pars_.Add(pars);
                }

                string query = "INSERT INTO " + tabName + " (" +
                    string.Join(", ", into.Select(x => x.Key).ToArray()) +
                    ") VALUES ";

                into = null;

                Dictionary<string, object> parss = new Dictionary<string, object>();


                int j = 1;
                foreach (Dictionary<string, object> pars in pars_)
                {
                    query += "(" +
                    string.Join(", ", pars.Select(x => "@" + x.Key).ToArray()) +
                    ")";
                    parss = parss.Union(pars).ToDictionary(k => k.Key, v => v.Value);
                    if (j < pars_.Count)
                        query += ", ";
                    j++;
                }

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", parss.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = DBSQL.ExecuteNonQueryWithParams(connectionString, query, parss);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }                      
        }
        static public int DeleteOperation(string connectionString, string tabName, Dictionary<string, QueryCondition> conditions)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                string query = "DELETE FROM " + tabName + " WHERE " +
                    string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                Dictionary<string, object> pars = new Dictionary<string, object>();
                foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                {
                    pars[entry.Key] = entry.Value.Value;
                }

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = DBSQL.ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));

                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }     
            
        }
        static public int UpdateOperation(string connectionString, string tabName, object dataVO, Dictionary<string, QueryCondition> conditions)
        {
            Stopwatch tw = new Stopwatch();
            tw.Start();

            int result = -1;

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                foreach (var prop in dataVO.GetType().GetProperties())
                {
                    if (prop.GetValue(dataVO, null) != null)
                    {
                        data[prop.Name + "_toSet"] = prop.GetValue(dataVO, null);
                    }
                }

                string query = "UPDATE " + tabName +
                    " SET " +
                    string.Join(", ", data.Select(x => x.Key + " = " + "@" + x.Key + "_toSet").ToArray()) +
                    " WHERE " +
                    string.Join(" ", conditions.Select(x => x.Value.Key + x.Value.Op + "@" + x.Key + " " + x.Value.Conj).ToArray());
                //string.Join(" and ", conditions.Select(x => x.Key + x.Value.Op + "@" + x.Key).ToArray());
                Dictionary<string, object> conditions_ = new Dictionary<string, object>();
                foreach (KeyValuePair<string, QueryCondition> entry in conditions)
                {
                    conditions_[entry.Key] = entry.Value.Value;

                }

                Dictionary<string, object> pars = data.Concat(conditions_).ToDictionary(x => x.Key, x => x.Value);

                log.Info(string.Format("Query: {0}", query));
                log.Info(string.Format("Params: {0}", string.Join("; ", pars.Select(x => x.Key + "=" + x.Value).ToArray())));

                result = DBSQL.ExecuteNonQueryWithParams(connectionString, query, pars);

                log.Info(string.Format("Query Executed! Inserted {0} records!", result));
               
                return result;
            }
            catch (Exception ex)
            {
                string msg = "An Error occured! Exception detected!";
                log.Info(msg);
                log.Error(msg + " " + ex.Message);
                throw;
            }
            finally
            {
                tw.Stop();
                log.Info(string.Format("Completed! Elapsed time {0}", LibString.TimeSpanToTimeHmsms(tw.Elapsed)));
            }            
        }
        
    }
}
