using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ValantExercise.Models;
using ValantExercise.Repositories;

namespace ValantExercise.Managers
{
    /// <summary>
    /// This class is responsible for working with the inventory.
    /// </summary>
    public class InventoryManager : IInventoryManager
    {
        #region Constants

        // messages
        private const string msgItemRemoved = @"Item '{0}' has been removed from inventory.";
        private const string msgExpired = @"Item '{0}' has expired.";

        #endregion

        #region Private Members

        private IInventoryRepository inventoryRep;
        private INotificationManager notificationMgr;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inventoryRep">Inventory repository.</param>
        /// <param name="notificationMgr">Notification manager.</param>
        public InventoryManager(IInventoryRepository inventoryRep, INotificationManager notificationMgr)
        {
            this.inventoryRep = inventoryRep;
            this.notificationMgr = notificationMgr;
        }

        #endregion

        #region IInventoryManager 

        /// <summary>
        /// Adds an item to inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            if (!inventoryRep.Exists(item.Label))
            {
                inventoryRep.Add(item);
            }
        }

        /// <summary>
        /// Removes an item from inventory and sends a notification.
        /// </summary>
        /// <param name="label">The label of the item to remove.</param>
        /// <returns>The item that was removed.</returns>
        public Item RemoveItem(string label)
        {
            Item result = null;

            if (inventoryRep.Exists(label))
            {
                result = inventoryRep.Get(label);
                inventoryRep.Delete(label);
                notificationMgr.Send(string.Format(InventoryManager.msgItemRemoved, label));
            }

            return result;
        }

        /// <summary>
        /// This method runs the expiration process.  Schedule controlled externally.
        /// </summary>
        public void ProcessExpirations()
        {
            // snag the expiration timestamp
            DateTime expirationDate = DateTime.Now;

            // fetch the newly expired items from our repository
            IEnumerable<Item> expiredItems = inventoryRep.GetExpiredItems(expirationDate);

            // process each expired item
            foreach (Item item in expiredItems)
            {
                // mark as expired
                // NOTE: This is sufficient as we are using an in-memory storage.  For a real-world
                //       scenario, this would have to go back to the inventory repository for persisting.
                item.Expired = true;

                // send the notification
                notificationMgr.Send(String.Format(InventoryManager.msgExpired, item.Label));
            }
        }

        #endregion
    }
}