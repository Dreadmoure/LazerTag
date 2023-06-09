﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.Repository.Provider
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// for implementation 
        /// </summary>
        /// <returns>IDbConnection</returns>
        IDbConnection CreateConnection();
    }
}
