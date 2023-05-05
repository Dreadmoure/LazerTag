using LazerTag.CommandPattern;
using LazerTag.ObserverPattern;
using LazerTag.ObserverPattern.PlatformCollisionEvents;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Character : Component, IGameListener
    {
        #region fields
        private SpriteRenderer spriteRenderer;

        private float speed;
        private bool canShoot;
        private float shootTimer;

        private Vector2 gravity;

        //private Weapon weapon;
        #endregion

        public int AmmoCount { get; set; }

        public int CharacterId { get; set; }

        #region methods
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer; 
            AmmoCount = 5;
            speed = 250;
            // set gravity, remember to multiply with speed 
            gravity = new Vector2(0, 0.9f) * speed;
        }

        public override void Update()
        {
            //handles input
            InputHandler.Instance.Execute(this);

            // make character fall using gravity 
            GameObject.Transform.Translate(gravity * GameWorld.DeltaTime); 
        }

        public void Move(Vector2 velocity)
        {
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;
            GameObject.Transform.Translate(velocity * GameWorld.DeltaTime);
        }

        public void Shoot()
        {

        }

        public void Jump()
        {

        }

        public void Notify(GameEvent gameEvent)
        {
            if(gameEvent is CollisionEvent)
            {
                GameObject other = (gameEvent as CollisionEvent).Other; 

                // check for pixel collision here 

                // check for other characters projectiles 
                if(other.Tag == "Projectile")
                {

                }

                // check for pick ups 
                if(other.Tag == "PickUp")
                {

                }
            }

            // platform side collision events 
            if(gameEvent is TopCollisionEvent)
            {
                GameObject other = (gameEvent as TopCollisionEvent).Other;

                if(other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // set character to be on top of the platform, so it does not fall through 
                    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, 
                                                                other.Transform.Position.Y - (otherSpriteRenderer.Origin.Y + spriteRenderer.Origin.Y));
                }
            }
            if (gameEvent is BottomCollisionEvent)
            {
                GameObject other = (gameEvent as BottomCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not get up through platform 
                    GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X,
                                                                other.Transform.Position.Y + (otherSpriteRenderer.Origin.Y + spriteRenderer.Origin.Y));
                }
            }
            if (gameEvent is LeftCollisionEvent)
            {
                GameObject other = (gameEvent as LeftCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not move right into platform 
                    GameObject.Transform.Position = new Vector2(other.Transform.Position.X - (otherSpriteRenderer.Origin.X + spriteRenderer.Origin.X),
                                                                GameObject.Transform.Position.Y);
                }
            }
            if (gameEvent is RightCollisionEvent)
            {
                GameObject other = (gameEvent as RightCollisionEvent).Other;

                if (other.Tag == "Platform")
                {
                    // get the platforms spriterenderer 
                    SpriteRenderer otherSpriteRenderer = other.GetComponent<SpriteRenderer>() as SpriteRenderer;

                    // make sure character can not move left into platform 
                    GameObject.Transform.Position = new Vector2(other.Transform.Position.X + (otherSpriteRenderer.Origin.X + spriteRenderer.Origin.X),
                                                                GameObject.Transform.Position.Y);
                }
            }
        }
        #endregion
    }
}
