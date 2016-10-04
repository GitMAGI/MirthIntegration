using System;
using System.Data.SqlClient;

namespace GeneralPurposeLib
{
    public static class LibString
    {
        public static string TimeSpanToTimeHmsms(TimeSpan ts)
        {
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours,
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds);

            return elapsedTime;
        }

        static public bool IsNumericType(this object o)
        {
            bool result = false;
            var type = Type.GetTypeCode(o.GetType());
            switch (type)
            {                
                case TypeCode.DateTime:
                    result=false;
                    break;
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    result=true;
                    break;
                default:
                    result=false;
                    break;
            }
            return result;
        }

        static public string SQLCommand2String(SqlCommand cmd)
        {
            string result = "";
            if (cmd.CommandType == System.Data.CommandType.StoredProcedure)
            {
                result = SQLCommandSP2String(cmd);
            }
            else
            {
                result = SQLCommandQuery2String(cmd);
            }

            return result;
        }

        static private string SQLCommandQuery2String(SqlCommand cmd)
        {
            string result = "";

            result = cmd.CommandText;
            //int _l = 1;
            foreach (SqlParameter p in cmd.Parameters)
            {
                string val_ = IsNumericType(p.DbType) ? p.Value.ToString() : string.Format("'{0}'", p.Value.ToString());
                string par_ = "@" + p.ParameterName;

                result = result.Replace(par_, val_);

                /*
                if (_l < cmd.Parameters.Count)
                    result += ", ";
                _l++;
                 * */
            }

            return result;
        }

        static private string SQLCommandSP2String(SqlCommand cmd)
        {
            string result = "";

            result = "exec " + cmd.CommandText + " ";
            int _l = 1;
            foreach (SqlParameter p in cmd.Parameters)
            {
                //string pv = IsNumericType(p.Value) == true ? p.ParameterName + " = " + p.Value.ToString() : p.ParameterName + " = '" + p.Value.ToString() + "'";
                string pv = IsNumericType(p.DbType) == true ? p.ParameterName + " = " + p.Value.ToString() : p.ParameterName + " = '" + p.Value.ToString() + "'";
                result += pv;
                if (_l < cmd.Parameters.Count)
                    result += ", ";
                _l++;
            }

            return result;
        }
    }
}
