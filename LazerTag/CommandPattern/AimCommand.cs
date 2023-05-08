using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class AimCommand : ICommand
    {
        private Vector2 aimDirection;

        public AimCommand(Vector2 aimDirection)
        {
            this.aimDirection = aimDirection;
        }

        public void Execute(Character character)
        {
            character.Aim(aimDirection);
        }
    }
}
