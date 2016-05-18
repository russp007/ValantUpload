using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ValantExercise.Models;
using ValantExercise.Repositories;

namespace ValantExercise.Managers
{
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

        public InventoryManager(IInventoryRepository inventoryRep, INotificationManager notificationMgr)
        {
            this.inventoryRep = inventoryRep;
            this.notificationMgr = notificationMgr;
        }

        #region IInventoryManager 

        public void AddItem(Item item)
        {
            if (!inventoryRep.Exists(item.Label))
            {
                inventoryRep.Add(item);
            }
        }

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

        public void ProcessExpirations()
        {
            // snag the expiration timestamp
            DateTime expirationDate = DateTime.Now;

            // fetch the newly expired items from our repository
            IEnumerable<Item> expiredItems = inventoryRep.GetExpiredItems(expirationDate);

            // process each expired item
            foreach (Item item in expiredItems)
            {
                // mark as expired and send notification
                item.Expired = true;
                notificationMgr.Send(String.Format(InventoryManager.msgExpired, item.Label));
            }
        }

        #endregion
    }
}