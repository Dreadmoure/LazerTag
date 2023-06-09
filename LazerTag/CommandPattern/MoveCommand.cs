﻿using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    /// <summary>
    /// Forfatter : Denni
    /// </summary>
    public class MoveCommand : ICommand
    {
        private Vector2 velocity;

        /// <summary>
        /// sets the velocity based on the input
        /// </summary>
        /// <param name="velocity">which direction we want</param>
        public MoveCommand(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        /// <summary>
        /// calls the move command on the character
        /// </summary>
        /// <param name="character">the character we need to execute the method on</param>
        public void Execute(Character character)
        {
            character.Move(velocity);
        }
    }
}
