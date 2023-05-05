using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.BuilderPattern
{
    public class PlatformBuilder : IBuilder
    {
        private GameObject gameObject;
        private Vector2 position; 

        public PlatformBuilder(int x, int y)
        {
            position = new Vector2(x, y); 
        }

        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();
        }

        /// <summary>
        /// adds components to the gameobject
        /// </summary>
        private void BuildComponents()
        {
            gameObject.AddComponent(new SpriteRenderer()); 

            Platform platform = gameObject.AddComponent(new Platform(position)) as Platform;
            Collider collider = gameObject.AddComponent(new Collider()) as Collider;

            gameObject.Tag = "Platform"; 
        }

        public GameObject GetResult()
        {
            return gameObject; 
        }
    }
}
