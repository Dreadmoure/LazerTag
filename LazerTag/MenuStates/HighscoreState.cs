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
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class HighscoreState : State
    {
        private Button backButton;
        private static List<HighScore> highScoreResults;
        private SpriteFont font;
        private Texture2D title;
        private Vector2 titleOrigin;

        /// <summary>
        /// constructor for HighscoreState - sends parameters to base State 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public HighscoreState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 backButtonPosition = new Vector2(200, 100);

            backButton = new Button(backButtonPosition, "BlueBackButton");

            highScoreResults = new List<HighScore>();
        }

        /// <summary>
        /// method for loading content 
        /// </summary>
        public override void LoadContent()
        {
            backButton.LoadContent(content);

            font = content.Load<SpriteFont>("Fonts\\LifeFont");

            // set title 
            title = content.Load<Texture2D>("Menus\\Titles\\HighScoreTitle");
            titleOrigin = new Vector2(title.Width / 2, title.Height / 2);

            // handle database for HighScores 
            GameWorld.HighScoreRepository.Open();
            highScoreResults = GameWorld.HighScoreRepository.GetAllScores();
            GameWorld.HighScoreRepository.Close();

            // sort the list 
            QuickSortRecursive(ref highScoreResults, 0, highScoreResults.Count -1);
        }

        /// <summary>
        /// method for updating 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            backButton.Update(gameTime);

            if (backButton.isClicked)
            {
                backButton.isClicked = false;
                game.ChangeState(GameWorld.Instance.MenuState);
            }
        }

        /// <summary>
        /// method for drawing 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            // draw title 
            spriteBatch.Draw(title, new Vector2(GameWorld.ScreenSize.X / 2, 300), null, Color.White, 0f, titleOrigin, 1f, SpriteEffects.None, 0.9f);

            backButton.Draw(gameTime, spriteBatch);

            int id = 1; 
            int yOffset = 0;

            foreach (HighScore highScore in highScoreResults)
            {
                string name = highScore.Name;
                string score = highScore.Score.ToString();

                float idOffsetX = font.MeasureString(id.ToString()).X;
                float scoreOffsetX = font.MeasureString(score).X;

                Vector2 position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2.5f + yOffset);

                spriteBatch.DrawString(font, id.ToString(), position + new Vector2(-200 -idOffsetX, 0), Color.White);
                spriteBatch.DrawString(font, name, position + new Vector2(-180, 0), Color.White);
                spriteBatch.DrawString(font, score, position + new Vector2(200 - scoreOffsetX, 0), Color.White);

                id++; 
                yOffset += 50;
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Optimized recursive quicksort algorithm
        /// Choses an element from the list which is then set as the pivot point.
        /// Sorts the elements from largest to smallest where the pivot point is between them.
        /// </summary>
        /// <param name="data">the list of highscores we need sorted</param>
        /// <param name="leftIndex">left is the index of the leftmost element of the sublist</param>
        /// <param name="rightIndex">right is the index of the leftmost element of the sublist</param>
        private void QuickSortRecursive(ref List<HighScore> data, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                //creates a partition 
                int storedIndex = Partition(ref data, leftIndex, rightIndex);
                //calls quicksort recursively on the left partition
                QuickSortRecursive(ref data, leftIndex, storedIndex - 1);
                //calls quicksort recursively on the right partition
                QuickSortRecursive(ref data, storedIndex + 1, rightIndex);
            }
        }

        /// <summary>
        /// Method for making a partition of the list depending on left and right
        /// </summary>
        /// <param name="data">list of scores we give it</param>
        /// <param name="leftIndex">left is the index of the leftmost element of the sublist</param>
        /// <param name="rightIndex">right is the index of the leftmost element of the sublist</param>
        /// <returns>returns the index it has reached</returns>
        private int Partition(ref List<HighScore> data, int leftIndex, int rightIndex)
        {
            //sets the pivot point
            HighScore pivot = data[rightIndex];
            HighScore temp;
            int i = leftIndex;

            //loops through the data list
            for (int j = leftIndex; j < rightIndex; ++j)
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
            data[rightIndex] = data[i];
            data[i] = pivot;

            return i;
        }

    }
}
