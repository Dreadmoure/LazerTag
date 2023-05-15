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
    public enum PickUpType { Battery, SpecialAmmo, SolarUpgrade }

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
        private GameObject solarUpgradePrototype;
        #endregion

        #region constructor
        /// <summary>
        /// private constructor for PickUpFactory
        /// </summary>
        private PickUpFactory()
        {
            CreateBatteryPrototype();
            CreateSpecialAmmoPrototype();
            CreateSolarUpgrade();
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

        /// <summary>
        /// Method for creating SpecialAmmoPrototype
        /// </summary>
        private void CreateSpecialAmmoPrototype()
        {
            specialAmmoPrototype = new GameObject();
            specialAmmoPrototype.AddComponent(new SpecialAmmo());
            specialAmmoPrototype.AddComponent(new SpriteRenderer());
            specialAmmoPrototype.AddComponent(new Collider());
        }

        /// <summary>
        /// Method for creating SolarUpgrade
        /// </summary>
        private void CreateSolarUpgrade()
        {
            solarUpgradePrototype = new GameObject();
            solarUpgradePrototype.AddComponent(new SolarUpgrade());
            solarUpgradePrototype.AddComponent(new SpriteRenderer());
            solarUpgradePrototype.AddComponent(new Collider());
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
                    break;
                case PickUpType.SpecialAmmo:
                    gameObject = (GameObject)specialAmmoPrototype.Clone();
                    break;
                case PickUpType.SolarUpgrade:
                    gameObject = (GameObject)solarUpgradePrototype.Clone();
                    break;
            }

            return gameObject;
        }
        #endregion
    }
}
