using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
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

        private List<GameObject> prototypes = new List<GameObject>();

        private CharacterFactory()
        {
            CreatePrototypeP1();
            CreatePrototypeP2();
            CreatePrototypeP3();
            CreatePrototypeP4();
        }

        #region methods

        private void CreatePrototypeP1()
        {
            GameObject gameObject = new GameObject();

            
            Character character = gameObject.AddComponent(new Character()) as Character;

            character.CharacterId = 1;

            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            

            spriteRenderer.SetSprite("Characters\\Red\\Idle\\RedIdle0");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            prototypes.Add(gameObject);
        }

        private void CreatePrototypeP2()
        {
            GameObject gameObject = new GameObject();

            Character character = gameObject.AddComponent(new Character()) as Character;

            character.CharacterId = 2;

            SpriteRenderer spriteRenderer = gameObject.AddComponent(new SpriteRenderer()) as SpriteRenderer;
            

            spriteRenderer.SetSprite("Characters\\Red\\Idle\\RedIdle0");

            spriteRenderer.Scale = 1;
            spriteRenderer.LayerDepth = 0.5f;

            prototypes.Add(gameObject);
        }

        private void CreatePrototypeP3()
        {

        }

        private void CreatePrototypeP4()
        {

        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            switch (type)
            {
                case PlayerIndex.One:
                    gameObject = (GameObject)prototypes[0].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 3, GameWorld.ScreenSize.Y / 2);
                                        
                    break;
                case PlayerIndex.Two:
                    gameObject = (GameObject)prototypes[1].Clone();

                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 1.5f, GameWorld.ScreenSize.Y / 2);

                    break;

            }
            return gameObject;


            //use PlayerIndex enum as it is available
        }
        #endregion
    }
}
