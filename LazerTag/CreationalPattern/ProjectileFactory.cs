using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    public enum ProjectileDirection
    {
        Horizontal, 
        Vertical 
    }

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
        private GameObject prototype2;

        private ProjectileFactory()
        {
            CreatePrototype1();
            CreatePrototype2();
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

        private void CreatePrototype2()
        {
            prototype2 = new GameObject();
            prototype2.AddComponent(new Projectile());

            SpriteRenderer spriteRenderer = prototype2.AddComponent(new SpriteRenderer()) as SpriteRenderer;

            spriteRenderer.SetSprite("Projectiles\\Projectile2");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.50f;
        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            switch (type)
            {
                case ProjectileDirection.Horizontal:
                    gameObject = (GameObject)prototype1.Clone();
                    break;
                case ProjectileDirection.Vertical:
                    gameObject = (GameObject)prototype2.Clone();
                    break; 
            }


            Projectile projectile = gameObject.GetComponent<Projectile>() as Projectile; 
            Collider collider = gameObject.AddComponent(new Collider()) as Collider;
            collider.CollisionEvent.Attach(projectile); 

            return gameObject;
        }
    }
}
