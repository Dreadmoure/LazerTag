using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// superclass for all pickup types 
    /// </summary>
    public abstract class PickUp : Component
    {
        protected SpriteRenderer spriteRenderer; 
        private Collider collider;
        private float removeTimer = 0;

        /// <summary>
        /// method run when first initialized 
        /// </summary>
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
            removeTimer += GameWorld.DeltaTime; 
            if(removeTimer >= 15)
            {
                GameWorld.Instance.Destroy(GameObject);
            }
        }
    }
}
