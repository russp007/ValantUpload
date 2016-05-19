using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantExercise.Models;

namespace ValantExercise.Repositories
{
    /// <summary>
    /// This is the contract for working with the inventory repository.
    /// </summary>
    public interface IInventoryRepository
    {
        bool Exists(string label);
        void Add(Item item);
        void Delete(string label);
        Item Get(string label);
        IEnumerable<Item> GetExpiredItems(DateTime expirationDate);
    }
}
