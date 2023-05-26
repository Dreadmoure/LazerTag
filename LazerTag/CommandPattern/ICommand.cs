using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// method for implementation
        /// </summary>
        /// <param name="player"></param>
        public void Execute(Character character);
    }
}
