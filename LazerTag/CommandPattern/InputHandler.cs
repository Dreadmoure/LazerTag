using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using LazerTag.ComponentPattern;
using System.Diagnostics;
using System.Xml;

namespace LazerTag.CommandPattern
{
    public class InputHandler
    {
        private Dictionary<PadInfo, ICommand> padbinds = new Dictionary<PadInfo, ICommand>();

        /// <summary>
        /// constructor, used for setting the padbinds
        /// </summary>
        public InputHandler()
        {
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickLeft), new MoveCommand(new Vector2(-1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickRight), new MoveCommand(new Vector2(1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftTrigger), new JumpCommand());

            padbinds.Add(new PadInfo(Buttons.RightThumbstickLeft), new AimCommand(new Vector2(-1, 0)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickRight), new AimCommand(new Vector2(1, 0)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickUp), new AimCommand(new Vector2(0, -1)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickDown), new AimCommand(new Vector2(0, 1)));
            padbinds.Add(new PadInfo(Buttons.RightTrigger), new ShootCommand());

            
        }

        /// <summary>
        /// method for executing any commands on a character 
        /// </summary>
        /// <param name="character">the character the command should be executed on</param>
        public void Execute(Character character)
        {
            //check the device for player one
            GamePadCapabilities capabilities = GamePad.GetCapabilities(character.CharacterIndex);

            //if there is a controller attached, handle it
            if (capabilities.IsConnected)
            {
                //get the current state of Controller1
                GamePadState padState = GamePad.GetState(character.CharacterIndex, GamePadDeadZone.IndependentAxes);

                ExecuteCommands(capabilities, padState, character);
            }
        }

        /// <summary>
        /// method for executing commands 
        /// </summary>
        /// <param name="capabilities">GamePadCapabilities</param>
        /// <param name="padState">GamePadState</param>
        /// <param name="character">the character the command should be executed on</param>
        private void ExecuteCommands(GamePadCapabilities capabilities, GamePadState padState, Character character)
        {
            // checks to see if the connected device is a GamePad
            if (capabilities.GamePadType == GamePadType.GamePad)
            {
                foreach (PadInfo padInfo in padbinds.Keys)
                {
                    if (padState.IsButtonDown(padInfo.Button))
                    {
                        //movement keys
                        if (padInfo.Button == Buttons.LeftThumbstickLeft && padState.ThumbSticks.Left.X < -0.02f)
                        {
                            character.CharacterDirection = Direction.Left;
                            character.IsWalking = true;
                            padbinds[padInfo].Execute(character);
                            padInfo.IsDown = true;
                        }
                        if (padInfo.Button == Buttons.LeftThumbstickRight && padState.ThumbSticks.Left.X > 0.02f)
                        {
                            character.CharacterDirection = Direction.Right;
                            character.IsWalking = true;
                            padbinds[padInfo].Execute(character);
                            padInfo.IsDown = true;
                        }
                        // jump
                        if (padInfo.Button == Buttons.LeftTrigger)
                        {
                            padbinds[padInfo].Execute(character);
                            padInfo.IsDown = true;
                        }
                        // shoot
                        if (padInfo.Button == Buttons.RightTrigger)
                        {
                            padbinds[padInfo].Execute(character);
                            padInfo.IsDown = true;
                        }
                        //Aim keys
                        if (padInfo.Button == Buttons.RightThumbstickLeft || padInfo.Button == Buttons.RightThumbstickRight ||
                            padInfo.Button == Buttons.RightThumbstickUp || padInfo.Button == Buttons.RightThumbstickDown)
                        {
                            padbinds[padInfo].Execute(character);
                            padInfo.IsDown = true;
                        }
                    }

                    if (!padState.IsButtonDown(padInfo.Button) && padInfo.IsDown == true)
                    {
                        padInfo.IsDown = false;
                        character.IsWalking = false;
                        character.IsJumping = false;
                    }
                }
            }
        }
    }
}
