using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JS.Core.Foundation.Helpers;
using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSuite;
using System.Threading.Tasks;
using JS.Core.Foundation.Constants;
using Microsoft.AspNet.Identity;

namespace JS.Suite.DataAbstraction.Test.JSSuite
{
    /// <summary>
    /// JSUserManager_Test
    /// </summary>
    [TestClass]
    public class JSUserManager_Test
    {
        /// <summary>
        /// JSUserManager Data Access Test
        /// </summary>
        [TestMethod]
        public async Task JSUserManager_Data_Access_Test()
        {
            IConnectionInfo connectionInfo = UnitTestHelper.Current.GetTestConnectionInfo();

            //Select all JSUsers
            List<JSUser> jsUsers = await JSUserManager.Current.SelectAsync(connectionInfo, new JSUser());

            Assert.IsTrue(jsUsers != null, "JSUsers Query Returned Null.");

            if (jsUsers.Count > 0)
            {
                JSUser filter = jsUsers[0];

                List<JSUser> filteredJSUsers = await JSUserManager.Current.SelectAsync(connectionInfo, filter);

                Assert.AreEqual(filteredJSUsers.Count, 1, "JSUser Filter Returned No Results.");

                filter.Name += Guid.NewGuid();

                List<JSUser> emptyCollectionItems = await JSUserManager.Current.SelectAsync(connectionInfo, filter);

                Assert.AreEqual(emptyCollectionItems.Count, 0, "Guid Filter Returned A Result.");
            }

            JSUser newJSUser = new JSUser()
            {
                Name = "Test User" + Guid.NewGuid()
            };

            IProcessResult result = await JSUserManager.Current.InsertAsync(connectionInfo, newJSUser);

            Assert.AreEqual(result.ResultCode, ResultCodes.Success, "Insert was not successful");

            Assert.IsTrue(newJSUser.JSUserId != default(int), "JSUserId was not populated");

            List<JSUser> insertedJSUsers = await JSUserManager.Current.SelectAsync(connectionInfo, new JSUser() { JSUserId = newJSUser.JSUserId });

            Assert.AreEqual(insertedJSUsers.Count, 1, "Inserted JSUser Not Found in the Database");
            Assert.AreEqual(insertedJSUsers[0].Name, newJSUser.Name, "Inserted JSUser's Name Does Not Match the Database's Value.");

            JSUser updatedJSUser = new JSUser()
            {
                JSUserId = newJSUser.JSUserId,
                Name = newJSUser.Name + " Updated"
            };

            IProcessResult updateResult = await JSUserManager.Current.UpdateAsync(connectionInfo, updatedJSUser);

            Assert.AreEqual(updateResult.ResultCode, ResultCodes.Success);

            List<JSUser> updatedJSUsers = await JSUserManager.Current.SelectAsync(connectionInfo, new JSUser() { JSUserId = updatedJSUser.JSUserId });

            Assert.AreEqual(updatedJSUsers.Count, 1, "Updated JSUser was not found in the database");
            Assert.AreNotEqual(updatedJSUsers[0].Name, newJSUser.Name, "Updated JSUser's Name was not updated in the database");
            Assert.AreEqual(updatedJSUsers[0].Name, updatedJSUser.Name, "Updated JSUser's Name does not match what was sent to the database");

            IProcessResult deleteResult = await JSUserManager.Current.DeleteAsync(connectionInfo, new JSUser() { JSUserId = updatedJSUser.JSUserId });

            Assert.AreEqual(deleteResult.ResultCode, ResultCodes.Success, "Delete was not successful");

            List<JSUser> deletedJSUsers = await JSUserManager.Current.SelectAsync(connectionInfo, new JSUser() { JSUserId = updatedJSUser.JSUserId });

            Assert.AreEqual(deletedJSUsers.Count, 0, "JSUser was not deleted");
        }
    }
}
