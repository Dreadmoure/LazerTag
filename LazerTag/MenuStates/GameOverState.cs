using LazerTag.ComponentPattern;
using LazerTag.Domain;
using LazerTag.Repository.Repositories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    public class GameOverState : State
    {
        private Button saveButton; 

        private Texture2D title;
        private Vector2 titleOrigin;

        private string winner;
        private int winnerScore;

        private string winnerName; 

        /// <summary>
        /// contructor for GameOverState 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public GameOverState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            // set mouse visible 
            game.IsMouseVisible = true;

            winner = GameState.Winner.Type.ToString();
            winnerScore = GameState.Winner.Score;
            winnerName = winner; 

            Vector2 buttonPosition = new Vector2(GameWorld.ScreenSize.X/2, GameWorld.ScreenSize.Y / 2 + 300);
            saveButton = new Button(buttonPosition, "MainMenuButton");
        }

        /// <summary>
        /// loads elements 
        /// </summary>
        public override void LoadContent()
        {
            saveButton.LoadContent(content);

            // set title 
            title = content.Load<Texture2D>("Menus\\Titles\\GameOverTitle");
            titleOrigin = new Vector2(title.Width / 2, title.Height / 2);
        }

        /// <summary>
        /// updates elements 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            saveButton.Update(gameTime);

            if (saveButton.isClicked)
            {
                saveButton.isClicked = false;

                // save name and score to repository 
                SaveToRepository(); 

                game.ChangeState(GameWorld.Instance.MenuState); 
            }
        }

        /// <summary>
        /// method used to check and save score to repository 
        /// </summary>
        private void SaveToRepository()
        {
            GameWorld.HighScoreRepository.Open();

            // get all elements of the repository 
            List<HighScore> scores = GameWorld.HighScoreRepository.GetAllScores();

            // check if 10 elements present in repository 
            if(scores.Count() >= 10)
            {
                // check if score is lower than the score of the lowest of 10 elements 
                HighScore temp = scores.First();

                foreach (HighScore highscore in scores)
                {
                    if(temp.Score > highscore.Score)
                    {
                        temp = highscore; 
                    }
                }
                
                if(winnerScore > temp.Score)
                {
                    // update the repository 
                    GameWorld.HighScoreRepository.UpdateScore(temp.ID, winnerName, winnerScore);
                }
            }
            else
            {
                GameWorld.HighScoreRepository.AddScore(winnerName, winnerScore);
            }

            GameWorld.HighScoreRepository.Close();
        }

        /// <summary>
        /// draws elements to the screne 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            // draw title 
            spriteBatch.Draw(title, new Vector2(GameWorld.ScreenSize.X / 2, 300), null, Color.White, 0f, titleOrigin, 1f, SpriteEffects.None, 0.9f);

            saveButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
