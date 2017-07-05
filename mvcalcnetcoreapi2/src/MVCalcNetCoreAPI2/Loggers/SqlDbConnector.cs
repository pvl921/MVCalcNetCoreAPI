using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MVCalcNetCoreAPI2.Loggers
{
    public class SqlDbConnector
    {
        ///<summary>
        ///Определяет подключение к БД и хранимую процедуру для последующего вызова. 
        ///</summary>
        public class GetDBStoredProcedure : IDisposable
        {
            const string CONNECTION_STRING = "Server = (local); Database=testDB; User Id=sa; Password=1234;";
            private bool disposed = false;
            public SqlConnection connection { get; }
            public SqlCommand command { get; }
            public GetDBStoredProcedure(string sp)
            {
                connection = new SqlConnection(CONNECTION_STRING);
                command = new SqlCommand(sp, connection);
                command.CommandType = CommandType.StoredProcedure;
            }
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                        command.Dispose();
                        connection.Dispose();
                    }
                    // Free your own state (unmanaged objects).
                    // Set large fields to null.
                    disposed = true;
                }
            }
            ~GetDBStoredProcedure()
            {
                Dispose(false);
            }
        }
    }
}
