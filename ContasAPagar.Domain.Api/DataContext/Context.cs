using System;
using System.Data;
using System.Data.SqlClient;

namespace ContasAPagar.Domain.Api.DataContext
{
    public class Context : IDisposable
    {
        public IDbConnection Connection { get; set; }

        public Context()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
