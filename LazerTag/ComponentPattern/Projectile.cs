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
    public class Projectile : Component, IGameListener
    {
        private SpriteRenderer spriteRenderer;
        private Collider collider; 
        private float speed;
        public Vector2 Velocity { get; set; }

        public Projectile()
        {

        }

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

        public void Notify(GameEvent gameEvent)
        {
            if(gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other; 

                // hit platform 
                if(other.Tag == "Platform")
                {
                    // remove self 
                    GameWorld.Instance.Destroy(GameObject);
                }

                // only collide with character if character is not own 
                if(other.GetComponent<Character>() != null && other.Tag != GameObject.Tag)
                {
                    // update score on player that the projectile came from 

                    // update health on player that was hit 

                    // remove character that was hit
                    //GameWorld.Instance.Destroy(other);

                    // remove self 
                    //GameWorld.Instance.Destroy(GameObject); 
                }
            }
        }
    }
}
