using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.CreationalPattern
{
    /// <summary>
    /// enum for the playertype/number
    /// </summary>
    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
        Player4
    }

    public class CharacterFactory : Factory
    {
        #region singleton
        private static CharacterFactory instance;

        public static CharacterFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CharacterFactory();
                }
                return instance;
            }
        }
        #endregion

        private List<GameObject> prototypes = new List<GameObject>();

        private CharacterFactory()
        {
            CreatePrototypeP1();
            CreatePrototypeP2();
            CreatePrototypeP3();
            CreatePrototypeP4();
        }

        #region methods

        private void CreatePrototypeP1()
        {

        }

        private void CreatePrototypeP2()
        {

        }

        private void CreatePrototypeP3()
        {

        }

        private void CreatePrototypeP4()
        {

        }

        public override GameObject Create(Enum type)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
