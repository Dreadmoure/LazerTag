using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag
{
    /// <summary>
    /// class for the animation
    /// </summary>
    public class Animation
    {
        #region properties
        /// <summary>
        /// property for getting and setting the frames per second of the animation
        /// </summary>
        public float FPS { get; private set; }
        /// <summary>
        /// property for getting and setting the name of the animation
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// property for getting and setting the array of sprites for the animation
        /// </summary>
        public Texture2D[] Sprites { get; private set; }
        #endregion

        #region constructors
        /// <summary>
        /// Constructor for Animation
        /// </summary>
        /// <param name="fps">Frames per second</param>
        /// <param name="name">Name of the animation</param>
        /// <param name="sprites">the array of sprites</param>
        public Animation(float fps, string name, Texture2D[] sprites)
        {
            FPS = fps;
            Name = name;
            Sprites = sprites;
        }
        #endregion
    }
}
