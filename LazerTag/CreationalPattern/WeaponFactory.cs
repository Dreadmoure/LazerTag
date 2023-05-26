using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LazerTag.CreationalPattern
{
    /// <summary>
    /// Forfatter : Denni
    /// </summary>
    public class WeaponFactory : Factory
    {
        #region singleton
        private static WeaponFactory instance;
        public static WeaponFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new WeaponFactory();
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
        /// Private constructor for Weaponfactory
        /// </summary>
        private WeaponFactory()
        {
            CreatePrototype1();
            CreatePrototype2();
            CreatePrototype3();
            CreatePrototype4();
        }
        #endregion

        #region methods
        /// <summary>
        /// Method for making prototype1
        /// </summary>
        private void CreatePrototype1()
        {
            prototype1 = new GameObject();
            prototype1.AddComponent(new Weapon());
            
            SpriteRenderer spriteRenderer = prototype1.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Weapons\\WeaponRed");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.98f;
        }

        /// <summary>
        /// Method for making prototype2
        /// </summary>
        private void CreatePrototype2()
        {
            prototype2 = new GameObject();
            prototype2.AddComponent(new Weapon());

            SpriteRenderer spriteRenderer = prototype2.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Weapons\\WeaponBlue");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.98f;
        }

        /// <summary>
        /// Method for making prototype3
        /// </summary>
        private void CreatePrototype3()
        {
            prototype3 = new GameObject();
            prototype3.AddComponent(new Weapon());

            SpriteRenderer spriteRenderer = prototype3.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Weapons\\WeaponGreen");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.98f;
        }

        /// <summary>
        /// Method for making prototype4
        /// </summary>
        private void CreatePrototype4()
        {
            prototype4 = new GameObject();
            prototype4.AddComponent(new Weapon());

            SpriteRenderer spriteRenderer = prototype4.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Weapons\\WeaponPink");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.98f;
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

            return gameObject;
        }
        #endregion
    }
}
