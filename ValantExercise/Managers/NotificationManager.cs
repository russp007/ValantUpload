using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValantExercise.Output;

namespace ValantExercise.Managers
{
    public class NotificationManager : INotificationManager
    {
        public INotificationWriter writer;

        public NotificationManager(INotificationWriter writer)
        {
            this.writer = writer;
        }

        #region INotificationManager implementation

        public void Send(string message)
        {
            this.writer.WriteLine(message);
        }

        #endregion
    }
}