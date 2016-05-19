using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValantExercise.Output
{
    /// <summary>
    /// This contract details how we send notification messages.
    /// </summary>
    public interface INotificationWriter
    {
        void WriteLine(string message);
    }
}
