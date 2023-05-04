using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag
{
    /// <summary>
    /// used to create a pixel based on the input
    /// </summary>
    public class RectangleData
    {

        #region properties
        /// <summary>
        /// Property for getting and setting a pixel
        /// </summary>
        public Rectangle Rectangle { get; set; }

        /// <summary>
        /// Property for getting and setting the value of X used in updating the position of the rectangle
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Property for getting and setting the value of Y used in updating the position of the rectangle
        /// </summary>
        public int Y { get; private set; }
        #endregion

        #region constructor
        /// <summary>
        /// Constructor which takes 2 parameters
        /// </summary>
        /// <param name="x">positions x value</param>
        /// <param name="y">positions y value</param>
        public RectangleData(int x, int y)
        {
            this.Rectangle = new Rectangle();
            this.X = x;
            this.Y = y;
        }
        #endregion

        #region methods
        /// <summary>
        /// Updates the position of the pixel based on the parameters
        /// </summary>
        /// <param name="gameObject">object which contains the pixel</param>
        /// <param name="width">objects width</param>
        /// <param name="height">objects height</param>
        public void UpdatePosition(GameObject gameObject, int width, int height)
        {
            Rectangle = new Rectangle((int)gameObject.Transform.Position.X + X - width / 2, (int)gameObject.Transform.Position.Y + Y - height / 2, 1, 1);
        }
        #endregion
    }
}
