﻿using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.BuilderPattern
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;
        private int id;

        /// <summary>
        /// constructor which takes 1 parameter
        /// </summary>
        /// <param name="id">the id for which player it should be</param>
        public PlayerBuilder(int id)
        {
            this.id = id;
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
            Player player = (Player)gameObject.AddComponent(new Player());
            player.Type = (PlayerIndex)id;
            

            UIRenderer uIRenderer = (UIRenderer)gameObject.AddComponent(new UIRenderer());

            if(player.Type == PlayerIndex.One)
            {
                uIRenderer.SetSprite("PlayerBoxes\\RedPlayerBox", new Vector2(GameWorld.ScreenSize.X / 5, GameWorld.ScreenSize.Y / 12));
                
            }
            else if (player.Type == PlayerIndex.Two)
            {
                uIRenderer.SetSprite("PlayerBoxes\\BluePlayerBox", new Vector2((GameWorld.ScreenSize.X / 5)*2, GameWorld.ScreenSize.Y / 12));

            }
            else if (player.Type == PlayerIndex.Three)
            {
                uIRenderer.SetSprite("PlayerBoxes\\GreenPlayerBox", new Vector2((GameWorld.ScreenSize.X / 5)*3, GameWorld.ScreenSize.Y / 12));

            }
            else if (player.Type == PlayerIndex.Four)
            {
                uIRenderer.SetSprite("PlayerBoxes\\PinkPlayerBox", new Vector2((GameWorld.ScreenSize.X / 5)*4, GameWorld.ScreenSize.Y / 12));

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
