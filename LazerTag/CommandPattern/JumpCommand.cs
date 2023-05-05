using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class JumpCommand : ICommand
    {
        public void Execute(Character character)
        {
            character.Jump(); 
        }
    }
}
