using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
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
            player.Life = 4;

            UIRenderer uIRenderer = (UIRenderer)gameObject.AddComponent(new UIRenderer());

            if(player.Id == 1)
            {
                uIRenderer.SetSprite("PlayerBoxes\\RedPlayerBox", new Vector2(GameWorld.ScreenSize.X / 8, GameWorld.ScreenSize.Y / 12));
                
            }
            else if (player.Id == 2)
            {
                uIRenderer.SetSprite("PlayerBoxes\\BluePlayerBox", new Vector2(GameWorld.ScreenSize.X / 3, GameWorld.ScreenSize.Y / 12));

            }
            else if (player.Id == 3)
            {
                uIRenderer.SetSprite("PlayerBoxes\\GreenPlayerBox", new Vector2(GameWorld.ScreenSize.X / 1.8f, GameWorld.ScreenSize.Y / 12));

            }
            else if (player.Id == 4)
            {
                uIRenderer.SetSprite("PlayerBoxes\\PinkPlayerBox", new Vector2(GameWorld.ScreenSize.X / 1.3f, GameWorld.ScreenSize.Y / 12));

            }

            uIRenderer.SetLifeText(player.Life);
            uIRenderer.SetScoreText(player.Score);
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
