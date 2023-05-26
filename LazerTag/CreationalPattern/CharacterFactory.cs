using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    /// <summary>
    /// Forfatter : Denni
    /// </summary>
    public class CharacterFactory : Factory
    {
        #region singleton
        private static CharacterFactory instance;

        public static CharacterFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CharacterFactory();
                }
                return instance;
            }
        }
        #endregion

        #region fields
        private List<GameObject> prototypes = new List<GameObject>();
        #endregion

        #region constructor
        private CharacterFactory()
        {
            CreatePrototypeP1();
            CreatePrototypeP2();
            CreatePrototypeP3();
            CreatePrototypeP4();
        }
        #endregion

        #region methods
        /// <summary>
        /// Method for creating prototype1
        /// </summary>
        private void CreatePrototypeP1()
        {
            GameObject gameObject = new GameObject();
            
            Character character = gameObject.AddComponent(new Character()) as Character;
            character.CharacterIndex = PlayerIndex.One;

            //add animator
            gameObject.AddComponent(new Animator());
            Animator animator = gameObject.GetComponent<Animator>() as Animator;

            character.WalkSprites = new string[8];

            //sets the sprites used for the animations
            for (int i = 0; i < character.WalkSprites.Length; i++)
            {
                character.WalkSprites[i] = $"Characters\\Red\\Walk\\RedWalk{i}";
            }

            character.IdleSprites = new string[6];

            for (int i = 0; i < character.IdleSprites.Length; i++)
            {
                character.IdleSprites[i] = $"Characters\\Red\\Idle\\RedIdle{i}";
            }

            //ads animations
            animator.AddAnimation(character.BuildAnimation(16, "Walk", character.WalkSprites));
            animator.AddAnimation(character.BuildAnimation(6, "Idle", character.IdleSprites));


            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            spriteRenderer.SetSprite("Characters\\Red\\Idle\\RedIdle0");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            gameObject.AddComponent(new Collider()); 

            prototypes.Add(gameObject);
        }

        /// <summary>
        /// Method for creating prototype2
        /// </summary>
        private void CreatePrototypeP2()
        {
            GameObject gameObject = new GameObject();

            Character character = gameObject.AddComponent(new Character()) as Character;
            character.CharacterIndex = PlayerIndex.Two;

            //add animator
            gameObject.AddComponent(new Animator());
            Animator animator = gameObject.GetComponent<Animator>() as Animator;

            character.WalkSprites = new string[8];

            //sets the sprites used for the animations
            for (int i = 0; i < character.WalkSprites.Length; i++)
            {
                character.WalkSprites[i] = $"Characters\\Blue\\Walk\\BlueWalk{i}";
            }

            character.IdleSprites = new string[6];

            for (int i = 0; i < character.IdleSprites.Length; i++)
            {
                character.IdleSprites[i] = $"Characters\\Blue\\Idle\\BlueIdle{i}";
            }

            //ads animations
            animator.AddAnimation(character.BuildAnimation(16, "Walk", character.WalkSprites));
            animator.AddAnimation(character.BuildAnimation(6, "Idle", character.IdleSprites));


            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            spriteRenderer.SetSprite("Characters\\Blue\\Idle\\BlueIdle0");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            gameObject.AddComponent(new Collider());

            prototypes.Add(gameObject);
        }

        /// <summary>
        /// Method for creating prototype3
        /// </summary>
        private void CreatePrototypeP3()
        {
            GameObject gameObject = new GameObject();

            Character character = gameObject.AddComponent(new Character()) as Character;
            character.CharacterIndex = PlayerIndex.Three;

            //add animator
            gameObject.AddComponent(new Animator());
            Animator animator = gameObject.GetComponent<Animator>() as Animator;

            character.WalkSprites = new string[8];

            //sets the sprites used for the animations
            for (int i = 0; i < character.WalkSprites.Length; i++)
            {
                character.WalkSprites[i] = $"Characters\\Green\\Walk\\GreenWalk{i}";
            }

            character.IdleSprites = new string[6];

            for (int i = 0; i < character.IdleSprites.Length; i++)
            {
                character.IdleSprites[i] = $"Characters\\Green\\Idle\\GreenIdle{i}";
            }

            //ads animations
            animator.AddAnimation(character.BuildAnimation(16, "Walk", character.WalkSprites));
            animator.AddAnimation(character.BuildAnimation(6, "Idle", character.IdleSprites));


            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            spriteRenderer.SetSprite("Characters\\Green\\Idle\\GreenIdle0");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            gameObject.AddComponent(new Collider());

            prototypes.Add(gameObject);
        }

        /// <summary>
        /// Method for creating prototype4
        /// </summary>
        private void CreatePrototypeP4()
        {
            GameObject gameObject = new GameObject();

            Character character = gameObject.AddComponent(new Character()) as Character;
            character.CharacterIndex = PlayerIndex.Four;

            //add animator
            gameObject.AddComponent(new Animator());
            Animator animator = gameObject.GetComponent<Animator>() as Animator;

            character.WalkSprites = new string[8];

            //sets the sprites used for the animations
            for (int i = 0; i < character.WalkSprites.Length; i++)
            {
                character.WalkSprites[i] = $"Characters\\Pink\\Walk\\PinkWalk{i}";
            }

            character.IdleSprites = new string[6];

            for (int i = 0; i < character.IdleSprites.Length; i++)
            {
                character.IdleSprites[i] = $"Characters\\Pink\\Idle\\PinkIdle{i}";
            }

            //ads animations
            animator.AddAnimation(character.BuildAnimation(16, "Walk", character.WalkSprites));
            animator.AddAnimation(character.BuildAnimation(6, "Idle", character.IdleSprites));


            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            spriteRenderer.SetSprite("Characters\\Pink\\Idle\\PinkIdle0");
            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            gameObject.AddComponent(new Collider());

            prototypes.Add(gameObject);
        }

        /// <summary>
        /// Method for creating a gameobject, by cloning it
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>GameObject</returns>
        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();
            Collider collider;
            Character character; 

            switch (type)
            {
                case PlayerIndex.One:
                    gameObject = (GameObject)prototypes[0].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 8, GameWorld.ScreenSize.Y / 4);

                    character = gameObject.GetComponent<Character>() as Character; 
                    collider = gameObject.GetComponent<Collider>() as Collider;
                    collider.CollisionEvent.Attach(character);
                    // attach platform side collisionevents 
                    collider.TopCollisionEvent.Attach(character);
                    collider.BottomCollisionEvent.Attach(character);
                    collider.LeftCollisionEvent.Attach(character);
                    collider.RightCollisionEvent.Attach(character);

                    gameObject.Tag = type.ToString();
                    break;
                case PlayerIndex.Two:
                    gameObject = (GameObject)prototypes[1].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 1.09f, GameWorld.ScreenSize.Y / 1.16f);

                    character = gameObject.GetComponent<Character>() as Character;
                    collider = gameObject.GetComponent<Collider>() as Collider;
                    collider.CollisionEvent.Attach(character);
                    // attach platform side collisionevents 
                    collider.TopCollisionEvent.Attach(character);
                    collider.BottomCollisionEvent.Attach(character);
                    collider.LeftCollisionEvent.Attach(character);
                    collider.RightCollisionEvent.Attach(character);

                    gameObject.Tag = type.ToString();
                    break;
                case PlayerIndex.Three:
                    gameObject = (GameObject)prototypes[2].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 1.07f, GameWorld.ScreenSize.Y / 4);

                    character = gameObject.GetComponent<Character>() as Character;
                    collider = gameObject.GetComponent<Collider>() as Collider;
                    collider.CollisionEvent.Attach(character);
                    // attach platform side collisionevents 
                    collider.TopCollisionEvent.Attach(character);
                    collider.BottomCollisionEvent.Attach(character);
                    collider.LeftCollisionEvent.Attach(character);
                    collider.RightCollisionEvent.Attach(character);

                    gameObject.Tag = type.ToString();
                    break;
                case PlayerIndex.Four:
                    gameObject = (GameObject)prototypes[3].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 11, GameWorld.ScreenSize.Y / 1.16f);

                    character = gameObject.GetComponent<Character>() as Character;
                    collider = gameObject.GetComponent<Collider>() as Collider;
                    collider.CollisionEvent.Attach(character);
                    // attach platform side collisionevents 
                    collider.TopCollisionEvent.Attach(character);
                    collider.BottomCollisionEvent.Attach(character);
                    collider.LeftCollisionEvent.Attach(character);
                    collider.RightCollisionEvent.Attach(character);

                    gameObject.Tag = type.ToString();
                    break;

            }
            return gameObject;
        }
        #endregion
    }
}
