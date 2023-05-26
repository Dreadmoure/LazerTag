using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
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
    public class Button
    {
        #region Fields 
        private Texture2D buttonTexture;
        private Texture2D buttonOnTexture;
        private Texture2D buttonOffTexture;
        private string textureName;
        private string textureOnName;
        private string textureOffName;

        private float scale;
        private float layer;
        private Color color;
        private bool colorShiftDown;

        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public bool isClicked;

        private bool isSwitch;
        private bool isOn = true; 
        #endregion

        #region Properties 
        /// <summary>
        /// property for button position 
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Property to get the size of the button sprite texture 
        /// </summary>
        private Vector2 GetSpriteSize
        {
            get { return new Vector2(buttonTexture.Width * scale, buttonTexture.Height * scale); }
        }
        /// <summary>
        /// Property to get the origin/the center of the button 
        /// </summary>
        private Vector2 GetOrigin
        {
            get { return new Vector2(buttonTexture.Width / 2, buttonTexture.Height / 2); }
        }
        /// <summary>
        /// Property to get the rectangle - used when mouse collides with button 
        /// </summary>
        private Rectangle GetRectangle
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - GetSpriteSize.X / 2),
                    (int)(Position.Y - GetSpriteSize.Y / 2),
                    (int)GetSpriteSize.X,
                    (int)GetSpriteSize.Y
                    );
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for Button - sets its initial variables 
        /// </summary>
        /// <param name="position">the position at which the button should be placed</param>
        /// <param name="textureName">the texture name that the button should have</param>
        public Button(Vector2 position, string textureName)
        {
            Position = position;
            this.textureName = textureName;
            color = Color.White;
            layer = 0.96f;
            scale = 1f;
            isSwitch = false; 
        }

        public Button(Vector2 position, string textureOnName, string textureOffName)
        {
            Position = position;
            this.textureName = textureOnName;
            this.textureOnName = textureOnName;
            this.textureOffName = textureOffName;
            color = Color.White;
            layer = 0.96f;
            scale = 1f;
            isSwitch = true; 
        }
        #endregion

        #region Methods
        public void LoadContent(ContentManager content)
        {
            if(!string.IsNullOrEmpty(textureName))
            {
                buttonTexture = content.Load<Texture2D>($"Menus\\Buttons\\{textureName}");
            }
            
            if(!string.IsNullOrEmpty(textureOnName))
            {
                buttonOnTexture = content.Load<Texture2D>($"Menus\\Buttons\\{textureOnName}");
            }
            
            if(!string.IsNullOrEmpty(textureOffName))
            {
                buttonOffTexture = content.Load<Texture2D>($"Menus\\Buttons\\{textureOffName}");
            }
            
        }

        public void Update(GameTime gameTime)
        {
            // update mouse states 
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            // set rectangle for mouse position 
            Rectangle mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            // when mouse hovers over button 
            if (mouseRectangle.Intersects(GetRectangle))
            {
                ColorShift();

                // when button gets clicked 
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    color.A = 255;

                    // play sound for button clicked 
                    SoundMixer.Instance.ButtonFx(); 
                }
            }
            else if (color.A < 255)
            {
                color.A += 3;
            }
        }

        /// <summary>
        /// Makes the button shift its color opacity 
        /// </summary>
        private void ColorShift()
        {
            if (color.A == 255)
            {
                colorShiftDown = false;
            }
            if (color.A == 0)
            {
                colorShiftDown = true;
            }
            if (colorShiftDown)
            {
                color.A += 3;
            }
            else
            {
                color.A -= 3;
            }
        }

        public void SwitchButton(bool isOn)
        {
            this.isOn = isOn; 
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (buttonTexture != null && !isSwitch)
            {
                spriteBatch.Draw(buttonTexture, Position, null, color, 0f, GetOrigin, scale, SpriteEffects.None, layer);
            }

            if (buttonOnTexture != null && isOn && isSwitch)
            {
                spriteBatch.Draw(buttonOnTexture, Position, null, color, 0f, GetOrigin, scale, SpriteEffects.None, layer);
            }

            if (buttonOffTexture != null && !isOn && isSwitch)
            {
                spriteBatch.Draw(buttonOffTexture, Position, null, color, 0f, GetOrigin, scale, SpriteEffects.None, layer);
            }
        }
        #endregion
    }
}
