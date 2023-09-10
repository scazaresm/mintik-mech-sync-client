using MechanicalSyncClient.Core.Domain;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncClient.Database
{
    public sealed class DB
    {
        private static string databasePath = "sync-local-state.db";

        private static DB instance = null;

        private SQLiteConnection syncConnection;
        private SQLiteAsyncConnection asyncConnection;

        private DB()
        {
            syncConnection = new SQLiteConnection(databasePath);
            asyncConnection = new SQLiteAsyncConnection(databasePath);
            InitializeTables();
        }

        private void InitializeTables()
        {
            syncConnection.CreateTable<LocalProject>();
            syncConnection.CreateTable<FileSyncEvent>();
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

        public static SQLiteAsyncConnection Async
        {
            get
            {
                return Instance.asyncConnection;
            }
        }

        public static SQLiteConnection Sync
        {
            get
            {
                return Instance.syncConnection;
            }
        }

    }
}
