using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class AimCommand : ICommand
    {
        private Vector2 aimDirection;

        /// <summary>
        /// constructor which takes 1 parameter 
        /// </summary>
        /// <param name="aimDirection">the direction that is aimed</param>
        public AimCommand(Vector2 aimDirection)
        {
            this.aimDirection = aimDirection;
        }

        /// <summary>
        /// method for executing the aim command on character
        /// </summary>
        /// <param name="character">the character the command should execute on</param>
        public void Execute(Character character)
        {
            character.Aim(aimDirection);
        }
    }
}
