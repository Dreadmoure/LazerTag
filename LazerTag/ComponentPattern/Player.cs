using LazerTag.CreationalPattern;
using LazerTag.MenuStates;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ComponentPattern
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class Player : Component
    {
        #region fields
        private UIRenderer uiRenderer;
        #endregion

        #region properties
        
        /// <summary>
        /// property for getting and setting the PlayerIndex
        /// </summary>
        public PlayerIndex Type { get; set; }

        /// <summary>
        /// property for getting and setting the Players Score
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// property for getting and setting the Players Life
        /// </summary>
        public int Life { get; set; }

        /// <summary>
        /// property for getting and setting the Players Character
        /// </summary>
        public GameObject Character { get; set; }
        #endregion

        #region methods
        /// <summary>
        /// Method that runs as the first thing
        /// </summary>
        public override void Awake()
        {
            Life = 4;
            Score = 0;

            uiRenderer = GameObject.GetComponent<UIRenderer>() as UIRenderer;

            uiRenderer.SetLifeText(Life);
            uiRenderer.SetScoreText(Score);
        }

        /// <summary>
        /// method which runs every frame
        /// </summary>
        public override void Update()
        {
            
            if(Character == null && Life > 0)
            {
                SpawnCharacter();
            }

            if(Character != null)
            {
                Character character = Character.GetComponent<Character>() as Character;
                
                uiRenderer.SetAmmoCountSprite(character.AmmoCount, Character.Transform.Position);

                uiRenderer.SetSolarUpgradeSprite(character.HasSolarUpgrade, Character.Transform.Position);

                if (character.HasSpecialAmmo)
                {
                    uiRenderer.SetSpecialAmmoSprite(Character.Transform.Position);
                }
            }
            else
            {
                uiRenderer.SetAmmoCountSprite(0, new Vector2(0, 0));
            }
            

            uiRenderer.SetLifeText(Life);
            uiRenderer.SetScoreText(Score);
            

        }

        /// <summary>
        /// method used for spawning a character
        /// </summary>
        private void SpawnCharacter()
        {
            Character = CharacterFactory.Instance.Create(Type);
            Character character = Character.GetComponent<Character>() as Character;

            if (GameWorld.Instance.CurrentState == GameWorld.Instance.LockInState)
            {
                LockInState.Instantiate(Character);
            }
            else
            {
                GameState.Instantiate(Character);
            }
            
        }

        /// <summary>
        /// method used for removing a character
        /// </summary>
        public void RemoveCharacter()
        {
            Life--;

            if (Life <= 0)
            {
                GameState.PlayerCount--;
            }

            Character character = Character.GetComponent<Character>() as Character;
            uiRenderer.SetSolarUpgradeSprite(character.HasSolarUpgrade, Character.Transform.Position);

            Character = null; 
        }
        #endregion
    }
}
