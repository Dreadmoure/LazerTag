using LazerTag.BuilderPattern;
using LazerTag.ComponentPattern;
using LazerTag.CreationalPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazerTag.MenuStates
{
    public class GameState : State
    {
        #region fields 
        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> destroyGameObjects = new List<GameObject>();
        private static List<GameObject> newGameObjects = new List<GameObject>();

        private static List<Vector2> pickupSpawnPos = new List<Vector2>();
        public static bool[] isSpawnPosOccupied;
        private static float spawnTimer = 0;
        private static float spawnTime = 5;

        //score check
        private List<Player> topPlayers; 
        #endregion

        #region properties 
        /// <summary>
        /// propertry for getting and setting the amount of players in the game
        /// </summary>
        public static int PlayerCount { get; set; }

        /// <summary>
        /// property for getting the winner of the game 
        /// </summary>
        public static Player Winner { get; private set; }

        /// <summary>
        /// Used to add colliders on instantiated objects and remove from the objects slated for removal
        /// </summary>
        public static List<Collider> Colliders { get; private set; } = new List<Collider>();
        #endregion

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            // set mouse invisible 
            game.IsMouseVisible = false; 

            // reset all 
            gameObjects = new List<GameObject>();
            destroyGameObjects = new List<GameObject>();
            newGameObjects = new List<GameObject>();
            pickupSpawnPos = new List<Vector2>();
            Colliders = new List<Collider>();
            PlayerCount = LockInState.PlayerIndices.Count;
            Winner = null; 
        }

        #region methods 
        public override void LoadContent()
        {
            // load music 
            SoundMixer.Instance.PlayGameMusic();

            foreach(PlayerIndex playerIndex in LockInState.PlayerIndices)
            {
                Director playerDirector = new Director(new PlayerBuilder((int)playerIndex));
                gameObjects.Add(playerDirector.Construct());
            }

            //// load playes 
            //for (int i = 0; i < PlayerCount; i++)
            //{
            //    Director playerDirector = new Director(new PlayerBuilder(i));
            //    gameObjects.Add(playerDirector.Construct());

            //}

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
                {3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1 },
                {1, 0, 0, 0, 2, 2, 1, 2, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 9, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 9, 0, 0, 0, 0, 1, 9, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 1, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 9, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 2, 2, 0, 1 },
                {1, 0, 0, 2, 1, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 9, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
                {3, 2, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 2, 2, 2, 2, 2, 2, 3 },
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
                    if (tileIds[i, j] == 9)
                    {
                        pickupSpawnPos.Add(new Vector2(j * 50, i * 50));
                    }
                }
            }
            //sets spawnoccupied array
            isSpawnPosOccupied = new bool[pickupSpawnPos.Count];
        }

        public override void Update(GameTime gameTime)
        {
            

            // spawn pickups via thread 
            spawnTimer += GameWorld.DeltaTime;
            if (spawnTimer >= spawnTime)
            {
                spawnTimer = 0;

                Thread t = new Thread(SpawnPickup);
                t.IsBackground = true;
                t.Start();
            }

            //calls update on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ResetPlayers();

                // exit to main menu 
                game.ChangeState(GameWorld.Instance.MenuState);
            }

            if (PlayerCount <= 1)
            {
                bool isStaleMate = IsStaleMate(); //checks if there is a stalemate

                if (isStaleMate == false)
                {
                    Winner = FindWinner(); //if there is not stalemate it checks the highest score and sets that to the winner

                    game.ChangeState(new GameOverState(content, graphicsDevice, game));
                }
                else //if there is a stalemate reset the level with the players that are in a stalemate
                {
                    ResetLevel();
                    //somehow get the players which are tied and make sure only those 2 are in the game with 1 life
                }
            }

            //calls cleanup
            Cleanup();
        }

        private void ResetPlayers()
        {
            Player playerOne = FindPlayerByTag(PlayerIndex.One.ToString());
            Player playerTwo = FindPlayerByTag(PlayerIndex.Two.ToString());
            Player playerThree = FindPlayerByTag(PlayerIndex.Three.ToString());
            Player playerFour = FindPlayerByTag(PlayerIndex.Four.ToString());

            //kill remaining players
            if(playerOne != null)
            {
                if (playerOne.Character != null)
                {
                    Character character = playerOne.Character.GetComponent<Character>() as Character;
                    character.RemoveCharacter();
                    playerOne.Life = 0;
                }
            }
            
            if(playerTwo != null)
            {
                if (playerTwo.Character != null)
                {
                    Character character = playerTwo.Character.GetComponent<Character>() as Character;
                    character.RemoveCharacter();
                    playerTwo.Life = 0;
                }
            }
            
            if(playerThree != null)
            {
                if (playerThree.Character != null)
                {
                    Character character = playerThree.Character.GetComponent<Character>() as Character;
                    character.RemoveCharacter();
                    playerThree.Life = 0;
                }
            }
            
            if(playerFour != null)
            {
                if (playerFour.Character != null)
                {
                    Character character = playerFour.Character.GetComponent<Character>() as Character;
                    character.RemoveCharacter();
                    playerFour.Life = 0;
                }
            }
            
        }

        private bool IsStaleMate()
        {
            List<Player> players = new List<Player>();
            players.Add(FindPlayerByTag(PlayerIndex.One.ToString()));
            players.Add(FindPlayerByTag(PlayerIndex.Two.ToString()));
            players.Add(FindPlayerByTag(PlayerIndex.Three.ToString()));
            players.Add(FindPlayerByTag(PlayerIndex.Four.ToString()));

            // find a player with highest score 
            Player player = players.First();

            foreach (Player p in players)
            {
                if(player.Score < p.Score)
                {
                    player = p; 
                }
            }

            // check all others with the same score, and store in list 
            topPlayers = new List<Player>();

            foreach (Player p in players)
            {
                if(player.Score == p.Score)
                {
                    topPlayers.Add(p); 
                }
            }

            if(topPlayers.Count > 1)
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }

        private Player FindWinner()
        {
            Player winner = topPlayers.First(); 

            foreach (Player player in topPlayers)
            {
                if (player.Score > player.Score)
                {
                    winner = player;
                }
                else
                {
                    winner = player;
                }
            }

            return winner; 
        }

        private void ResetLevel()
        {
            ResetPlayers();

            Player playerOne = FindPlayerByTag(PlayerIndex.One.ToString());
            Player playerTwo = FindPlayerByTag(PlayerIndex.Two.ToString());
            Player playerThree = FindPlayerByTag(PlayerIndex.Three.ToString());
            Player playerFour = FindPlayerByTag(PlayerIndex.Four.ToString());

            //sets life to 1 for the two remaining players 
            foreach (Player player in topPlayers)
            {
                if(playerOne != null)
                {
                    if (player.Type == PlayerIndex.One)
                    {
                        playerOne.Life = 1;
                        PlayerCount++;
                    }
                }
                
                if(playerTwo != null)
                {
                    if (player.Type == PlayerIndex.Two)
                    {
                        playerTwo.Life = 1;
                        PlayerCount++;
                    }
                }
                
                if(playerThree != null)
                {
                    if (player.Type == PlayerIndex.Three)
                    {
                        playerThree.Life = 1;
                        PlayerCount++;
                    }
                }
                
                if(playerFour != null)
                {
                    if (player.Type == PlayerIndex.Four)
                    {
                        playerFour.Life = 1;
                        PlayerCount++;
                    }
                }
                
            }
        }

        private void SpawnPickup()
        {
            Random random = new Random();
            GameObject pickup = new GameObject();
            PickUpType type;

            int r = random.Next(0, 100);

            // choose random pickup type 
            if (r >= 90)
            {
                pickup = PickUpFactory.Instance.Create(PickUpType.SolarUpgrade);
                type = PickUpType.SolarUpgrade;
            }
            else if (r >= 75)
            {
                pickup = PickUpFactory.Instance.Create(PickUpType.SpecialAmmo);
                type = PickUpType.SpecialAmmo;
            }
            else
            {
                pickup = PickUpFactory.Instance.Create(PickUpType.Battery);
                type = PickUpType.Battery;
            }

            bool posNotFound = true;

            while (posNotFound)
            {
                int randomNumber = random.Next(pickupSpawnPos.Count);

                if (!isSpawnPosOccupied[randomNumber])
                {
                    isSpawnPosOccupied[randomNumber] = true;
                    pickup.Transform.Position = pickupSpawnPos[randomNumber];

                    if (type == PickUpType.SolarUpgrade)
                    {
                        SolarUpgrade p = pickup.GetComponent<SolarUpgrade>() as SolarUpgrade;
                        p.OccupiedPos = randomNumber;
                    }
                    if (type == PickUpType.SpecialAmmo)
                    {
                        SpecialAmmo p = pickup.GetComponent<SpecialAmmo>() as SpecialAmmo;
                        p.OccupiedPos = randomNumber;
                    }
                    if (type == PickUpType.Battery)
                    {
                        Battery p = pickup.GetComponent<Battery>() as Battery;
                        p.OccupiedPos = randomNumber;
                    }

                    posNotFound = false;
                }
            }

            // add gameobject 
            Instantiate(pickup);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.DarkGray); 

            //we begin to draw
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            //calls draw on all gameobjects
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }

            // stop drawing
            spriteBatch.End();
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

        /// <summary>
        /// Method for finding an object of a type, so we can reference it
        /// </summary>
        /// <typeparam name="T">The type of component we are searching for</typeparam>
        /// <returns>returns the object of the type we are looking for</returns>
        public static Component FindObjectOfType<T>() where T : Component
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
