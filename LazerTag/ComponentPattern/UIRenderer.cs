using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class UIRenderer : Component
    {
        private SpriteFont spriteFont;

        #region properties
        /// <summary>
        /// Property for getting or setting the sprite
        /// </summary>
        public Texture2D Sprite { get; private set; }

        /// <summary>
        /// Property for setting the position of the sprite
        /// </summary>
        public Vector2 SpritePosition { get; private set; }

        /// <summary>
        /// Property for setting the position of the text
        /// </summary>
        public Vector2 LifeTextPosition { get; private set; }

        /// <summary>
        /// Property for setting the position of the text
        /// </summary>
        public Vector2 ScoreTextPosition { get; private set; }

        /// <summary>
        /// Property for getting or setting the origin of the sprite
        /// </summary>
        public Vector2 Origin { get; private set; }

        /// <summary>
        /// Property for getting or setting the layerdepth of the image
        /// </summary>
        public float LayerDepth { get; set; }

        /// <summary>
        /// property for getting or setting the scale of the image
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// property for getting or setting the text
        /// </summary>
        public string LifeText { get; set; }

        /// <summary>
        /// property for getting or setting the text
        /// </summary>
        public string ScoreText { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// method for setting properties and loading stuff from the start
        /// </summary>
        public override void Start()
        {
            if(Sprite != null)
            {
                Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
                Scale = 1f;
            }

            LayerDepth = 0.98f;
            spriteFont = GameWorld.Instance.Content.Load<SpriteFont>("Fonts\\LifeFont");
        }

        /// <summary>
        /// method for updating the UI elements
        /// </summary>
        /// <param name="playerLife">Updates the life of the player</param>
        public void SetLifeText(int life)
        {
            LifeText = life.ToString();
            LifeTextPosition = new Vector2(SpritePosition.X - 110, SpritePosition.Y - 5);
        }

        public void SetScoreText(int score)
        {
            ScoreText = score.ToString();
            ScoreTextPosition = new Vector2(SpritePosition.X, SpritePosition.Y);
        }

        /// <summary>
        /// method for setting the sprite based on the input string
        /// </summary>
        /// <param name="spriteName">path and name of the file</param>
        public void SetSprite(string spriteName, Vector2 position)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            SpritePosition = position;
        }

        /// <summary>
        /// method for drawing a sprite to the screen
        /// </summary>
        /// <param name="spriteBatch">passed in from gameworld so we can draw through it</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            float lifeTextX = spriteFont.MeasureString(LifeText).X / 2;
            float lifeTextY = spriteFont.MeasureString(LifeText).Y / 2;

            Vector2 lifeTextOrigin = new Vector2(lifeTextX, lifeTextY);

            float scoreTextX = spriteFont.MeasureString(ScoreText).X / 2;
            float scoreTextY = spriteFont.MeasureString(ScoreText).Y / 2;

            Vector2 scoreTextOrigin = new Vector2(scoreTextX, scoreTextY);

            //draw a sprite
            if (Sprite != null)
            {
                spriteBatch.Draw(Sprite, SpritePosition, null, Color.White, 0f, Origin, Scale, SpriteEffects.None, LayerDepth);
            }
            
            //draw lifeText
            spriteBatch.DrawString(spriteFont, LifeText, LifeTextPosition, Color.White, 0f, lifeTextOrigin, 1f, SpriteEffects.None, LayerDepth + 0.01f);
            //draw scoreText
            spriteBatch.DrawString(spriteFont, ScoreText, ScoreTextPosition, Color.White, 0f, scoreTextOrigin, 1f, SpriteEffects.None, LayerDepth + 0.01f);
        }
        #endregion
    }
}
