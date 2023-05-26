﻿using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    /// <summary>
    /// Forfatter : Ida
    /// </summary>
    public class HorizontalProjectileFactory : Factory
    {
        #region singleton
        private static HorizontalProjectileFactory instance;
        public static HorizontalProjectileFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HorizontalProjectileFactory();
                }
                return instance;
            }
        }
        #endregion

        #region fields
        private GameObject prototype1;
        private GameObject prototype2;
        private GameObject prototype3;
        private GameObject prototype4;
        #endregion

        #region constructor
        /// <summary>
        /// private constructor for ProjectileFactory
        /// </summary>
        private HorizontalProjectileFactory()
        {
            CreatePrototype1();
            CreatePrototype2();
            CreatePrototype3();
            CreatePrototype4();
        }
        #endregion

        #region methods
        /// <summary>
        /// Method for creating prototype1
        /// </summary>
        private void CreatePrototype1()
        {
            prototype1 = new GameObject();
            prototype1.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype1.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\RedH");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        /// <summary>
        /// Method for creating prototype2
        /// </summary>
        private void CreatePrototype2()
        {
            prototype2 = new GameObject();
            prototype2.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype2.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\BlueH");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        /// <summary>
        /// Method for creating prototype3
        /// </summary>
        private void CreatePrototype3()
        {
            prototype3 = new GameObject();
            prototype3.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype3.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\GreenH");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        /// <summary>
        /// Method for creating prototype4
        /// </summary>
        private void CreatePrototype4()
        {
            prototype4 = new GameObject();
            prototype4.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype4.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\PinkH");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        /// <summary>
        /// Method for creating a gameobject, by cloning it
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>GameObject</returns>
        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            switch (type)
            {
                case PlayerIndex.One:
                    gameObject = (GameObject)prototype1.Clone();
                    break;
                case PlayerIndex.Two:
                    gameObject = (GameObject)prototype2.Clone();
                    break;
                case PlayerIndex.Three:
                    gameObject = (GameObject)prototype3.Clone();
                    break;
                case PlayerIndex.Four:
                    gameObject = (GameObject)prototype4.Clone();
                    break;
            }

            Projectile projectile = gameObject.GetComponent<Projectile>() as Projectile;
            Collider collider = gameObject.AddComponent(new Collider()) as Collider;
            collider.CollisionEvent.Attach(projectile);

            return gameObject;
        }
        #endregion
    }
}
