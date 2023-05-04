using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using LazerTag.ComponentPattern;

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
            Character character = GameWorld.Instance.FindObjectOfType<Character>() as Character;
            GamePadState padState = GamePad.GetState(PlayerIndex.One);

            padbinds.Add(new PadInfo(Buttons.LeftThumbstickLeft), new MoveCommand(new Vector2(-1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickRight), new MoveCommand(new Vector2(1, 0)));
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickUp), new MoveCommand(new Vector2(0, -1)));
            padbinds.Add(new PadInfo(Buttons.LeftThumbstickDown), new MoveCommand(new Vector2(0, 1)));
        }

        public void Execute(Character character)
        {
            KeyboardState keyState = Keyboard.GetState();

            //check the device for player one
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            //if there is a controller attached, handle it
            if (capabilities.IsConnected)
            {
                //get the current state of Controller1
                GamePadState padState = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    foreach (PadInfo padInfo in padbinds.Keys)
                    {
                        //if (padState.IsButtonDown(padInfo.Button))
                        //{
                        //    if (padInfo.Button == Buttons.LeftThumbstickLeft && padState.ThumbSticks.Left.X < -0.5f)
                        //    {
                        //        character.PlayerDirection = character.Direction.Left;
                        //    }
                        //    if (padInfo.Button == Buttons.LeftThumbstickRight && padState.ThumbSticks.Left.X > 0.5f)
                        //    {
                        //        character.PlayerDirection = character.Direction.Right;
                        //    }
                        //    if (padInfo.Button == Buttons.LeftThumbstickUp && padState.ThumbSticks.Left.Y > 0.5f)
                        //    {
                        //        character.PlayerDirection = character.Direction.Up;
                        //    }
                        //    if (padInfo.Button == Buttons.LeftThumbstickDown && padState.ThumbSticks.Left.Y < -0.5f)
                        //    {
                        //        character.PlayerDirection = character.Direction.Down;
                        //    }

                        //    padbinds[padInfo].Execute(character);
                        //    padInfo.IsDown = true;
                        //    character.IsWalking = true;
                        //}

                        //if (!padState.IsButtonDown(padInfo.Button) && padInfo.IsDown == true)
                        //{
                        //    padInfo.IsDown = false;
                        //    character.IsWalking = false;
                        //}
                    }
                }
            }
        }
    }
}
