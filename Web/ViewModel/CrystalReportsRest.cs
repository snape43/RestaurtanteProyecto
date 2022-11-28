using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class CrystalReportsRest
    {
        public static CrystalDecisions.Shared.ConnectionInfo GetConnectionInfo()
        {
            var SConn = new System.Data.SqlClient.SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DBRestauranteCnn"].ConnectionString);

            CrystalDecisions.Shared.ConnectionInfo connInfo = new CrystalDecisions.Shared.ConnectionInfo();
            connInfo.ServerName = SConn.DataSource;
            connInfo.DatabaseName = SConn.InitialCatalog;
            connInfo.UserID = SConn.UserID;
            connInfo.Password = SConn.Password;
            //    connInfo.IntegratedSecurity = SConn.IntegratedSecurity;


            /* CrystalDecisions.Shared.ConnectionInfo connInf = new CrystalDecisions.Shared.ConnectionInfo();
             connInf.ServerName = @"DESKTOP-6VL71VD";
             connInf.DatabaseName = "DBRestaurante";
             connInfo.IntegratedSecurity = true; */

            return connInfo;
        }


    }
}