using LazerTag.ComponentPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    public enum PickUpType { Battery }

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

        private GameObject batteryPrototype;

        private PickUpFactory()
        {
            CreateBatteryPrototype();
        }

        private void CreateBatteryPrototype()
        {
            batteryPrototype = new GameObject();
            batteryPrototype.AddComponent(new Battery());
            batteryPrototype.AddComponent(new SpriteRenderer());
            batteryPrototype.AddComponent(new Collider());
        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            switch (type)
            {
                case PickUpType.Battery:
                    gameObject = (GameObject)batteryPrototype.Clone();
                    gameObject.Transform.Position = new Vector2(GameWorld.ScreenSize.X/2, GameWorld.ScreenSize.Y/2);
                    break;
            }

            return gameObject;
        }
    }
}
