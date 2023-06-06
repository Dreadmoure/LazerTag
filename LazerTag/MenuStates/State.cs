using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    public abstract class State
    {
        protected ContentManager content;
        protected GraphicsDevice graphicsDevice;
        protected GameWorld game;

        /// <summary>
        /// Constructor for State - sets the initial variables 
        /// </summary>
        /// <param name="content">The ContentManager</param>
        /// <param name="graphicsDevice">The GraphicsDevice</param>
        /// <param name="game">The GameWorld</param>
        public State(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this.game = game;
        }

        public virtual void PreloadContent()
        {

        }

        /// <summary>
        /// abstract method to be implemented 
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// abstract method to be implemented 
        /// </summary>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// abstract method to be implemented 
        /// </summary>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
