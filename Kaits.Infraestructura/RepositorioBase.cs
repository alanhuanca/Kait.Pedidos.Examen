using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Kaits.Infraestructura
{
    public abstract class RepositorioBase
    {
        protected IDbConnection CreateConnection()
        {
            var cadenaConexion = ConfigurationManager.ConnectionStrings["KaitsConexion"].ConnectionString;
            return new SqlConnection(cadenaConexion);
        }
    }
}
