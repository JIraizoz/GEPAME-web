///
/// Jesús Iráizoz
///
using System.Data;
using System.Data.SqlClient;

namespace GEPAMECore.AD
{
    class AD_Gestor
    {
        AD_Gestor()
        {
            new AD_GestorSqlServer("Data Source=localhost;Initial Catalog=GEPAME;Integrated Security=True");
        }
    }

    class AD_GestorSqlServer
    {
        private string connectionString;
        private IDbConnection connection;

        internal AD_GestorSqlServer(string connectionSting)
        {
            this.connectionString = connectionSting;
            this.connection = new SqlConnection(this.connectionString);
        }

        public string ConnectionString { get => connectionString; set => connectionString = value; }

        public IDbConnection Connection { get => connection; }

    }
}
