using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CommandPattern
{
    public class PadInfo
    {
        /// <summary>
        /// property for getting or setting wether a button is down
        /// </summary>
        public bool IsDown { get; set; }

        /// <summary>
        /// property for getting or setting a button
        /// </summary>
        public Buttons Button { get; set; }

        /// <summary>
        /// constructor which takes an argument so we can define a button
        /// </summary>
        /// <param name="button">the button we want as a padbind</param>
        public PadInfo(Buttons button)
        {
            this.Button = button;
        }
    }
}
