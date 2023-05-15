using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class ThreadCommand
    {
        private Character character; 
        private Vector2 direction; 

        public ThreadCommand(Character character, Vector2 direction)
        {
            this.character = character;
            this.direction = direction; 
        }

        public void ThreadWithParameter()
        {
            character.Aim(direction);
        }
    }
}
