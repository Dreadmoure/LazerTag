using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    public class ProjectileFactory : Factory
    {
        #region singleton
        private ProjectileFactory instance;
        public ProjectileFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ProjectileFactory();
                }
                return instance;
            }
        }
        #endregion

        private ProjectileFactory()
        {
            CreatePrototype1();
        }

        private void CreatePrototype1()
        {

        }

        public override GameObject Create(Enum type)
        {
            GameObject gameObject = new GameObject();

            return gameObject;
        }
    }
}
