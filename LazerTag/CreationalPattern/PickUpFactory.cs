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
    /// enum for the pickup types
    /// </summary>
    public enum PickUpType { Battery, SpecialAmmo }

    public class PickUpFactory : Factory
    {
        #region singleton
        private static PickUpFactory instance;
        public static PickUpFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PickUpFactory();
                }
                return instance;
            }
        }
        #endregion

        #region field
        private GameObject batteryPrototype;
        private GameObject specialAmmoPrototype;
        #endregion

        #region constructor
        /// <summary>
        /// private constructor for PickUpFactory
        /// </summary>
        private PickUpFactory()
        {
            CreateBatteryPrototype();
            CreateSpecialAmmoPrototype();
        }
        #endregion

        #region methods
        /// <summary>
        /// Method for creating BatteryPrototype
        /// </summary>
        private void CreateBatteryPrototype()
        {
            batteryPrototype = new GameObject();
            batteryPrototype.AddComponent(new Battery());
            batteryPrototype.AddComponent(new SpriteRenderer());
            batteryPrototype.AddComponent(new Collider());
        }

        private void CreateSpecialAmmoPrototype()
        {
            specialAmmoPrototype = new GameObject();
            specialAmmoPrototype.AddComponent(new SpecialAmmo());
            specialAmmoPrototype.AddComponent(new SpriteRenderer());
            specialAmmoPrototype.AddComponent(new Collider());
        }

        /// <summary>
        /// Method for creating a gameobject, by cloning it
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>GameObject</returns>
        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            switch (type)
            {
                case PickUpType.Battery:
                    gameObject = (GameObject)batteryPrototype.Clone();
                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X/2, GameWorld.ScreenSize.Y/2);
                    break;
                case PickUpType.SpecialAmmo:
                    gameObject = (GameObject)specialAmmoPrototype.Clone();
                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X / 4, GameWorld.ScreenSize.Y / 2);
                    break;
            }

            return gameObject;
        }
        #endregion
    }
}
