using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class TextBox
    {
        #region fields 
        private SpriteFont textFont;
        private string text;
        private Vector2 boxPosition;
        private Texture2D boxTexture; 

        private KeyboardState currentKeyState; 
        private KeyboardState previousKeyState;
        #endregion

        #region properties 
        private Vector2 GetSpriteSize
        {
            get
            {
                return new Vector2(boxTexture.Width, boxTexture.Height);
            }
        }

        private Rectangle GetRectangle
        {
            get
            {
                return new Rectangle(
                                     (int)(boxPosition.X - GetSpriteSize.X),
                                     (int)(boxPosition.Y - GetSpriteSize.Y), 
                                     (int)GetSpriteSize.X, 
                                     (int)GetSpriteSize.Y
                                     );
            }
        }

        private Vector2 GetOrigin
        {
            get
            {
                return new Vector2(boxTexture.Width / 2, boxTexture.Height / 2);
            }
        }

        public string GetTextEntered
        {
            get
            {
                if(string.Equals(text, "Enter name"))
                {
                    return "";
                }
                else
                {
                    return text; 
                }
            }
        }
        #endregion

        public TextBox(Vector2 position)
        {
            this.boxPosition = position;
            text = "";
        }

        #region methods 
        public void LoadContent(ContentManager content)
        {
            // load font 
            textFont = content.Load<SpriteFont>("Fonts\\LifeFont");

            // load text field texture 
            boxTexture = content.Load<Texture2D>("Menus\\GoldTextBox");
        }

        public void Update()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            // check if text is empty 
            if(string.Equals(text, ""))
            {
                text = "Enter name";
            }

            // make sure user can not hold down key 
            if(currentKeyState != previousKeyState)
            {
                if (string.Equals(text, "Enter name"))
                {
                    text = "";
                }

                // delete letters at end of text 
                if(text.Length > 0 && currentKeyState.IsKeyDown(Keys.Back))
                {
                    text = text.Remove(text.Length - 1); 
                }

                // check that text is within a limit 
                if(text.Length < 8)
                {
                    foreach (var key in currentKeyState.GetPressedKeys())
                    {
                        string keyValue = key.ToString();

                        // validate input 
                        if (AllowedInput(keyValue) && keyValue.Length <= 1)
                        {
                            text += keyValue; 
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string t = "Enter name to save to highscore: ";
            float tX = textFont.MeasureString(t).X/2;
            spriteBatch.DrawString(textFont, t, boxPosition - new Vector2(0, 75), Color.White, 0, new Vector2(tX, 0), 0.7f, SpriteEffects.None, 0.6f);

            spriteBatch.Draw(boxTexture, boxPosition, null, Color.White, 0, GetOrigin, 1f, SpriteEffects.None, 0.5f);

            if (!string.IsNullOrEmpty(text))
            {
                float x = (GetRectangle.X + GetRectangle.Width / 2) - textFont.MeasureString(text).X / 2 + GetOrigin.X; 
                float y = (GetRectangle.Y + GetRectangle.Height / 2) - textFont.MeasureString(text).Y / 2 + GetOrigin.Y;

                spriteBatch.DrawString(textFont, text, new Vector2(x, y), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.6f);
            }
        }

        /// <summary>
        /// method that validates the input, letters can only be letters from A-Z, automaticly all caps 
        /// </summary>
        /// <param name="s">the string that should be validated</param>
        /// <returns>true if string is letters from A-Z</returns>
        private bool AllowedInput(string s)
        {
            Regex regex = new Regex("[A-Z]");

            Match match = regex.Match(s);

            return match.Success;
        }
        #endregion
    }
}
