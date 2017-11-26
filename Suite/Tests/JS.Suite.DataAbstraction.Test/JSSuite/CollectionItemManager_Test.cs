using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Constants;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.Test.JSSuite
{
    /// <summary>
    /// Summary description for CollectionItemManager_Test
    /// </summary>
    [TestClass]
    public class CollectionItemManager_Test
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        List<string> movieNames = new List<string>()
        {
            "Surf Ninjas",
            "Batman",
            "Batman Returns",
            "Robin Hood",
            "Batman Forever",
            "Gladiator",
            "Lord of the Rings: Fellowship of the Ring",
            "Lord of the Rings: The Two Towers",
            "Lord of the Rings: The Return of the King",
            "The Hobbit: An Unexpected Journey",
            "The Hobbit: Desolation of Smaug",
            "The Hobbit: There and Back Again"
        };

        [TestMethod]
        public async Task DataAccess_Test()
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();

            List<CollectionItem> collectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, new CollectionItem());

            Assert.IsTrue(collectionItems != null);

            if (collectionItems.Count > 0)
            {
                CollectionItem filter = collectionItems[0];

                List<CollectionItem> filteredCollectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, filter);

                Assert.AreEqual(filteredCollectionItems.Count, 1);

                filter.Name += Guid.NewGuid();

                List<CollectionItem> emptyCollectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, filter);

                Assert.AreEqual(emptyCollectionItems.Count, 0);
            }

            Random rnd = new Random();

            CollectionItem newCollectionItem = new CollectionItem()
            {
                Name = movieNames[rnd.Next(movieNames.Count)]
            };

            IProcessResult result = await CollectionItemManager.Current.InsertAsync(connectionInfo, newCollectionItem);

            Assert.AreEqual(result.ResultCode, ResultCodes.Success);

            Assert.IsTrue(newCollectionItem.CollectionItemId != default(int));

            List<CollectionItem> insertedCollectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, new CollectionItem() { CollectionItemId = newCollectionItem.CollectionItemId });

            Assert.AreEqual(insertedCollectionItems.Count, 1);
            Assert.AreEqual(insertedCollectionItems[0].Name, newCollectionItem.Name);

            CollectionItem updateCollectionItem = new CollectionItem()
            {
                CollectionItemId = newCollectionItem.CollectionItemId,
                Name = newCollectionItem.Name + ": The Sequel"
            };

            IProcessResult updateResult = await CollectionItemManager.Current.UpdateAsync(connectionInfo, updateCollectionItem);

            Assert.AreEqual(updateResult.ResultCode, ResultCodes.Success);

            List<CollectionItem> updatedCollectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, new CollectionItem() { CollectionItemId = updateCollectionItem.CollectionItemId });

            Assert.AreEqual(updatedCollectionItems.Count, 1);
            Assert.AreNotEqual(updatedCollectionItems[0].Name, newCollectionItem.Name);
            Assert.AreEqual(updatedCollectionItems[0].Name, updateCollectionItem.Name);

            IProcessResult deleteResult = await CollectionItemManager.Current.DeleteAsync(connectionInfo, new CollectionItem() { CollectionItemId = updateCollectionItem.CollectionItemId });

            Assert.AreEqual(deleteResult.ResultCode, ResultCodes.Success);

            List<CollectionItem> deletedCollectionItems = await CollectionItemManager.Current.SelectAsync(connectionInfo, new CollectionItem() { CollectionItemId = updateCollectionItem.CollectionItemId });

            Assert.AreEqual(deletedCollectionItems.Count, 0);
        }
    }
}
