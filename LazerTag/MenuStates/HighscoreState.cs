﻿using LazerTag.Domain;
using LazerTag.Repository.Mapper;
using LazerTag.Repository.Repositories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    public class HighscoreState : State
    {
        private Button backButton;
        private static List<HighScore> highScoreResults;
        private SpriteFont font;

        public HighscoreState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 backButtonPosition = new Vector2(200, 100);

            backButton = new Button(backButtonPosition, "Back", Color.White);

            highScoreResults = new List<HighScore>();
        }

        public override void LoadContent()
        {
            backButton.LoadContent(content);

            font = content.Load<SpriteFont>("Fonts\\LifeFont");

            GameWorld.HighScoreRepository.Open();

            highScoreResults = GameWorld.HighScoreRepository.GetAllScores();

            GameWorld.HighScoreRepository.Close();

            QuickSortRecursive(ref highScoreResults, 0, highScoreResults.Count -1);
        }

        public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);

            if (backButton.isClicked)
            {
                backButton.isClicked = false;
                game.ChangeState(GameWorld.Instance.MenuState);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            backButton.Draw(gameTime, spriteBatch);

            int i = 0;

            foreach (HighScore highScore in highScoreResults)
            {
                string name = highScore.Name;

                int score = highScore.Score;

                spriteBatch.DrawString(font, name, new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2 + i), Color.White);
                spriteBatch.DrawString(font, score.ToString(), new Vector2(GameWorld.ScreenSize.X / 2 + 100, GameWorld.ScreenSize.Y / 2 + i), Color.White);
                i += 50;
            }


            spriteBatch.End();
        }

        /// <summary>
        /// Optimized recursive quicksort algorithm
        /// Choses an element from the list which is then set as the pivot point.
        /// Sorts the elements from largest to smallest where the pivot point is between them.
        /// </summary>
        /// <param name="data">the list of highscores we need sorted</param>
        /// <param name="left">left is the index of the leftmost element of the sublist</param>
        /// <param name="right">right is the index of the leftmost element of the sublist</param>
        private void QuickSortRecursive(ref List<HighScore> data, int left, int right)
        {
            if (left < right)
            {
                //creates a partition at the pivotpoint
                int q = Partition(ref data, left, right);
                //calls quicksort recursively on the left partition
                QuickSortRecursive(ref data, left, q - 1);
                //calls quicksort recursively on the right partition
                QuickSortRecursive(ref data, q + 1, right);
            }
        }

        /// <summary>
        /// Method for making a partition of the list depending on left and right
        /// </summary>
        /// <param name="data">list of scores we give it</param>
        /// <param name="left">left is the index of the leftmost element of the sublist</param>
        /// <param name="right">right is the index of the leftmost element of the sublist</param>
        /// <returns>returns the index it has reached</returns>
        private int Partition(ref List<HighScore> data, int left, int right)
        {
            //sets the pivot point
            HighScore pivot = data[right];
            HighScore temp;
            int i = left;

            //loops through the data list
            for (int j = left; j < right; ++j)
            {
                //compares each data against the pivot point
                if (data[j].Score >= pivot.Score) // >= makes sure it is from largest to smallest
                {
                    temp = data[j];
                    data[j] = data[i];
                    data[i] = temp;
                    i++;
                }
            }

            //swaps the data
            data[right] = data[i];
            data[i] = pivot;

            return i;
        }

    }
}
