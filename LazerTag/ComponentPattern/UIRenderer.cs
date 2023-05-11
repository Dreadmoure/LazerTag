﻿using System;
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
        #region fields
        private SpriteFont spriteFont;
        #endregion

        #region properties
        /// <summary>
        /// Property for getting or setting the sprite
        /// </summary>
        public Texture2D Sprite { get; private set; }

        /// <summary>
        /// Property for getting or setting the AmmoCountSprite
        /// </summary>
        public Texture2D AmmoCountSprite { get; private set; }

        /// <summary>
        /// Property for setting the position of the sprite
        /// </summary>
        public Vector2 SpritePosition { get; private set; }

        /// <summary>
        /// Property for setting the position of AmmoCountSprite
        /// </summary>
        public Vector2 AmmoCountSpritePosition { get; private set; }

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
        public string LifeText { get; private set; }

        /// <summary>
        /// property for getting or setting the text
        /// </summary>
        public string ScoreText { get; private set; }
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
            if (AmmoCountSprite != null)
            {
                Origin = new Vector2(AmmoCountSprite.Width / 2, AmmoCountSprite.Height / 2);
                Scale = 1f;
            }

            LayerDepth = 0.98f;
            spriteFont = GameWorld.Instance.Content.Load<SpriteFont>("Fonts\\LifeFont");

            // ammo counter 

        }

        /// <summary>
        /// method for updating the LifeText
        /// </summary>
        /// <param name="playerLife">Updates the life of the player</param>
        public void SetLifeText(int life)
        {
            LifeText = life.ToString();
            LifeTextPosition = new Vector2(SpritePosition.X - 110, SpritePosition.Y - 5);
        }

        /// <summary>
        /// method for updating the ScoreText
        /// </summary>
        /// <param name="score"></param>
        public void SetScoreText(int score)
        {
            ScoreText = score.ToString();
            ScoreTextPosition = new Vector2(SpritePosition.X, SpritePosition.Y);
        }

        /// <summary>
        /// method for setting the sprite based on the input string and a position
        /// </summary>
        /// <param name="spriteName">the path of the sprite</param>
        /// <param name="position">the position it needs to be at</param>
        public void SetSprite(string spriteName, Vector2 position)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            SpritePosition = position;
        }

        /// <summary>
        /// method for setting the sprite based on the input string and a position
        /// </summary>
        /// <param name="ammoCount">the path of the sprite</param>
        /// <param name="position">the position it needs to be at</param>
        public void SetAmmoCountSprite(int ammoCount, Vector2 position)
        {
            if(ammoCount == 5)
            {
                AmmoCountSprite = GameWorld.Instance.Content.Load<Texture2D>("AmmoCounter\\AmmoCount5");

                float newPosX = position.X + 142;
                float newPosY = position.Y - 5;

                Vector2 newPos = new Vector2(newPosX, newPosY);

                AmmoCountSpritePosition = newPos;
            }
            else if(ammoCount == 4)
            {
                AmmoCountSprite = GameWorld.Instance.Content.Load<Texture2D>("AmmoCounter\\AmmoCount4");

                float newPosX = position.X + 147;
                float newPosY = position.Y - 5;

                Vector2 newPos = new Vector2(newPosX, newPosY);

                AmmoCountSpritePosition = newPos;

            }
            else if (ammoCount == 3)
            {
                AmmoCountSprite = GameWorld.Instance.Content.Load<Texture2D>("AmmoCounter\\AmmoCount3");

                float newPosX = position.X + 152;
                float newPosY = position.Y - 5;

                Vector2 newPos = new Vector2(newPosX, newPosY);

                AmmoCountSpritePosition = newPos;
            }
            else if (ammoCount == 2)
            {
                AmmoCountSprite = GameWorld.Instance.Content.Load<Texture2D>("AmmoCounter\\AmmoCount2");

                float newPosX = position.X + 157;
                float newPosY = position.Y - 5;

                Vector2 newPos = new Vector2(newPosX, newPosY);

                AmmoCountSpritePosition = newPos;
            }
            else if (ammoCount == 1)
            {
                AmmoCountSprite = GameWorld.Instance.Content.Load<Texture2D>("AmmoCounter\\AmmoCount1");

                float newPosX = position.X + 162;
                float newPosY = position.Y - 5;

                Vector2 newPos = new Vector2(newPosX, newPosY);

                AmmoCountSpritePosition = newPos;
            }
            else
            {
                AmmoCountSprite = null;
            }
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
            if (AmmoCountSprite != null)
            {
                spriteBatch.Draw(AmmoCountSprite, AmmoCountSpritePosition, null, Color.White, 0f, Origin, Scale, SpriteEffects.None, LayerDepth);
            }

            //draw lifeText
            spriteBatch.DrawString(spriteFont, LifeText, LifeTextPosition, Color.White, 0f, lifeTextOrigin, 1f, SpriteEffects.None, LayerDepth + 0.01f);
            //draw scoreText
            spriteBatch.DrawString(spriteFont, ScoreText, ScoreTextPosition, Color.White, 0f, scoreTextOrigin, 1f, SpriteEffects.None, LayerDepth + 0.01f);
        }
        #endregion
    }
}
