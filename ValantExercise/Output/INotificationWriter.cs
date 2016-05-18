using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValantExercise.Output
{
    public interface INotificationWriter
    {
        void WriteLine(string message);
    }
}
