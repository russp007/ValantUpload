using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantExercise.Models;

namespace ValantExercise.Managers
{
    public interface IInventoryManager
    {
        void AddItem(Item item);
        Item RemoveItem(string label);
        void ProcessExpirations();
    }
}
