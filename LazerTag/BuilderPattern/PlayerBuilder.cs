using LazerTag.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.BuilderPattern
{
    public class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;
        private int id;

        public PlayerBuilder(int id)
        {
            this.id = id;
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
            Player player = (Player)gameObject.AddComponent(new Player());
            player.Id = id;
        }

        /// <summary>
        /// Method for returning the gameobject
        /// </summary>
        /// <returns>GameObject</returns>
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
