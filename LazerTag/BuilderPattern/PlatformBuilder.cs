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
        private int platformID;

        public PlatformBuilder(int x, int y, int platformID)
        {
            position = new Vector2(x, y);
            this.platformID = platformID;
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

            Platform platform = gameObject.AddComponent(new Platform(position, platformID)) as Platform;
            Collider collider = gameObject.AddComponent(new Collider()) as Collider;

            gameObject.Tag = "Platform"; 
        }

        public GameObject GetResult()
        {
            return gameObject; 
        }
    }
}
