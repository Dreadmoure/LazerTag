using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    /// <summary>
    /// Forfatter : Ida
    /// </summary>
    public class JumpCommand : ICommand
    {
        /// <summary>
        /// method for executing command jump on character 
        /// </summary>
        /// <param name="character">the character the command should be executed on</param>
        public void Execute(Character character)
        {
            character.Jump(); 
        }
    }
}
