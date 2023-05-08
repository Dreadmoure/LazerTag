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
        private Vector2 projectileSpawnPosition;

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

        }

        public override void Update()
        {
            
        }

        public void Move(Vector2 position)
        {
            if (aimDirection.Y == -1)
            {
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(0, -30);
                GameObject.Transform.Rotation = -1.6f;
            }
            else if (aimDirection.Y == 1)
            {
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(0, 30);
                GameObject.Transform.Rotation = 1.6f;
            }

            else if (aimDirection.X == -1)
            {
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
                GameObject.Transform.Position = position + new Vector2(-30, 0);
                GameObject.Transform.Rotation = 0f;
            }
            else
            {
                spriteRenderer.Flip = SpriteEffects.None;
                GameObject.Transform.Position = position + new Vector2(30, 0);
                GameObject.Transform.Rotation = 0f;
            }

        }

        public void Aim(Vector2 aimDirection)
        {
            this.aimDirection = aimDirection;
        }
    }
}
