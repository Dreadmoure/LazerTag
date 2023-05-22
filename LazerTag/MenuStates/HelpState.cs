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

        public HelpState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            Vector2 backButtonPosition = new Vector2(200, 100);

            backButton = new Button(backButtonPosition, "GreenBackButton");
        }

        public override void LoadContent()
        {
            backButton.LoadContent(content);
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

            spriteBatch.End();
        }
    }
}
