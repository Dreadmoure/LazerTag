using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    /// <summary>
    /// class for the WeaponFactory which inherits from Factory
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
        #endregion

        #region constructor
        /// <summary>
        /// Private constructor for Weaponfactory
        /// </summary>
        private WeaponFactory()
        {
            CreatePrototype1();
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

            spriteRenderer.SetSprite("Weapons\\Weapon1");

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
            GameObject gameObject = (GameObject)prototype1.Clone();

            return gameObject;
        }
        #endregion
    }
}
