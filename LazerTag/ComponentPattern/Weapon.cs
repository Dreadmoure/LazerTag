using LazerTag.CreationalPattern;
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
    /// <summary>
    /// enum for the direction of the projectile
    /// </summary>
    public enum ProjectileDirection
    {
        Horizontal,
        Vertical,
        DiagonalLeft,
        DiagonalRight
    }

    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class Weapon : Component
    {
        #region fields
        private SpriteRenderer spriteRenderer;
        private Vector2 aimDirection;
        private Vector2 offset;
        #endregion

        #region properties
        /// <summary>
        /// method for getting and setting the spawn position of the projectile
        /// </summary>
        public Vector2 ProjectileSpawnPosition { get; private set; }

        /// <summary>
        /// method for getting and setting the velocity of the projectile
        /// </summary>
        public Vector2 ProjectileVelocity { get; private set; }

        /// <summary>
        /// method for getting and setting the direction of the projectile
        /// </summary>
        public ProjectileDirection ProjectileDirection { get; private set; }
        #endregion

        #region methods
        /// <summary>
        /// Method that runs as the first when the program starts
        /// </summary>
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            aimDirection = new Vector2(1, 0);
            offset = new Vector2(30, 0);
        }

        /// <summary>
        /// Method used to move the weapon with the position of the character
        /// </summary>
        /// <param name="position">characters position</param>
        public void Move(Vector2 position)
        {
            ProjectileSpawnPosition = position;
            GameObject.Transform.Position = position + offset;

            
            if (aimDirection.Y == -1)
            {
                // top 
                spriteRenderer.Flip = SpriteEffects.None;
                offset = new Vector2(0, -30);
                GameObject.Transform.Rotation = -1.6f;

                ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(0, -35); 
                ProjectileVelocity = new Vector2(0, -1);
                ProjectileDirection = ProjectileDirection.Vertical; 
            }
            else if (aimDirection.Y == 1)
            {
                // bottom 
                spriteRenderer.Flip = SpriteEffects.None;
                offset = new Vector2(0, 30);
                GameObject.Transform.Rotation = 1.6f;

                ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(0, 35);
                ProjectileVelocity = new Vector2(0, 1);
                ProjectileDirection = ProjectileDirection.Vertical;
            }
            else if (aimDirection.X == -1)
            {
                // left 
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
                offset = new Vector2(-30, 0);
                GameObject.Transform.Rotation = 0f;

                ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(-35, 0);
                ProjectileVelocity = new Vector2(-1, 0);
                ProjectileDirection = ProjectileDirection.Horizontal;
            }
            else if (aimDirection.X == 1)
            {
                // right
                spriteRenderer.Flip = SpriteEffects.None;
                offset = new Vector2(30, 0);
                GameObject.Transform.Rotation = 0f;

                ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(35, 0);
                ProjectileVelocity = new Vector2(1, 0);
                ProjectileDirection = ProjectileDirection.Horizontal;
            }
            else if (aimDirection.X > Math.Cos(7*Math.PI /6) && aimDirection.X < Math.Cos(4*Math.PI /3) && aimDirection.Y > -Math.Sin(7 * Math.PI / 6) && aimDirection.Y < -Math.Sin(4 * Math.PI / 3))
            {
                // bottom left
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
                offset = new Vector2(-15, 15);
                GameObject.Transform.Rotation = -0.8f;

                ProjectileSpawnPosition = GameObject.Transform.Position + offset;
                ProjectileVelocity = new Vector2(-1, 1);
                ProjectileDirection = ProjectileDirection.DiagonalRight;
            }
            else if (aimDirection.X > Math.Cos(5* Math.PI / 3) && aimDirection.X < Math.Cos(11 * Math.PI / 6) && aimDirection.Y > -Math.Sin(11 * Math.PI / 6) && aimDirection.Y < -Math.Sin(5 * Math.PI / 3))
            {
                // bottom right
                spriteRenderer.Flip = SpriteEffects.None;
                offset = new Vector2(15, 15);
                GameObject.Transform.Rotation = 0.8f;

                ProjectileSpawnPosition = GameObject.Transform.Position + offset;
                ProjectileVelocity = new Vector2(1, 1);
                ProjectileDirection = ProjectileDirection.DiagonalLeft;
            }
            else if (aimDirection.X > Math.Cos(Math.PI / 3) && aimDirection.X < Math.Cos(Math.PI / 6) && aimDirection.Y < -Math.Sin(Math.PI / 6) && aimDirection.Y > -Math.Sin(Math.PI / 3))
            {
                // top right
                spriteRenderer.Flip = SpriteEffects.None;
                offset = new Vector2(15, -15);
                GameObject.Transform.Rotation = -0.8f;

                ProjectileSpawnPosition = GameObject.Transform.Position + offset;
                ProjectileVelocity = new Vector2(1, -1);
                ProjectileDirection = ProjectileDirection.DiagonalRight;
            }
            else if (aimDirection.X < Math.Cos(2 * Math.PI / 3) && aimDirection.X > Math.Cos(5 * Math.PI / 6) && aimDirection.Y > -Math.Sin(2 * Math.PI / 3) && aimDirection.Y < -Math.Sin(5 * Math.PI / 6))
            {
                // top left
                spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
                offset = new Vector2(-15, -15);
                GameObject.Transform.Rotation = 0.8f;

                ProjectileSpawnPosition = GameObject.Transform.Position + offset;
                ProjectileVelocity = new Vector2(-1, -1);
                ProjectileDirection = ProjectileDirection.DiagonalLeft;
            }
            

        }


        /// <summary>
        /// method used to determine which way the weapon is aiming
        /// </summary>
        /// <param name="aimDirection">the direction it is aiming</param>
        public void Aim(Vector2 aimDirection)
        {
            this.aimDirection = aimDirection;
            
            if(this.aimDirection != Vector2.Zero)
            {
                this.aimDirection.Normalize();
            }
        }
        #endregion
    }
}
