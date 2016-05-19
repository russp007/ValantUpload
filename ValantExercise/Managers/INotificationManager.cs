using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValantExercise.Managers
{
    /// <summary>
    /// Defines the contract for working with the notification manager.
    /// </summary>
    public interface INotificationManager
    {
        void Send(string message);
    }
}
