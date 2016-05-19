using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantExercise.Models;

namespace ValantExercise.Managers
{
    /// <summary>
    /// Defines the contract for working with the inventory manager.
    /// </summary>
    public interface IInventoryManager
    {
        void AddItem(Item item);
        Item RemoveItem(string label);
        void ProcessExpirations();
    }
}
