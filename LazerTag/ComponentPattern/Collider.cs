using LazerTag.ObserverPattern;
using LazerTag.ObserverPattern.PlatformCollisionEvents;
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
        public TopCollisionEvent TopCollisionEvent { get; private set; } = new TopCollisionEvent();
        public BottomCollisionEvent BottomCollisionEvent { get; private set; } = new BottomCollisionEvent();
        public LeftCollisionEvent LeftCollisionEvent { get; private set; } = new LeftCollisionEvent();
        public RightCollisionEvent RightCollisionEvent { get; private set; } = new RightCollisionEvent();

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
            texture = GameWorld.Instance.Content.Load<Texture2D>("Pixel");
        }

        /// <summary>
        /// updates each frame
        /// </summary>
        /// <param name="gameTime">we can acces the renderer through this</param>
        public override void Update()
        {
            //UpdatePixelCollider();
            CheckCollision();
        }

        //use this if you want to draw the collisionboxes
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(CollisionBox, spriteBatch);
        }

        public void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// method for checking if objects collide
        /// </summary>
        private void CheckCollision()
        {
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other != this && other.CollisionBox.Intersects(CollisionBox))
                {
                    CollisionEvent.Notify(other.GameObject);

                    if(TouchTopOf(CollisionBox, other.CollisionBox))
                    {
                        TopCollisionEvent.Notify(other.GameObject); 
                    }
                    if (TouchBottomOf(CollisionBox, other.CollisionBox))
                    {
                        BottomCollisionEvent.Notify(other.GameObject);
                    }
                    if (TouchLeftOf(CollisionBox, other.CollisionBox))
                    {
                        LeftCollisionEvent.Notify(other.GameObject);
                    }
                    if (TouchRightOf(CollisionBox, other.CollisionBox))
                    {
                        RightCollisionEvent.Notify(other.GameObject);
                    }
                }
            }
        }

        private bool TouchTopOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 5 &&
                    r1.Bottom <= r2.Top + 5 &&
                    r1.Right >= r2.Left &&
                    r1.Left <= r2.Right); 
        }
        private bool TouchBottomOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + 10 &&
                    r1.Top >= r2.Bottom - 10 &&
                    r1.Right >= r2.Left + 7 &&
                    r1.Left <= r2.Right - 7);
        }
        private bool TouchLeftOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Left + 5 &&
                    r1.Right >= r2.Left - 5 &&
                    r1.Top <= r2.Bottom - 5 &&
                    r1.Bottom >= r2.Top + 5);
        }
        private bool TouchRightOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Right - 5 &&
                    r1.Left <= r2.Right + 5 &&
                    r1.Top <= r2.Bottom - 5 &&
                    r1.Bottom >= r2.Top + 5);
        }
        #endregion
    }
}
