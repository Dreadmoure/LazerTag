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

        /// <summary>
        /// constructor which takes 3 parameters 
        /// </summary>
        /// <param name="x">the x coordinate for the poisition</param>
        /// <param name="y">the y coordinate for the poisition</param>
        /// <param name="platformID">the id for which type of platform to create</param>
        public PlatformBuilder(int x, int y, int platformID)
        {
            position = new Vector2(x, y);
            this.platformID = platformID;
        }

        /// <summary>
        /// method for building the gameobject 
        /// </summary>
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
            if(platformID == 1 || platformID == 2)
            {
                Collider collider = gameObject.AddComponent(new Collider()) as Collider;
            }

            gameObject.Tag = "Platform"; 
        }

        /// <summary>
        /// method for getting the built gameobject 
        /// </summary>
        /// <returns>the gameobject that was built</returns>
        public GameObject GetResult()
        {
            return gameObject; 
        }
    }
}
