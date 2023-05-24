using LazerTag.BuilderPattern;
using LazerTag.ComponentPattern;
using LazerTag.CreationalPattern;
using LazerTag.Domain;
using LazerTag.Repository;
using LazerTag.MenuStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data.SQLite;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LazerTag.Repository.Provider;
using LazerTag.Repository.Mapper;
using LazerTag.Repository.Repositories;

namespace LazerTag
{
    /// <summary>
    /// class for the gameworld which inherits from game
    /// </summary>
    public class GameWorld : Game
    {
        #region singleton
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }
        #endregion

        #region fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // handle states 
        private State nextState; 
        #endregion

        #region properties
        /// <summary>
        /// Property for accessing the size of the screen
        /// </summary>
        public static Vector2 ScreenSize { get; private set; }

        public State MenuState { get; private set; }
        public State HighscoreState { get; private set; }
        public State HelpState { get; private set; }
        public State SettingsState { get; private set; }
        public State LockInState { get; private set; }
        public State CurrentState { get; set; }

        /// <summary>
        /// property for getting the elapsed gametime
        /// </summary>
        public static float DeltaTime { get; private set; }

        public IDatabaseProvider Provider { get; private set; }

        public HighScoreMapper HighScoreMapper { get; private set; }

        public static HighScoreRepository HighScoreRepository { get; private set; }
        #endregion

        #region constructor
        /// <summary>
        /// Singleton constructor
        /// </summary>
        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;

            //_graphics.IsFullScreen = true; 

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            HighScoreMapper = new HighScoreMapper();
            Provider = new SQLiteDatabaseProvider("Data Source=highScores.db;Version=3;new=true");
            HighScoreRepository = new HighScoreRepository(Provider, HighScoreMapper);

            HighScoreDummyMethod();

        }
        #endregion

        #region methods
        /// <summary>
        /// used to add scores to the database, for internal use only
        /// </summary>
        private void HighScoreDummyMethod()
        {
            GameWorld.HighScoreRepository.Open();

            //GameWorld.HighScoreRepository.AddScore("Ida", 50);
            //GameWorld.HighScoreRepository.AddScore("Denni", 300);
            //GameWorld.HighScoreRepository.AddScore("Lars", 995);

            //GameWorld.HighScoreRepository.UpdateScore(1, "Karl", 1200);

            GameWorld.HighScoreRepository.Close();
        }


        /// <summary>
        /// method which runs first when the program is executed
        /// </summary>
        protected override void Initialize()
        {
            this.Window.Title = "LazerTag";

            // set initial state 
            MenuState = new MenuState(Content, GraphicsDevice, this);
            HighscoreState = new HighscoreState(Content, GraphicsDevice, this);
            HelpState = new HelpState(Content, GraphicsDevice, this); 
            SettingsState = new SettingsState(Content, GraphicsDevice, this); 
            LockInState = new LockInState(Content, GraphicsDevice, this); 


            base.Initialize();
        }

        /// <summary>
        /// method which is used to load the content
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // load content for SoundMixer 
            SoundMixer.Instance.LoadContent(Content);

            // handle states
            CurrentState = MenuState;
            CurrentState.LoadContent();
            nextState = null; 
        }

        /// <summary>
        /// method for updating objects each frame
        /// </summary>
        /// <param name="gameTime">Time in the game</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //updates the gametime
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // check if a new state is available 
            if (nextState != null)
            {
                CurrentState = nextState;
                CurrentState.LoadContent();
                nextState = null; 
            }

            CurrentState.Update(gameTime); 

            base.Update(gameTime);
        }

        public void ChangeState(State state)
        {
            nextState = state; 
        }

        /// <summary>
        /// method used to draw objects to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            CurrentState.Draw(gameTime, _spriteBatch); 

            base.Draw(gameTime);
        }
        #endregion
    }
}