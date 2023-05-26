using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.BuilderPattern
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// for implementation
        /// </summary>
        public void BuildGameObject();

        /// <summary>
        /// for implementation
        /// </summary>
        /// <returns>GameObject</returns>
        public GameObject GetResult();
    }
}
