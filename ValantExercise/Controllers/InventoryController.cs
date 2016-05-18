using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ValantExercise.Managers;
using ValantExercise.Models;

namespace ValantExercise.Controllers
{
    /// <summary>
    /// Controller class for dealing with inventory.
    /// </summary>
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiController
    {
        #region Data Members

        // managers
        private IInventoryManager inventoryMgr;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inventoryMgr">The inventory manager object.</param>
        public InventoryController(IInventoryManager inventoryMgr)
        {
            this.inventoryMgr = inventoryMgr;
        }

        #endregion

        #region Web Methods

        /// <summary>
        /// This method handles the POST request to add a new item.
        /// </summary>
        /// <param name="item">The model object representing the item to add.</param>
        [Route("")]
        [HttpPost]
        public void AddItem([FromBody] Item item)
        {
            // check the input
            if ((item == null) || (!ModelState.IsValid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            this.inventoryMgr.AddItem(item);
        }

        /// <summary>
        /// This method handles the DELETE request to remove an item.
        /// </summary>
        /// <param name="label">Identifies the item to remove.</param>
        /// <returns>The item that has been removed.</returns>
        [Route("{label}")]
        [HttpDelete]
        public Item RemoveItem(string label)
        {
            // check the input
            if (String.IsNullOrEmpty(label))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // fetch and return
            Item result = this.inventoryMgr.RemoveItem(label);
            return result;
        }

        #endregion
    }
}
