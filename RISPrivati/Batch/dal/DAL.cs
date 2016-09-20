using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public partial class DAL
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string GRConnectionString = ConfigurationManager.ConnectionStrings["GR"].ConnectionString;
        public string HltDesktopConnectionString = ConfigurationManager.ConnectionStrings["HltDesktop"].ConnectionString;

    }
}
