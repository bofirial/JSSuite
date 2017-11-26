using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.BusinessLogic.JSSuite;
using Microsoft.AspNet.Identity;
using JS.Core.Foundation.Constants;
using System.Threading.Tasks;
using JS.Suite.DataAbstraction.JSSuite;
using System.Threading;
using System.Security.Claims;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Helpers;

namespace JS.Suite.BusinessLogic.Test.Security
{
    /// <summary>
    /// Security Manager Test
    /// </summary>
    [TestClass]
    public class SecurityManager_Test
    {
        /// <summary>
        /// Signs the in async_ test.
        /// </summary>
        [TestMethod]
        public void SignInAsync_Test()
        {
            //Assert.AreEqual(Thread.CurrentPrincipal.Identity.IsAuthenticated, false, "User is already authenticated.");

            JSUser user = JSUserBusinessManager.Current.UserManager.FindById((int)SystemUsers.UnitTestUser);

            SecurityManager.Current.SignIn(user);

            Assert.AreEqual(user.UserName, Thread.CurrentPrincipal.Identity.Name, "UserName did not match.");
            Assert.AreEqual(true, Thread.CurrentPrincipal.Identity.IsAuthenticated, "User is not authenticated.");
            
            JSUser user2 = JSUserBusinessManager.Current.UserManager.FindById(100008);

            SecurityManager.Current.SignIn(user2);

            Assert.AreEqual(Thread.CurrentPrincipal.Identity.Name, user2.UserName, "UserName did not match.");

            BackgroundProcessHelper.Current.Trigger((cancellationToken) =>
            {
                var bill = SecurityManager.Current.ConnectionInfo;
            });
        }
    }
}
