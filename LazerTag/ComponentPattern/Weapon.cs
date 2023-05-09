using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Weapon : Component
    {
        private SpriteRenderer spriteRenderer;
        private Vector2 aimDirection; //maybe enum
        public Vector2 ProjectileSpawnPosition { get; private set; }
        public Vector2 ProjectileVelocity { get; private set; }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

        }

        public override void Update()
        {
            
        }

        public void Move(Vector2 position)
        {
            ProjectileSpawnPosition = position; 

            //if (aimDirection.X == -1 && aimDirection.Y == -1)
            //{
            //    // top left 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, -30);
            //    GameObject.Transform.Rotation = -0.6f;
            //}
            //else if (aimDirection.X == 1 && aimDirection.Y == -1)
            //{
            //    // top right 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, -30);
            //    GameObject.Transform.Rotation = 0.6f;
            //}
            //else if (aimDirection.X == -1 && aimDirection.Y == 1)
            //{
            //    // bottom left 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, -30);
            //    GameObject.Transform.Rotation = -5.6f;
            //}
            //else if (aimDirection.X == 1 && aimDirection.Y == 1)
            //{
            //    // bottom right 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, -30);
            //    GameObject.Transform.Rotation = -10.6f;
            //}
            //else
            if (aimDirection.Y == -1)
            {
                // top 
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(0, -30);
                GameObject.Transform.Rotation = -1.6f;
                ProjectileVelocity = new Vector2(0, -1);
            }
            else if (aimDirection.Y == 1)
            {
                // bottom 
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(0, 30);
                GameObject.Transform.Rotation = 1.6f;
                ProjectileVelocity = new Vector2(0, 1);
            }
            else if (aimDirection.X == -1)
            {
                // left 
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
                GameObject.Transform.Position = position + new Vector2(-30, 0);
                GameObject.Transform.Rotation = 0f;
                ProjectileVelocity = new Vector2(-1, 0);
            }
            else
            {
                // right
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(30, 0);
                GameObject.Transform.Rotation = 0f;
                ProjectileVelocity = new Vector2(1, 0);
            }

        }

        public void Aim(Vector2 aimDirection)
        {
            this.aimDirection = aimDirection;
        }
    }
}
