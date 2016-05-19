using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ValantExercise.Models;

namespace ValantExercise.Repositories
{
    /// <summary>
    /// Provised the persistence mechanism for this application.
    /// </summary>
    public class InventoryRepository : IInventoryRepository
    {
        #region Private Members

        // for synchronization
        private static object syncRoot = new object();

        // in-memory storage for data
        protected static Dictionary<string, Item> items = new Dictionary<string, Item>();

        #endregion

        #region IInventoryRepository implementation

        /// <summary>
        /// Adds an item to the repository.
        /// </summary>
        /// <param name="item">The item to add.</param>
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

        /// <summary>
        /// Deletes an item from the repository.
        /// </summary>
        /// <param name="label">The label of the item to delete.</param>
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

        /// <summary>
        /// Indicates if the indicated item exists in the repository.
        /// </summary>
        /// <param name="label">The label of the item to check.</param>
        /// <returns>True if the item exists in the repository, otherwise false.</returns>
        public bool Exists(string label)
        {
            return items.ContainsKey(label);
        }

        /// <summary>
        /// Retrieves an item from the repository.
        /// </summary>
        /// <param name="label">The label of the item to retrieve.</param>
        /// <returns>The requested item.</returns>
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

        /// <summary>
        /// Retrieves from the repository all expired items.
        /// </summary>
        /// <param name="expirationDate">The date to use to determine expiration.</param>
        /// <returns>An enumeration of items who have expired.</returns>
        public IEnumerable<Item> GetExpiredItems(DateTime expirationDate)
        {
            // locate the items that have expired
            IEnumerable<Item> expired;
            lock (InventoryRepository.syncRoot)
            {
                // fetch the expired items
                expired = InventoryRepository.items.Values
                    .Where(i => i.Expiration < expirationDate)
                    .Where(i => i.Expired.Equals(false))
                    .Select(i => i);
            }

            // return
            return expired;
        }

        #endregion
    }
}