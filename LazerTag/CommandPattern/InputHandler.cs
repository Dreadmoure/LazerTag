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
        #region singleton
        private static InputHandler instance;

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }
        #endregion

        private Dictionary<PadInfo, ICommand> padbinds = new Dictionary<PadInfo, ICommand>();

        private InputHandler()
        {
            //Character character = GameWorld.Instance.FindObjectOfType<Character>() as Character;
            //GameObject gameObject = GameWorld.Instance.FindObjectByTag("Player1");
            //GamePadState padState = GamePad.GetState(PlayerIndex.One);


            padbinds.Add(new PadInfo(Buttons.LeftThumbstickLeft), new MoveCommand(new Vector2(-1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickRight), new MoveCommand(new Vector2(1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftTrigger), new JumpCommand());

            padbinds.Add(new PadInfo(Buttons.RightThumbstickLeft), new AimCommand(new Vector2(-1, 0)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickRight), new AimCommand(new Vector2(1, 0)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickUp), new AimCommand(new Vector2(0, -1)));
            padbinds.Add(new PadInfo(Buttons.RightThumbstickDown), new AimCommand(new Vector2(0, 1)));
            //padbinds.Add(new PadInfo(Buttons.RightThumbstickLeft, Buttons.RightThumbstickUp), new AimCommand(new Vector2(-1, -1)));
            //padbinds.Add(new PadInfo(Buttons.RightThumbstickLeft, Buttons.RightThumbstickDown), new AimCommand(new Vector2(-1, 1)));
            //padbinds.Add(new PadInfo(Buttons.RightThumbstickRight, Buttons.RightThumbstickUp), new AimCommand(new Vector2(1, -1)));
            //padbinds.Add(new PadInfo(Buttons.RightThumbstickRight, Buttons.RightThumbstickDown), new AimCommand(new Vector2(1, 1)));

            padbinds.Add(new PadInfo(Buttons.RightTrigger), new ShootCommand());
        }

        public void Execute(Character character)
        {

            if(character.CharacterId == 1)
            {
                //check the device for player one
                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller1
                    GamePadState padState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);

                    ExecuteCommands(capabilities, padState, character);
                }
            }

            if (character.CharacterId == 2)
            {
                //check the device for player two
                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.Two);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller2
                    GamePadState padState = GamePad.GetState(PlayerIndex.Two, GamePadDeadZone.IndependentAxes);

                    ExecuteCommands(capabilities, padState, character);
                }
            }

            //player 3
            if (character.CharacterId == 3)
            {
                //check the device for player two
                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.Three);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller2
                    GamePadState padState = GamePad.GetState(PlayerIndex.Three, GamePadDeadZone.IndependentAxes);

                    ExecuteCommands(capabilities, padState, character);
                }
            }

            //player 4
            if (character.CharacterId == 4)
            {
                //check the device for player two
                GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.Four);

                //if there is a controller attached, handle it
                if (capabilities.IsConnected)
                {
                    //get the current state of Controller2
                    GamePadState padState = GamePad.GetState(PlayerIndex.Four, GamePadDeadZone.IndependentAxes);

                    ExecuteCommands(capabilities, padState, character);
                }
            }

        }

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
                        //if((padInfo.Button == Buttons.RightThumbstickLeft && padInfo.Button2 == Buttons.RightThumbstickUp) ||
                        //   (padInfo.Button == Buttons.RightThumbstickLeft && padInfo.Button2 == Buttons.RightThumbstickDown) ||
                        //   (padInfo.Button == Buttons.RightThumbstickRight && padInfo.Button2 == Buttons.RightThumbstickUp) ||
                        //   (padInfo.Button == Buttons.RightThumbstickRight && padInfo.Button2 == Buttons.RightThumbstickDown) ||
                        //   padInfo.Button == Buttons.RightThumbstickLeft || padInfo.Button == Buttons.RightThumbstickRight ||
                        //   padInfo.Button == Buttons.RightThumbstickUp || padInfo.Button == Buttons.RightThumbstickDown)
                        //{
                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //}
                        //if (padInfo.Button == Buttons.RightThumbstickLeft && padState.ThumbSticks.Right.X < -0.02f)
                        //{
                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //}
                        //if (padInfo.Button == Buttons.RightThumbstickRight && padState.ThumbSticks.Right.X > 0.02f)
                        //{
                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //}
                        //if (padInfo.Button == Buttons.RightThumbstickUp && padState.ThumbSticks.Right.Y > -0.02f)
                        //{
                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //}
                        //if (padInfo.Button == Buttons.RightThumbstickDown && padState.ThumbSticks.Right.Y < 0.02f)
                        //{
                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //}
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
