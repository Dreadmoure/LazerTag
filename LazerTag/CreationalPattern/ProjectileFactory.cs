using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    public class ProjectileFactory : Factory
    {
        #region singleton
        private ProjectileFactory instance;
        public ProjectileFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ProjectileFactory();
                }
                return instance;
            }
        }
        #endregion

        private GameObject prototype1;

        private ProjectileFactory()
        {
            CreatePrototype1();
        }

        private void CreatePrototype1()
        {
            prototype1 = new GameObject();
            prototype1.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype1.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\Projectile1");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = (GameObject)prototype1.Clone();

            return gameObject;
        }
    }
}
