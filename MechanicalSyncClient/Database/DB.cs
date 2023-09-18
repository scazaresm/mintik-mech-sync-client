using MechanicalSyncClient.Core.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Database
{
    public sealed class DB : IDisposable
    {
        private static string databasePath = "sync-local-state.db";

        private static DB instance = null;

        private readonly SQLiteAsyncConnection _connection;
        private bool disposedValue;

        private DB()
        {
            using(var syncConnection = new SQLiteConnection(databasePath))
            {
                syncConnection.CreateTable<LocalProject>();
                syncConnection.CreateTable<FileSyncEvent>();
            }
            _connection = new SQLiteAsyncConnection(databasePath);
        }

        public static DB Instance
        {
            get
            {
                if (instance == null)
                    instance = new DB();
                return instance;
            }
        }

        public static SQLiteAsyncConnection Connection
        {
            get
            {
                return Instance._connection;
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ = _connection.CloseAsync();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
