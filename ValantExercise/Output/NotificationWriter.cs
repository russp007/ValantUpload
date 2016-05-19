using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValantExercise.Output
{
    /// <summary>
    /// This class is an abstraction of the console as a destination for messages.
    /// </summary>
    public class NotificationWriter : INotificationWriter
    {
        #region INotificationWriter implementation

        public void WriteLine(string message)
        {
            Console.Out.WriteLine(message);
        }

        #endregion
    }
}