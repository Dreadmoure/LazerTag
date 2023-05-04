using LazerTag.CreationalPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    public class Player : Component
    {
        #region properties
        public int Score { get; set; }
        public int Id { get; set; }
        public int Life { get; set; }
        public GameObject Character { get; set; }
        //public Character Character { get; set; }
        #endregion

        #region methods
        public override void Awake()
        {
            Score = 0;
            Life = 4;
        }
        public override void Start()
        {

        }
        public override void Update()
        {
            if(Character == null)
            {
                SpawnCharacter();
            }
        }

        public void SpawnCharacter()
        {
            if(Id == 1)
            {
                GameObject character = CharacterFactory.Instance.Create(PlayerIndex.One);

                GameWorld.Instance.Instantiate(character);

                Character = character;
            }
            else if (Id == 2)
            {
                GameObject character = CharacterFactory.Instance.Create(PlayerIndex.Two);

                GameWorld.Instance.Instantiate(character);

                Character = character;
            }


        }
        #endregion
    }
}
