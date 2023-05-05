using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag
{
    public class Animation
    {
        #region properties
        public float FPS { get; private set; }

        public string Name { get; private set; }

        public Texture2D[] Sprites { get; private set; }
        #endregion

        #region constructors
        public Animation(float fps, string name, Texture2D[] sprites)
        {
            FPS = fps;
            Name = name;
            Sprites = sprites;
        }
        #endregion
    }
}
