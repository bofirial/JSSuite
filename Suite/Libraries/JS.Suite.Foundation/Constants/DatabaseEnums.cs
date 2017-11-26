using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.Foundation.Constants
{
    /// <summary>
    /// Applications
    /// </summary>
    public enum Applications
    {
        /// <summary> James And Jennifer Wedding </summary>
        JamesAndJenniferWedding = 1,
        /// <summary> Justin And Nicole Wedding </summary>
        JustinAndNicoleWedding = 2,
        /// <summary> Hub </summary>
        Hub = 3,
        /// <summary> Code Generation </summary>
        CodeGeneration = 4,
        /// <summary> Unit Test </summary>
        UnitTest = 5,
        /// <summary> Games </summary>
        Games = 6,
        /// <summary> Collections </summary>
        Collections = 7,
        /// <summary> Support </summary>
        Support = 8
    }

    /// <summary>
    /// Message Handlers
    /// </summary>
    public enum MessageHandlers
    {
        /// <summary> Trace </summary>
        Trace = 1,
        /// <summary> Application Log </summary>
        ApplicationLog = 2,
        /// <summary> Email </summary>
        Email = 3
    }

    /// <summary>
    /// Message Types
    /// </summary>
    public enum MessageTypes
    {
        /// <summary> Application Error </summary>
        ApplicationError = 1,
        /// <summary> Database Error </summary>
        DatabaseError = 2,
        /// <summary> Http Request </summary>
        HttpRequest = 3,
        /// <summary> Database Query </summary>
        DatabaseQuery = 4,
        /// <summary> Email Sent </summary>
        EmailSent = 5,
        /// <summary> Spam Wedding Comment </summary>
        SpamWeddingComment = 6,
        /// <summary> Nicole And Justin Wedding Comment </summary>
        NicoleAndJustinWeddingComment = 7,
        /// <summary> Jennifer And James Wedding Comment </summary>
        JenniferAndJamesWeddingComment = 8,
        /// <summary> Application StartUp </summary>
        ApplicationStartUp = 9,
        /// <summary> Application Shutdown </summary>
        ApplicationShutdown = 10,
        /// <summary> Not Found 404 </summary>
        NotFound404 = 11,
        /// <summary> Debug Message </summary>
        DebugMessage = 12,
        /// <summary> New User Account Registered </summary>
        NewUserAccountRegistered = 13,
        /// <summary> Failed Cookie Deserialization </summary>
        FailedCookieDeserialization = 14,
        /// <summary> User Logged In </summary>
        UserLoggedIn = 15,
        /// <summary> User Locked Out </summary>
        UserLockedOut = 16,
        /// <summary> Confirmation Email Sent </summary>
        ConfirmationEmailSent = 17,
        /// <summary> Email Confirmed </summary>
        EmailConfirmed = 18,
        /// <summary> Password Reset Email Sent </summary>
        PasswordResetEmailSent = 19,
        /// <summary> Password Reset </summary>
        PasswordReset = 20

    }

    /// <summary>
    /// JSUserTypes
    /// </summary>
    public enum JSUserTypes
    {
        /// <summary> Admin </summary>
        Admin = 1,
        /// <summary> Beta User </summary>
        BetaUser = 2,
        /// <summary> User </summary>
        User = 3
    }

    /// <summary>
    /// Wedding Sites
    /// </summary>
    public enum WeddingSites
    {
        /// <summary> Jennifer and James Wedding </summary>
        JenniferAndJamesWedding = 1,
        /// <summary> Nikki and Justin Wedding </summary>
        NicoleAndJustinWedding = 2
    }
}
