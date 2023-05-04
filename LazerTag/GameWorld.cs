﻿using LazerTag.ComponentPattern;
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

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }
        #endregion

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //updates the gametime
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //calls update on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
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
    }
}