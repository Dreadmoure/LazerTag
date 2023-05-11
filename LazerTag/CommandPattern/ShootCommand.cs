using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class ShootCommand : ICommand
    {
        /// <summary>
        /// method for executing shoot command on character 
        /// </summary>
        /// <param name="character">the character the command should be executed on</param>
        public void Execute(Character character)
        {
            character.Shoot(); 
        }
    }
}
