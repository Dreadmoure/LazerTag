using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag
{
    public class UI
    {
        #region singleton
        private static UI instance;

        public static UI Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UI();
                }
                return instance;
            }
        }
        #endregion

        private UI()
        {

        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
