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
        private Texture2D title;
        private Vector2 titleOrigin; 
        //private Texture2D menuBackground;
        //private Vector2 backgroundOrigin; 
        private List<Button> buttons;
        private Button playButton;
        private Button highscoreButton;
        private Button helpButton;
        private Button quitButton; 

        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 buttonPosition = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);

            Color buttonColor = Color.White; 

            playButton = new Button(buttonPosition, "NewGameButton");
            highscoreButton = new Button(buttonPosition + new Vector2(0, 100), "HighScoreButton");
            helpButton = new Button(buttonPosition + new Vector2(0, 200), "HowToPlayButton");
            quitButton = new Button(buttonPosition + new Vector2(0, 300), "QuitGameButton");

            buttons = new List<Button>() { playButton, highscoreButton, helpButton, quitButton };
        }

        #region Methods 
        public override void LoadContent()
        {
            // set mouse visible 
            game.IsMouseVisible = true;

            // set title 
            title = content.Load<Texture2D>("Menus\\Titles\\Title");
            titleOrigin = new Vector2(title.Width/2, title.Height/2);
            
            // set background 
            //menuBackground = content.Load<Texture2D>("Menus\\MenuBackground");
            //backgroundOrigin = new Vector2(menuBackground.Width/2, menuBackground.Height/2);

            foreach (Button button in buttons)
            {
                button.LoadContent(content);
            }

            // load music 
            SoundMixer.Instance.PlayMenuMusic(); 
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
                game.ChangeState(GameWorld.Instance.LockInState);
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

            // draw title 
            spriteBatch.Draw(title, new Vector2(GameWorld.ScreenSize.X/2, 300), null, Color.White, 0f, titleOrigin, 1f, SpriteEffects.None, 0.9f);
            
            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch); 
            }

            spriteBatch.End();
        }
        #endregion
    }
}
