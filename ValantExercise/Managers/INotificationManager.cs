using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValantExercise.Managers
{
    public interface INotificationManager
    {
        void Send(string message);
    }
}
