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
        Vertical
    }

    public class Weapon : Component
    {
        #region fields
        private SpriteRenderer spriteRenderer;
        private Vector2 aimDirection;
        private Vector2 position; 
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
        }

        /// <summary>
        /// Method used to move the weapon with the position of the character
        /// </summary>
        /// <param name="characterPosition">characters position</param>
        public void Move(Vector2 characterPosition)
        {
            ProjectileSpawnPosition = characterPosition;
            this.position = characterPosition; 

            int displacement = 30;

            // set position 
            GameObject.Transform.Position = characterPosition + displacement * aimDirection;

            // set weapon rotation 
            //GameObject.Transform.Rotation = (float)Math.Atan2(aimDirection.Y, aimDirection.X);

            //if (GameObject.Transform.Rotation > Math.PI / 2 && GameObject.Transform.Rotation < 3 * Math.PI / 2)
            //{
            //    spriteRenderer.Flip = SpriteEffects.FlipVertically;
            //}
            //else
            //{
            //    spriteRenderer.Flip = SpriteEffects.None;
            //}




            //if (aimDirection.Y == -1)
            //{
            //    // top 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, -30);
            //    GameObject.Transform.Rotation = -1.6f;

            //    ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(0, -35); 
            //    ProjectileVelocity = new Vector2(0, -1);
            //    ProjectileDirection = ProjectileDirection.Vertical; 
            //}
            //else if (aimDirection.Y == 1)
            //{
            //    // bottom 
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(0, 30);
            //    GameObject.Transform.Rotation = 1.6f;

            //    ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(0, 35);
            //    ProjectileVelocity = new Vector2(0, 1);
            //    ProjectileDirection = ProjectileDirection.Vertical;
            //}
            //else if (aimDirection.X == -1)
            //{
            //    // left 
            //    spriteRenderer.Flip = SpriteEffects.FlipHorizontally;
            //    GameObject.Transform.Position = position + new Vector2(-30, 0);
            //    GameObject.Transform.Rotation = 0f;

            //    ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(-35, 0);
            //    ProjectileVelocity = new Vector2(-1, 0);
            //    ProjectileDirection = ProjectileDirection.Horizontal;
            //}
            //else
            //{
            //    // right
            //    spriteRenderer.Flip = SpriteEffects.None;
            //    GameObject.Transform.Position = position + new Vector2(30, 0);
            //    GameObject.Transform.Rotation = 0f;

            //    ProjectileSpawnPosition = GameObject.Transform.Position + new Vector2(35, 0);
            //    ProjectileVelocity = new Vector2(1, 0);
            //    ProjectileDirection = ProjectileDirection.Horizontal;
            //}

        }


        /// <summary>
        /// method used to determine which way the weapon is aiming
        /// </summary>
        /// <param name="aimDirection">the direction it is aiming</param>
        public void Aim(Vector2 aimDirection)
        {
            this.aimDirection = new Vector2(aimDirection.X - position.X, aimDirection.Y - position.Y);

            if(this.aimDirection != Vector2.Zero)
            {
                this.aimDirection.Normalize();
            }

            ProjectileVelocity = aimDirection;

            // set weapon rotation 
            GameObject.Transform.Rotation = (float)Math.Atan2(aimDirection.Y, aimDirection.X);
        }
        #endregion
    }
}
