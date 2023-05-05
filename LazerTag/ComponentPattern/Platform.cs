using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// class for platforms 
    /// </summary>
    public class Platform : Component
    {
        #region fields 
        private SpriteRenderer spriteRenderer;
        private Vector2 position;
        #endregion

        /// <summary>
        /// constructor, gets the positon of the platform 
        /// </summary>
        /// <param name="position">the placement of the platform</param>
        public Platform(Vector2 position)
        {
            this.position = position; 
        }

        #region methods 
        public override void Awake()
        {
            // set the sprite for the platform 
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("Platforms\\tile1");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            // set the platforms position, depending on the sprite size 
            GameObject.Transform.Position = new Vector2(position.X * spriteRenderer.Sprite.Width, position.Y * spriteRenderer.Sprite.Height);
        }
        #endregion
    }
}
