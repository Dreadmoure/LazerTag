using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    public class SettingsState : State
    {
        #region fields 
        private Button backButton;
        private Button setMusic;
        private Button setFx;
        private Texture2D title;
        private Vector2 titleOrigin;

        private List<Button> buttons;

        private SpriteFont font;
        #endregion

        /// <summary>
        /// constructor for SettingsState - sends parameters to base State 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public SettingsState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 3);

            setMusic = new Button(position + new Vector2(200, 200), "OnButton", "OffButton");
            setFx = new Button(position + new Vector2(200, 300), "OnButton", "OffButton");

            Vector2 backButtonPosition = new Vector2(200, 100);

            backButton = new Button(backButtonPosition, "GreenBackButton");

            buttons = new List<Button>() { setMusic, setFx, backButton };
        }

        #region methods 
        public override void LoadContent()
        {
            title = content.Load<Texture2D>("Menus\\Titles\\OptionsTitle");
            titleOrigin = new Vector2(title.Width / 2, title.Height / 2);

            foreach (Button button in buttons)
            {
                button.LoadContent(content);
            }

            // set font 
            font = content.Load<SpriteFont>("Fonts\\GameOverFont");
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (SoundMixer.Volume < 1.0f)
                {
                    // increase sound volume 
                    SoundMixer.Volume += 0.1f;
                    SoundMixer.Volume = (float)Math.Round(SoundMixer.Volume, 1);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (SoundMixer.Volume >= 0.1f)
                {
                    // decrease sound volume 
                    SoundMixer.Volume -= 0.1f;
                    SoundMixer.Volume = (float)Math.Round(SoundMixer.Volume, 1);
                } 
            }

            if (setMusic.isClicked)
            {
                setMusic.isClicked = false;

                SoundMixer.MusicOn = !SoundMixer.MusicOn;
                SoundMixer.Instance.SetMusic();
                setMusic.SwitchButton(SoundMixer.MusicOn); 
            }

            if (setFx.isClicked)
            {
                setFx.isClicked = false;

                SoundMixer.SoundEffectsOn = !SoundMixer.SoundEffectsOn;
                setFx.SwitchButton(SoundMixer.SoundEffectsOn);
            }

            if (backButton.isClicked)
            {
                backButton.isClicked = false;
                game.ChangeState(GameWorld.Instance.MenuState);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            // draw title 
            spriteBatch.Draw(title, new Vector2(GameWorld.ScreenSize.X / 2, 300), null, Color.White, 0f, titleOrigin, 1f, SpriteEffects.None, 0.9f);

            foreach (Button button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            // print instruction text 
            Vector2 position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 3);

            string volume = "Change volume of music and sound effects:";
            string leftArrow = "Left arrow: turn down";
            string rightArrow = "Right arrow: turn up";

            float volumeX = font.MeasureString(volume).X / 2;

            spriteBatch.DrawString(font, volume, position + new Vector2(-50, 0), Color.White, 0f, new Vector2(volumeX, 0), 0.5f, SpriteEffects.None, 0.9f);
            spriteBatch.DrawString(font, leftArrow, position + new Vector2(50, 50), Color.White, 0f, new Vector2(volumeX, 0), 0.5f, SpriteEffects.None, 0.9f);
            spriteBatch.DrawString(font, rightArrow, position + new Vector2(50, 100), Color.White, 0f, new Vector2(volumeX, 0), 0.5f, SpriteEffects.None, 0.9f);

            int percent = (int)(SoundMixer.Volume * 100); 
            string currentVolume = percent.ToString() + "%";
            spriteBatch.DrawString(font, currentVolume, position + new Vector2(200, 50), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

            string music = "Turn music on or off: ";
            float musicX = font.MeasureString(music).X;
            spriteBatch.DrawString(font, music, position + new Vector2(-300, 200), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.9f);

            string sfx = "Turn sound effects on or off: ";
            float sfxX = font.MeasureString(sfx).X;
            spriteBatch.DrawString(font, sfx, position + new Vector2(-300, 300), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.9f);

            spriteBatch.End();
        }
        #endregion
    }
}
