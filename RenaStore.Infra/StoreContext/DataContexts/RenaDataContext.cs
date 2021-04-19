using System;
using System.Data;
using System.Data.SqlClient;
using RenaStore.Shared;

namespace RenaStore.Infra.StoreContext.DataContexts
{
    public class RenaDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public RenaDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if(Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}