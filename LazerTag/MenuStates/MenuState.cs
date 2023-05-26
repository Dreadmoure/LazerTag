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
    /// <summary>
    /// Forfatter : Ida
    /// </summary>
    public class MenuState : State
    {
        private Texture2D title;
        private Vector2 titleOrigin; 
        private List<Button> buttons;
        private Button playButton;
        private Button highscoreButton;
        private Button settingsButton;
        private Button quitButton; 

        /// <summary>
        /// constructor for MenuState - sends parameters to base State 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 buttonPosition = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);

            Color buttonColor = Color.White; 

            playButton = new Button(buttonPosition, "NewGameButton");
            highscoreButton = new Button(buttonPosition + new Vector2(0, 100), "HighScoreButton");
            settingsButton = new Button(buttonPosition + new Vector2(0, 200), "OptionsButton");
            quitButton = new Button(buttonPosition + new Vector2(0, 300), "QuitGameButton");

            buttons = new List<Button>() { playButton, highscoreButton, settingsButton, quitButton };
        }

        #region Methods 
        /// <summary>
        /// method to load all content 
        /// </summary>
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

        /// <summary>
        /// method for updating menu 
        /// </summary>
        /// <param name="gameTime"></param>
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
            if (settingsButton.isClicked)
            {
                settingsButton.isClicked = false;

                // change state to HelpState
                game.ChangeState(GameWorld.Instance.SettingsState);
            }
            if (quitButton.isClicked)
            {
                quitButton.isClicked = false;

                // exit game 
                game.Exit(); 
            }
        }

        /// <summary>
        /// draws all elements in the menu 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
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
