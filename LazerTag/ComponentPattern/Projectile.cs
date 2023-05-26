using LazerTag.MenuStates;
using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class Projectile : Component, IGameListener
    {
        #region fields
        private SpriteRenderer spriteRenderer;
        private Collider collider; 
        private float speed;
        #endregion

        #region properties
        /// <summary>
        /// Property used for getting and setting the velocity of the projectile
        /// </summary>
        public Vector2 Velocity { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// method for setting properties and loading stuff from the start
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            speed = 500;

            // set CollisionBox 
            collider = GameObject.GetComponent<Collider>() as Collider;
            collider.CollisionBox = new Rectangle(
                                                  (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                                                  (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                                                  (int)spriteRenderer.Sprite.Width,
                                                  (int)spriteRenderer.Sprite.Height
                                                  );
        }

        /// <summary>
        /// method which runs every frame
        /// </summary>
        public override void Update()
        {
            Move();

            // update CollisionBox 
            if (collider.CollisionBox.X != GameObject.Transform.Position.X + spriteRenderer.Sprite.Width / 2 ||
               collider.CollisionBox.Y != GameObject.Transform.Position.Y + spriteRenderer.Sprite.Height / 2)
            {
                collider.CollisionBox = new Rectangle(
                                                  (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                                                  (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                                                  (int)spriteRenderer.Sprite.Width,
                                                  (int)spriteRenderer.Sprite.Height
                                                  );
            }
        }

        /// <summary>
        /// method used to move the projectile
        /// </summary>
        private void Move()
        {
            Vector2 velocity = Velocity; 

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }

        /// <summary>
        /// Method for checking if an event happens
        /// </summary>
        /// <param name="gameEvent">the gameevent type</param>
        public void Notify(GameEvent gameEvent)
        {
            if(gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other; 

                // hit platform 
                if(other.Tag == "Platform")
                {
                    // remove self 
                    if(GameWorld.Instance.CurrentState == GameWorld.Instance.LockInState)
                    {
                        LockInState.Destroy(GameObject);
                    }
                    else
                    {
                        GameState.Destroy(GameObject);
                    }
                    
                }
            }
        }
        #endregion
    }
}
