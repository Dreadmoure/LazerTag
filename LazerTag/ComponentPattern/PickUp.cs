using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public abstract class PickUp : Component
    {
        protected SpriteRenderer spriteRenderer; 
        private Collider collider; 

        public override void Start()
        {
            // set CollisionBox 
            collider = GameObject.GetComponent<Collider>() as Collider;
            collider.CollisionBox = new Rectangle(
                                                  (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                                                  (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                                                  spriteRenderer.Sprite.Width,
                                                  spriteRenderer.Sprite.Height
                                                  );
        }

        public override void Update()
        {
            // if we want the pickups to fall with gravity, uncomment this 
            // update CollisionBox 
            //if (collider.CollisionBox.X != GameObject.Transform.Position.X + spriteRenderer.Sprite.Width / 2 ||
            //   collider.CollisionBox.Y != GameObject.Transform.Position.Y + spriteRenderer.Sprite.Height / 2)
            //{
            //    collider.CollisionBox = new Rectangle(
            //                                      (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
            //                                      (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
            //                                      spriteRenderer.Sprite.Width,
            //                                      spriteRenderer.Sprite.Height
            //                                      );
            //}
        }
    }
}
