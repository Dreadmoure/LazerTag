using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
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

        private GameObject prototype1;

        private WeaponFactory()
        {
            CreatePrototype1();
        }

        private void CreatePrototype1()
        {
            prototype1 = new GameObject();
            prototype1.AddComponent(new Weapon());
            
            SpriteRenderer spriteRenderer = prototype1.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Weapons\\Weapon1");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.51f;
        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = (GameObject)prototype1.Clone();



            return gameObject;
        }
    }
}
