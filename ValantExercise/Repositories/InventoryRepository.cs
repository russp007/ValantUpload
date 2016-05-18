using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ValantExercise.Models;

namespace ValantExercise.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        #region Private Members

        // for synchronization
        private static object syncRoot = new object();

        // in-memory storage for data
        protected static Dictionary<string, Item> items = new Dictionary<string, Item>();

        #endregion

        #region IInventoryRepository implementation

        public void Add(Item item)
        {
            lock (InventoryRepository.syncRoot)
            {
                if (!items.ContainsKey(item.Label))
                {
                    items.Add(item.Label, item);
                }
            }
        }

        public void Delete(string label)
        {
            lock (InventoryRepository.syncRoot)
            {
                if (items.ContainsKey(label))
                {
                    items.Remove(label);
                }
            }
        }

        public bool Exists(string label)
        {
            return items.ContainsKey(label);
        }

        public Item Get(string label)
        {
            if (items.ContainsKey(label))
            {
                return items[label];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Item> GetExpiredItems(DateTime expirationDate)
        {
            // locate the items that have expired
            IEnumerable<Item> expired;
            lock (InventoryRepository.syncRoot)
            {
                // fetch the expired items
                expired = InventoryRepository.items.Values
                    .Where(i => i.Expiration > expirationDate)
                    .Where(i => i.Expired.Equals(false))
                    .Select(i => i);
            }

            // return
            return expired;
        }

        #endregion
    }
}