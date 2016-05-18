using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantExercise.Output;

namespace ValantExercise.Tests.Output
{
    /// <summary>
    /// This implementation of INotificationWriter is suitable for testing.  All messages written are
    /// retained and available for examination.
    /// </summary>
    public class TestNotificationWriter : INotificationWriter
    {
        private List<string> messages = new List<string>();

        /// <summary>
        /// The messages that have been written.
        /// </summary>
        public IEnumerable<string> Messages
        {
            get { return this.messages.ToArray(); }
        }

        #region INotificationWriter implementation

        /// <summary>
        /// Sends a notification.
        /// </summary>
        /// <param name="message">The message body.</param>
        public void WriteLine(string message)
        {
            Console.Out.WriteLine(message);
            messages.Add(message);
        }

        #endregion
    }
}
