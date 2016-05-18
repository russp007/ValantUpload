using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValantExercise.Output
{
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