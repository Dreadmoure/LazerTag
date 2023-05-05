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
        private int platformID;
        #endregion

        /// <summary>
        /// constructor, gets the positon of the platform 
        /// </summary>
        /// <param name="position">the placement of the platform</param>
        public Platform(Vector2 position, int platformID)
        {
            this.position = position;
            this.platformID = platformID;
        }

        #region methods 
        public override void Awake()
        {
            // set the sprite for the platform 
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            if(platformID == 1)
            {
                spriteRenderer.SetSprite("Platforms\\Dirt");
            }
            else if(platformID == 2)
            {
                spriteRenderer.SetSprite("Platforms\\DirtWithGrass");
            }

            
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            // set the platforms position, depending on the sprite size 
            GameObject.Transform.Position = new Vector2(position.X * spriteRenderer.Sprite.Width, position.Y * spriteRenderer.Sprite.Height);
        }
        #endregion
    }
}
