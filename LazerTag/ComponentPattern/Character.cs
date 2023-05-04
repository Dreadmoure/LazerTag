using LazerTag.CommandPattern;
using LazerTag.ObserverPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Character : Component
    {
        #region fields
        private SpriteRenderer spriteRenderer;

        private float speed;
        private bool canShoot;
        private float shootTimer;

        //private Weapon weapon;
        #endregion

        public int AmmoCount { get; set; }

        public int CharacterId { get; set; }

        #region methods
        public override void Start()
        {
            AmmoCount = 5;
            speed = 250;
        }

        public override void Update()
        {
            //handles input
            InputHandler.Instance.Execute(this);
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

        }
        #endregion
    }
}
