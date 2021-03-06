﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValantExercise.Controllers;
using ValantExercise.Managers;
using ValantExercise.Models;
using ValantExercise.Output;
using ValantExercise.Repositories;
using ValantExercise.Tests.Output;

namespace ValantExercise.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTest
    {
        #region Tests for AddItem()

        [TestMethod]
        [ExpectedException(typeof(System.Web.Http.HttpResponseException))]
        public void TestAddItem_ConfirmFailureOnNullArgument()
        {
            // arrange
            //
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new NotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);

            // act
            //
            controller.AddItem(null);

            // assert
            //
            // ... we should never make it this far
        }

        [TestMethod]
        [ExpectedException(typeof(System.Web.Http.HttpResponseException))]
        public void TestAddItem_ConfirmFailureOnMissingLabel()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new NotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);
            Item item = new Item
            {
                Label = null,                           // Label is required, so this is invalid
                Expiration = DateTime.Now.AddDays(5),
                Type = "Type 2"
            };

            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(item, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(item, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // act
            //
            controller.AddItem(item);

            // assert
            //
            // ... we should never make it this far
        }

        [TestMethod]
        [ExpectedException(typeof(System.Web.Http.HttpResponseException))]
        public void TestAddItem_ConfirmFailureOnEmptyLabel()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new NotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);
            Item item = new Item
            {
                Label = String.Empty,                           // Label is required, so this is invalid
                Expiration = DateTime.Now.AddDays(5),
                Type = "Type 2"
            };

            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(item, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(item, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // act
            //
            controller.AddItem(item);

            // assert
            //
            // ... we should never make it this far
        }

        [TestMethod]
        public void TestAddItem_ConfirmSuccessOnHappyPath()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new TestNotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);
            Item item = new Item
            {
                Label = "Label 2",                           // Label is required, so this is invalid
                Expiration = DateTime.Now.AddDays(5),
                Type = "Type 2"
            };

            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(item, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(item, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // act
            //
            controller.AddItem(item);

            // assert
            //
            
            // we expect to see the item in our repository
            Item testItem = inventoryRep.Get(item.Label);
            Assert.AreEqual(item, testItem);
            
            // confirm that no notifications were sent (as per requirements)
            List<string> messages = new List<string>();
            messages.AddRange(((TestNotificationWriter)writer).Messages);
            Assert.IsTrue(messages.Count.Equals(0));
        }

        #endregion

        #region Tests for RemoveItem()

        [TestMethod]
        [ExpectedException(typeof(System.Web.Http.HttpResponseException))]
        public void TestRemoveItem_ConfirmFailureOnNullArgument()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new TestNotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);

            // act
            //
            controller.RemoveItem(null);

            // assert
            //
            // ... we should not reach this point.
        }

        [TestMethod]
        [ExpectedException(typeof(System.Web.Http.HttpResponseException))]
        public void TestRemoveItem_ConfirmFailureOnEmptyArgument()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new TestNotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);

            // act
            //
            controller.RemoveItem(String.Empty);

            // assert
            //
            // ... we should not reach this point.
        }

        [TestMethod]
        public void TestRemoveItem_ConfirmSuccessOnHappyPath()
        {
            // arrange
            // 
            IInventoryRepository inventoryRep = new InventoryRepository();
            INotificationWriter writer = new TestNotificationWriter();
            INotificationManager notificationMgr = new NotificationManager(writer);
            IInventoryManager manager = new InventoryManager(inventoryRep, notificationMgr);
            InventoryController controller = new InventoryController(manager);
            Item item = new Item
            {
                Label = "Label 3", 
                Expiration = DateTime.Now.AddDays(5),
                Type = "Type 3"
            };

            // add the item to the repository in anticipation of the remove
            inventoryRep.Add(item);

            // act
            //
            Item removedItem = controller.RemoveItem(item.Label);

            // assert
            //

            // confirm that we got the correct instance back
            Assert.AreEqual(item, removedItem);

            // confirm that the item no longer exists in the repository
            Assert.IsFalse(inventoryRep.Exists(removedItem.Label));

            // confirm that we received a notification for this removal
            List<string> messages = new List<string>();
            messages.AddRange(((TestNotificationWriter)writer).Messages);
            Assert.AreEqual(messages.Count, 1);
            string target = String.Format("Item '{0}' has been removed from inventory.", removedItem.Label);
            Assert.AreEqual(messages[0], target);

        }

        #endregion
    }
}
