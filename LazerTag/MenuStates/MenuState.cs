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
    public class MenuState : State
    {
        private Texture2D menuBackground;
        private List<Button> buttons;
        private Button playButton;
        private Button highscoreButton;
        private Button helpButton;
        private Button quitButton; 

        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 buttonPosition = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);

            Color buttonColor = Color.White; 

            playButton = new Button(buttonPosition, "Play", buttonColor);
            highscoreButton = new Button(buttonPosition + new Vector2(0, 100), "Highscore", buttonColor);
            helpButton = new Button(buttonPosition + new Vector2(0, 200), "Help", buttonColor);
            quitButton = new Button(buttonPosition + new Vector2(0, 300), "Quit", buttonColor);

            buttons = new List<Button>() { playButton, highscoreButton, helpButton, quitButton };
        }

        #region Methods 
        public override void LoadContent()
        {
            // set mouse visible 
            game.IsMouseVisible = true;

            //menuBackground = content.Load<Texture2D>("");

            foreach (Button button in buttons)
            {
                button.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

            if (playButton.isClicked)
            {
                playButton.isClicked = false;

                // change state to GameState
                game.ChangeState(new GameState(content, graphicsDevice, game));
            }
            if (highscoreButton.isClicked)
            {
                highscoreButton.isClicked = false;

                // change state to HighscoreState
                game.ChangeState(GameWorld.Instance.HighscoreState);
            }
            if (helpButton.isClicked)
            {
                helpButton.isClicked = false;

                // change state to HelpState
                game.ChangeState(GameWorld.Instance.HelpState);
            }
            if (quitButton.isClicked)
            {
                quitButton.isClicked = false;

                // exit game 
                game.Exit(); 
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch); 
            }

            spriteBatch.End();
        }
        #endregion
    }
}
