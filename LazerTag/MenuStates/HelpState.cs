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
    public class HelpState : State
    {
        private Button backButton;
        private Texture2D title;
        private Vector2 titleOrigin;

        public HelpState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 backButtonPosition = new Vector2(200, 100);

            backButton = new Button(backButtonPosition, "GreenBackButton");
        }

        public override void LoadContent()
        {
            backButton.LoadContent(content);

            // set title 
            title = content.Load<Texture2D>("Menus\\Titles\\HowToPlayTitle");
            titleOrigin = new Vector2(title.Width / 2, title.Height / 2);
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

            // draw title 
            spriteBatch.Draw(title, new Vector2(GameWorld.ScreenSize.X / 2, 300), null, Color.White, 0f, titleOrigin, 1f, SpriteEffects.None, 0.9f);

            backButton.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
