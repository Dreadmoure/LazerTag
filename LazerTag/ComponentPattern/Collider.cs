using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Collider : Component
    {
        #region fields
        private SpriteRenderer spriteRenderer = new SpriteRenderer();
        private Texture2D texture;
        #endregion

        #region properties
        /// <summary>
        /// used to get or set the collsion event
        /// </summary>
        public CollisionEvent CollisionEvent { get; private set; } = new CollisionEvent();

        /// <summary>
        /// used to return a rectangle based on the objects position and its sprite
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                        (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                        spriteRenderer.Sprite.Width,
                        spriteRenderer.Sprite.Height
                    );
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// runs at the begining
        /// </summary>
        public override void Start()
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();
            texture = GameWorld.Instance.Content.Load<Texture2D>("Sprites\\Pixel");
        }

        /// <summary>
        /// updates each frame
        /// </summary>
        /// <param name="gameTime">we can acces the renderer through this</param>
        public override void Update(GameTime gameTime)
        {
            //UpdatePixelCollider();
            CheckCollision();
        }

        //use this if you want to draw the collisionboxes
        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    DrawRectangle(CollisionBox, spriteBatch);
        //}

        /// <summary>
        /// method for checking if objects collide
        /// </summary>
        private void CheckCollision()
        {
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other != this && other.CollisionBox.Contains(CollisionBox))
                {
                    CollisionEvent.Notify(other.GameObject);
                }
            }
        }
        #endregion
    }
}
