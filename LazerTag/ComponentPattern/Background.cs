using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace LazerTag.ComponentPattern
{
    public class Background : Component
    {
        private Color startColor;
        private Color endColor;
        private float amount;
        private bool colorShiftUp;

        public Color CurrentColor { get; private set; }

        public override void Start()
        {
            startColor = new Color(176, 196, 222, 255); // 105, 105, 105, 255
            endColor = new Color(0, 0, 0, 255);

            CurrentColor = startColor;
            amount = 0;

            Thread t = new Thread(Lerp);
            t.IsBackground = true;
            t.Start();
            
        }


        private void Lerp()
        {
            while (true)
            {
                if(amount >= 0.8f)
                {
                    colorShiftUp = false;
                }
                else if(amount <= 0)
                {
                    colorShiftUp = true;
                }

                if (colorShiftUp)
                {
                    amount += 0.0005f;
                }
                else
                {
                    amount -= 0.0005f;
                }

                CurrentColor = Color.Lerp(startColor, endColor, amount);
                Thread.Sleep(10);
            }
            
        }
    }
}
