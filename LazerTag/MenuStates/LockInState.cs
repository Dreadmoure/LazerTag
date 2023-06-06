using LazerTag.BuilderPattern;
using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    /// <summary>
    /// Forfatter : Denni
    /// </summary>
    public class LockInState : State
    {
        #region fields
        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> destroyGameObjects = new List<GameObject>();
        private static List<GameObject> newGameObjects = new List<GameObject>();

        private Texture2D lockInSprite;
        private Texture2D lockOutSprite;
        private Texture2D startButtonSprite;
        private SpriteFont font;
        private string lockInString = "Lock in to play";
        private string lockOutString = "Lock out";
        private bool[] isLockedIn = new bool[4] { false, false, false, false };
        private bool canPlay = false;
        private Vector2[] lockSpritePos = new Vector2[4] {
                    new Vector2(GameWorld.ScreenSize.X/4.5f, GameWorld.ScreenSize.Y/3.5f),
                    new Vector2(GameWorld.ScreenSize.X/1.35f, GameWorld.ScreenSize.Y/1.3f), 
                    new Vector2(GameWorld.ScreenSize.X/1.35f, GameWorld.ScreenSize.Y/3.5f), 
                    new Vector2(GameWorld.ScreenSize.X/4.5f, GameWorld.ScreenSize.Y/1.3f) };

        private Texture2D[] helpSprites = new Texture2D[4];
        private Vector2[] helpSpritesPos = new Vector2[4] {
                    new Vector2(GameWorld.ScreenSize.X/3f, GameWorld.ScreenSize.Y/1.88f),
                    new Vector2(GameWorld.ScreenSize.X/2.55f, GameWorld.ScreenSize.Y/1.88f),
                    new Vector2(GameWorld.ScreenSize.X/1.75f, GameWorld.ScreenSize.Y/1.88f),
                    new Vector2(GameWorld.ScreenSize.X/1.6f, GameWorld.ScreenSize.Y/1.88f) };

        private static int playerCount;
        private State gameState;
        #endregion

        #region properties
        /// <summary>
        /// Used to add colliders on instantiated objects and remove from the objects slated for removal
        /// </summary>
        public static List<Collider> Colliders { get; private set; } = new List<Collider>();

        /// <summary>
        /// property for keeping track of who has locked in 
        /// </summary>
        public static List<PlayerIndex> PlayerIndices { get; private set; }
        #endregion

        /// <summary>
        /// constructor for LockInState - sends parameters to base State 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphicsDevice"></param>
        /// <param name="game"></param>
        public LockInState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            playerCount = 4;
        }

        #region methods
        public override void PreloadContent()
        {
            gameState = new GameState(content, graphicsDevice, game);
            gameState.PreloadContent();
        }

        public override void LoadContent()
        {
            Thread t = new Thread(PreloadContent);
            t.IsBackground = true;
            t.Start();

            // reset all 
            gameObjects = new List<GameObject>();
            destroyGameObjects = new List<GameObject>();
            newGameObjects = new List<GameObject>();
            Colliders = new List<Collider>();
            canPlay = false;
            isLockedIn = new bool[4] { false, false, false, false };
            PlayerIndices = new List<PlayerIndex>();
            game.IsMouseVisible = false;

            // load music 
            SoundMixer.Instance.PlayGameMusic();

            lockInSprite = content.Load<Texture2D>("Menus\\LockInButton");
            lockOutSprite = content.Load<Texture2D>("Menus\\LockOutButton");
            startButtonSprite = content.Load<Texture2D>("Menus\\StartButton");
            font = content.Load<SpriteFont>("Fonts\\LifeFont");

            for (int i = 0; i < 4; i++)
            {
                helpSprites[i] = content.Load<Texture2D>($"Menus\\HelpButton{i+1}");
            }

            // load playes 
            for (int i = 0; i < playerCount; i++)
            {
                Director playerDirector = new Director(new PlayerBuilder(i));
                gameObjects.Add(playerDirector.Construct());
            }

            // load level 
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

            //calls start on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }
        }

        /// <summary>
        /// method for setting up the level with platforms 
        /// </summary>
        private void AddPlatforms()
        {
            // the entire level is laid out in a 2 dimentional int array, where each 1 is a platform 
            int[,] tileIds = new int[,]
            {
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3 },
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }
            };

            // loop through the 2d array 
            for (int i = 0; i < tileIds.GetLength(0); i++)
            {
                for (int j = 0; j < tileIds.GetLength(1); j++)
                {
                    // check if there should be a platform, meaning id is not 0
                    if (tileIds[i, j] != 0 && tileIds[i, j] != 9)
                    {
                        // create platform 
                        Director platformDirector = new Director(new PlatformBuilder(j, i, tileIds[i, j]));
                        gameObjects.Add(platformDirector.Construct());
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            //press escape to go back to the menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ResetPlayers();

                // exit to main menu 
                game.ChangeState(GameWorld.Instance.MenuState);
            }

            //calls update on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            LockIn();

            int playersReady = 0;

            for(int i = 0; i < isLockedIn.Length; i++)
            {
                if (isLockedIn[i])
                {
                    playersReady++;
                }
            }

            if(playersReady >= 2)
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }

            EnterGame();

            Cleanup();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.DarkGray);

            float lockInStringX = font.MeasureString(lockInString).X / 2;
            float lockOutStringX = font.MeasureString(lockOutString).X / 2;

            //we begin to draw
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            //calls draw on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }

            for(int i = 0; i < helpSprites.Length; i++)
            {
                spriteBatch.Draw(helpSprites[i], helpSpritesPos[i], null, Color.White, 0f, new Vector2(helpSprites[i].Width / 2, helpSprites[i].Height / 2), 1, SpriteEffects.None, 0.9f);
            }

            for(int i = 0; i < 4; i++)
            {
                if (isLockedIn[i])
                {
                    spriteBatch.DrawString(font, lockOutString, lockSpritePos[i] + new Vector2(0, -150), Color.White, 0f, new Vector2(lockOutStringX, 0), 1, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(lockOutSprite, lockSpritePos[i], null, Color.White, 0f, new Vector2(lockInSprite.Width / 2, lockInSprite.Height / 2), 1, SpriteEffects.None, 0.1f);
                }
                else
                {
                    spriteBatch.DrawString(font, lockInString, lockSpritePos[i] + new Vector2(0, -150), Color.White, 0f, new Vector2(lockInStringX, 0), 1, SpriteEffects.None, 0.1f);
                    spriteBatch.Draw(lockInSprite, lockSpritePos[i], null, Color.White, 0f, new Vector2(lockInSprite.Width / 2, lockInSprite.Height / 2), 1, SpriteEffects.None, 0.1f);
                }
                
            }

            if (canPlay)
            {
                spriteBatch.Draw(startButtonSprite, new Vector2(GameWorld.ScreenSize.X/2 - 35, GameWorld.ScreenSize.Y / 2 + 35), null, Color.White, 0f, new Vector2(startButtonSprite.Width / 2, startButtonSprite.Height / 2), 1, SpriteEffects.None, 0.9f);
            }

            // stop drawing
            spriteBatch.End();
        }

        private void LockIn()
        {
            //check the device for playerindex
            for (int i = 0; i < 4; i++)
            {
                GamePadCapabilities capabilities = GamePad.GetCapabilities((PlayerIndex)i);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller1
                    GamePadState padState = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.IndependentAxes);

                    if (capabilities.GamePadType == GamePadType.GamePad)
                    {
                        if (padState.IsButtonDown(Buttons.A) && !isLockedIn[i])
                        {
                            SoundMixer.Instance.LoginFx();
                            isLockedIn[i] = true;
                            PlayerIndices.Add((PlayerIndex)i);
                        }
                        if (padState.IsButtonDown(Buttons.Y) && isLockedIn[i])
                        {
                            SoundMixer.Instance.LogoutFx();
                            isLockedIn[i] = false;
                            PlayerIndices.Remove((PlayerIndex)i);
                        }
                    }
                }
            }
        }

        private void EnterGame()
        {
            //check the device for playerindex
            for (int i = 0; i < 4; i++)
            {
                GamePadCapabilities capabilities = GamePad.GetCapabilities((PlayerIndex)i);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller1
                    GamePadState padState = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.IndependentAxes);

                    if (canPlay && padState.IsButtonDown(Buttons.Start))
                    {
                        SoundMixer.Instance.LoginFx();
                        ResetPlayers();
                        game.ChangeState(new GameState(content, graphicsDevice, game));
                    }
                }
            }
        }

        /// <summary>
        /// method for adding gameobjects to a list slated for removal
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Destroy(GameObject gameObject)
        {
            destroyGameObjects.Add(gameObject);
        }

        /// <summary>
        /// method for adding gameobjects to a list slated to be added
        /// </summary>
        /// <param name="gameObject"></param>
        public static void Instantiate(GameObject gameObject)
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

        private void ResetPlayers()
        {
            Player playerOne = FindPlayerByTag(PlayerIndex.One.ToString());
            Player playerTwo = FindPlayerByTag(PlayerIndex.Two.ToString());
            Player playerThree = FindPlayerByTag(PlayerIndex.Three.ToString());
            Player playerFour = FindPlayerByTag(PlayerIndex.Four.ToString());

            //kill remaining players
            if (playerOne.Character != null)
            {
                Character character = playerOne.Character.GetComponent<Character>() as Character;
                character.RemoveCharacter();
                playerOne.Life = 0;
            }
            if (playerTwo.Character != null)
            {
                Character character = playerTwo.Character.GetComponent<Character>() as Character;
                character.RemoveCharacter();
                playerTwo.Life = 0;
            }
            if (playerThree.Character != null)
            {
                Character character = playerThree.Character.GetComponent<Character>() as Character;
                character.RemoveCharacter();
                playerThree.Life = 0;
            }
            if (playerFour.Character != null)
            {
                Character character = playerFour.Character.GetComponent<Character>() as Character;
                character.RemoveCharacter();
                playerFour.Life = 0;
            }
        }

        /// <summary>
        /// method for finding a player based on its tag
        /// </summary>
        /// <param name="tag">the tag of the player we want to find</param>
        /// <returns></returns>
        public static Player FindPlayerByTag(string tag)
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
                if (p.Type.ToString() == tag)
                {
                    return p;
                }
            }

            return null;
        }
        #endregion
    }
}
