using LazerTag.CreationalPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Player : Component
    {
        private UIRenderer uiRenderer;

        #region properties
        public PlayerIndex Type { get; set; }
        public int Score { get; set; }
        public int Life { get; set; }
        public GameObject Character { get; set; }
        #endregion

        #region methods
        public override void Awake()
        {
            Life = 4;
            Score = 0;

            uiRenderer = GameObject.GetComponent<UIRenderer>() as UIRenderer;

            uiRenderer.SetLifeText(Life);
            uiRenderer.SetScoreText(Score);
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            if(Character == null && Life > 0)
            {
                SpawnCharacter();
            }

            Character character = Character.GetComponent<Character>() as Character;

            uiRenderer.SetLifeText(Life);
            uiRenderer.SetScoreText(Score);
            uiRenderer.SetAmmoCountSprite(character.AmmoCount, Character.Transform.Position);

        }

        public void SpawnCharacter()
        {
            Character = CharacterFactory.Instance.Create(Type);

            GameWorld.Instance.Instantiate(Character);
        }

        public void RemoveCharacter()
        {
            Life--; 
            Character = null; 
        }
        #endregion
    }
}
