using LazerTag.BuilderPattern;
using LazerTag.ComponentPattern;
using LazerTag.CreationalPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LazerTag
{
    public class GameWorld : Game
    {
        #region singleton
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if(instance == null)
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

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> destroyGameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();
        #endregion

        #region properties
        /// <summary>
        /// property for getting the elapsed gametime
        /// </summary>
        public static float DeltaTime { get; private set; }

        /// <summary>
        /// Used to add colliders on instantiated objects and remove from the objects slated for removal
        /// </summary>
        public List<Collider> Colliders { get; private set; } = new List<Collider>();

        /// <summary>
        /// Property for accessing the size of the screen
        /// </summary>
        public static Vector2 ScreenSize { get; private set; }
        #endregion

        #region constructor
        /// <summary>
        /// Singleton constructor
        /// </summary>
        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            //_graphics.IsFullScreen = true; 

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }
        #endregion

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Director playerDirector1 = new Director(new PlayerBuilder(0));
            gameObjects.Add(playerDirector1.Construct());

            Director playerDirector2 = new Director(new PlayerBuilder(1));
            gameObjects.Add(playerDirector2.Construct());

            Director playerDirector3 = new Director(new PlayerBuilder(2));
            gameObjects.Add(playerDirector3.Construct());

            Director playerDirector4 = new Director(new PlayerBuilder(3));
            gameObjects.Add(playerDirector4.Construct());

            // call add platforms method 
            AddPlatforms(); 

            //loop that calls awake on all GameObjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();

                Collider collider = (Collider)gameObjects[i].GetComponent<Collider>();
                if (collider != null)
                {
                    Colliders.Add(collider);
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// method for setting up the level with platforms 
        /// </summary>
        private void AddPlatforms()
        {
            // the entire level is laid out in a 2 diemtional int array, where each 1 is a platform 
            int[,] tileIds = new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1 },
                {1, 0, 0, 0, 2, 2, 1, 2, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 1, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 2, 2, 0, 1 },
                {1, 0, 0, 2, 1, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
                {1, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };

            // loop through the 2d array 
            for (int i = 0; i < tileIds.GetLength(0); i++)
            {
                for (int j = 0; j < tileIds.GetLength(1); j++)
                {
                    // check if there should be a platform, meaning id is not 0
                    if (tileIds[i, j] != 0)
                    {
                        // create platform 
                        Director platformDirector = new Director(new PlatformBuilder(j, i, tileIds[i, j]));
                        gameObjects.Add(platformDirector.Construct());
                    }
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //loads the content in the UI
            UI.Instance.LoadContent(Content);

            //calls start on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }

            //SpawnCharacters();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //updates the gametime
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Update the UI
            UI.Instance.Update();

            //calls update on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            base.Update(gameTime);

            //calls cleanup
            Cleanup();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //we begin to draw
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            //calls draw on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(_spriteBatch);
            }

            //draw the ui
            UI.Instance.Draw(_spriteBatch);

            //we stop drawing
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// method for adding gameobjects to a list slated for removal
        /// </summary>
        /// <param name="gameObject"></param>
        public void Destroy(GameObject gameObject)
        {
            destroyGameObjects.Add(gameObject);
        }

        /// <summary>
        /// method for adding gameobjects to a list slated to be added
        /// </summary>
        /// <param name="gameObject"></param>
        public void Instantiate(GameObject gameObject)
        {
            newGameObjects.Add(gameObject);
        }

        /// <summary>
        /// Method for removing and adding gameobjects during runtime, including colliders
        /// </summary>
        private void Cleanup()
        {
            //add to gameobjects 
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                gameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();

                //if no collider is on the new game object, and there needs to be, adds one
                Collider collider = (Collider)newGameObjects[i].GetComponent<Collider>();
                if (collider != null)
                {
                    Colliders.Add(collider);
                }
            }

            //remove from gameobjects
            for (int i = 0; i < destroyGameObjects.Count; i++)
            {
                gameObjects.Remove(destroyGameObjects[i]);

                //remove collider
                Collider collider = (Collider)destroyGameObjects[i].GetComponent<Collider>();

                if (destroyGameObjects[i].GetComponent<Projectile>() != null)
                {
                    Projectile projectile = destroyGameObjects[i].GetComponent<Projectile>() as Projectile;
                    collider.CollisionEvent.Detach(projectile);
                }
                if (destroyGameObjects[i].GetComponent<Character>() != null)
                {
                    Character character = destroyGameObjects[i].GetComponent<Character>() as Character;
                    collider.CollisionEvent.Detach(character);
                }



                if (collider != null)
                {
                    Colliders.Remove(collider);
                }
            }

            //clear the lists
            destroyGameObjects.Clear();
            newGameObjects.Clear();
        }

        /// <summary>
        /// Method for finding an object of a type, so we can reference it
        /// </summary>
        /// <typeparam name="T">The type of component we are searching for</typeparam>
        /// <returns>returns the object of the type we are looking for</returns>
        public Component FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject gameObject in gameObjects)
            {
                Component c = gameObject.GetComponent<T>();

                if (c != null)
                {
                    return c;
                }
            }
            return null;
        }

        public Player FindPlayerByTag(string tag)
        {
            List<Player> players = new List<Player>();

            foreach (GameObject gameObject in gameObjects)
            {
                Player p = gameObject.GetComponent<Player>() as Player;

                if (p != null)
                {
                    players.Add(p); 
                }
            }

            foreach (Player p in players)
            {
                if(p.Type.ToString() == tag)
                {
                    return p; 
                }
            }

            return null; 
        }

        //public GameObject FindObjectByTag(string tag) 
        //{
        //    foreach(GameObject gameObject in gameObjects)
        //    {
        //        if(gameObject.Tag == tag)
        //        {
        //            return gameObject;
        //        }

        //    }
        //    return null;
        //}
    }
}