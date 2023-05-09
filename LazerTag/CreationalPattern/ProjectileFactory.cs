using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    public class ProjectileFactory : Factory
    {
        #region singleton
        private static ProjectileFactory instance;
        public static ProjectileFactory Instance
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

            prototype1.Transform.Rotate(0.3f); 
        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = (GameObject)prototype1.Clone();
            gameObject.Tag = type.ToString();

            gameObject.Transform.Rotate(0.9f);

            Projectile projectile = gameObject.GetComponent<Projectile>() as Projectile; 
            Collider collider = gameObject.AddComponent(new Collider()) as Collider;
            collider.CollisionEvent.Attach(projectile); 

            return gameObject;
        }
    }
}
