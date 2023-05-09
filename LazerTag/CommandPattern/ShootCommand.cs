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
        public void Execute(Character character)
        {
            character.Shoot(); 
        }
    }
}
