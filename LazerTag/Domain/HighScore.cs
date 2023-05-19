using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.Domain
{
    public class HighScore
    {
        #region properties
        /// <summary>
        /// property to get or set the ID of the HighScore
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// property to get or set the Name of the HighScore
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// property to get or set the score of the HighScore
        /// </summary>
        public int Score { get; set; }
        #endregion
    }
}
