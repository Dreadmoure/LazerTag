using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.ObserverPattern
{
    /// <summary>
    /// Interface for a listener which can be implemented
    /// </summary>
    public interface IGameListener
    {
        /// <summary>
        /// method which can be implemented to listen for an event
        /// </summary>
        /// <param name="gameEvent">the event that needs to happen</param>
        public void Notify(GameEvent gameEvent);
    }
}
