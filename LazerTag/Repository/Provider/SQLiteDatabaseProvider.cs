using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LazerTag.Repository.Provider
{
    public class SQLiteDatabaseProvider : IDatabaseProvider
    {
        private readonly string connectionString;


        #region methods
        /// <summary>
        /// Constructor for the provider which takes a string
        /// </summary>
        /// <param name="connectionString">the path to the database</param>
        public SQLiteDatabaseProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Method for returning a connection based on the string
        /// </summary>
        /// <returns>IDbConnection</returns>
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        #endregion
    }
}
