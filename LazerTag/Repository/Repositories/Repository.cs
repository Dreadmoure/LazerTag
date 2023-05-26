using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazerTag.Repository.Provider;

namespace LazerTag.Repository.Repositories
{
    /// <summary>
    /// class used to inherit functionality
    /// Forfatter: Denni, Ida
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Gets the database provider 
        /// </summary>
        protected IDatabaseProvider Provider { get; set; }

        /// <summary>
        /// Gets and sets database connection 
        /// </summary>
        protected IDbConnection Connection { get; private set; }

        /// <summary>
        /// method used for opening a repository
        /// </summary>
        public void Open()
        {
            if (Connection == null)
            {
                Connection = Provider.CreateConnection();
            }
            Connection.Open();
            CreateDatabaseTables();
        }

        /// <summary>
        /// Implemented in the specific repository 
        /// </summary>
        protected virtual void CreateDatabaseTables()
        {
            
        }

        /// <summary>
        /// method used for closing a repository
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }
    }
}
