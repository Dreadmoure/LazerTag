using LazerTag.Domain;
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

    }
}
