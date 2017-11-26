using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Suite.Foundation.Constants;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Security
{
	/// <summary>
	/// JS User Store
	/// </summary>
	public class JSUserStore : 
		IUserStore<JSUser, int>
		, IUserPasswordStore<JSUser, int>
		, IUserLoginStore<JSUser, int>
		, IUserClaimStore<JSUser, int>
		, IUserSecurityStampStore<JSUser, int>
		, IUserLockoutStore<JSUser, int>
		, IUserEmailStore<JSUser, int>
	{
		private async Task<JSUser> SelectJSUserByFilter(JSUser filter)
		{
			List<JSUser> jsUsers = await JSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, filter);

			return jsUsers.FirstOrDefault();
		}

		/// <summary>
		/// Creates a User Asynchronously
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Task CreateAsync(JSUser user)
		{
            user.JSUserTypeId = (int)JSUserTypes.User;
            user.FailedLoginAttempts = 0;

			return JSUserBusinessManager.Current.InsertAsync(SecurityManager.Current.ConnectionInfo, user);
		}

        /// <summary>
        /// Deletes a User Asynchronously
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
		public Task DeleteAsync(JSUser user)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Finds the User by it's JSUser Id
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public Task<JSUser> FindByIdAsync(int userId)
		{
			return SelectJSUserByFilter(new JSUser() { JSUserId = userId });
		}

		/// <summary>
		/// Finds the user by Name
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns></returns>
		public Task<JSUser> FindByNameAsync(string userName)
		{
			return SelectJSUserByFilter(new JSUser() { Name = userName });
		}

		/// <summary>
		/// Updates the JS User Asynchronously
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Task UpdateAsync(JSUser user)
		{
			return JSUserBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, user);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
		}

		/// <summary>
		/// Returns a User's Password Asynchronously
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Task<string> GetPasswordHashAsync(JSUser user)
		{
			return Task.FromResult(user.Password);
		}

		/// <summary>
		/// Returns whether a user has a Password Asynchronously
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Task<bool> HasPasswordAsync(JSUser user)
		{
			return Task.FromResult(!String.IsNullOrEmpty(user.Password));
		}

		/// <summary>
		/// Sets the password hash asynchronously.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="passwordHash">The password hash.</param>
		/// <returns></returns>
		public Task SetPasswordHashAsync(JSUser user, string passwordHash)
		{
			return Task.FromResult(user.Password = passwordHash);
		}

		/// <summary>
		/// Adds an External Login Provider to a User Asynchronously
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="login">The login.</param>
		/// <returns></returns>
		public Task AddLoginAsync(JSUser user, UserLoginInfo login)
		{
			switch (login.LoginProvider)
			{
				case ExternalLoginProviders.Google:
					user.GoogleId = login.ProviderKey;
					break;
				case ExternalLoginProviders.Facebook:
					user.FacebookId = login.ProviderKey;
					break;
				case ExternalLoginProviders.Twitter:
					user.TwitterId = login.ProviderKey;
					break;
				default:
					throw new Exception("Unexpected Login Provider: " + login.LoginProvider);
			}

			return JSUserBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, user);
		}

		/// <summary>
		/// Finds a User by it's External Provider Id
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		public Task<JSUser> FindAsync(UserLoginInfo login)
		{
			JSUser filter = new JSUser();

			switch (login.LoginProvider)
			{
				case ExternalLoginProviders.Google:
					filter.GoogleId = login.ProviderKey;
					break;
				case ExternalLoginProviders.Facebook:
					filter.FacebookId = login.ProviderKey;
					break;
				case ExternalLoginProviders.Twitter:
					filter.TwitterId = login.ProviderKey;
					break;
				default:
					throw new Exception("Unexpected Login Provider: " + login.LoginProvider);
			}

			return SelectJSUserByFilter(filter);
		}

		/// <summary>
		/// Gets the User's Logins Asynchronously
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<IList<UserLoginInfo>> GetLoginsAsync(JSUser user)
		{
			IList<UserLoginInfo> userLoginInfos = new List<UserLoginInfo>();

			if (!String.IsNullOrEmpty(user.GoogleId))
			{
				userLoginInfos.Add(new UserLoginInfo(ExternalLoginProviders.Google, user.GoogleId));
			}

			if (!String.IsNullOrEmpty(user.FacebookId))
			{
				userLoginInfos.Add(new UserLoginInfo(ExternalLoginProviders.Facebook, user.FacebookId));
			}

			if (!String.IsNullOrEmpty(user.TwitterId))
			{
				userLoginInfos.Add(new UserLoginInfo(ExternalLoginProviders.Twitter, user.TwitterId));
			}

			return Task.FromResult(userLoginInfos);
		}

		/// <summary>
		/// Removes the login asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="login">The login.</param>
		/// <returns></returns>
		public Task RemoveLoginAsync(JSUser user, UserLoginInfo login)
		{
			switch (login.LoginProvider)
			{
				case ExternalLoginProviders.Google:
					user.GoogleId = null;
					break;
				case ExternalLoginProviders.Facebook:
					user.FacebookId = null;
					break;
				case ExternalLoginProviders.Twitter:
					user.TwitterId = null;
					break;
				default:
					throw new Exception("Unexpected Login Provider: " + login.LoginProvider);
			}

			return JSUserBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, user);
		}

		/// <summary>
		/// Adds the claim asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="claim">The claim.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Task AddClaimAsync(JSUser user, System.Security.Claims.Claim claim)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the claims asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<IList<Claim>> GetClaimsAsync(JSUser user)
		{
			IList<Claim> claims = new List<Claim>();

			JSUserTypes userType = (JSUserTypes)user.JSUserTypeId;

			claims.Add(new Claim(JSClaimTypes.UserType, EnumHelper.Current.GetName(userType)));

			switch (userType)
			{
				case JSUserTypes.Admin:
					//claims.Add(new Claim(JSClaimTypes.Application, EnumHelper.Current.GetName(Applications.Games)));
					claims.Add(new Claim(JSClaimTypes.Application, EnumHelper.Current.GetName(Applications.Collections)));
					claims.Add(new Claim(JSClaimTypes.Application, EnumHelper.Current.GetName(Applications.Support)));
					break;
				case JSUserTypes.BetaUser:
				case JSUserTypes.User:
                default:
                    claims.Add(new Claim(JSClaimTypes.Application, EnumHelper.Current.GetName(Applications.Collections)));
					break;
			}

			return Task.FromResult(claims);
		}

        /// <summary>
        /// Removes the claim asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
		public Task RemoveClaimAsync(JSUser user, System.Security.Claims.Claim claim)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Gets the security stamp asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
		public Task<string> GetSecurityStampAsync(JSUser user)
		{
            return Task.FromResult(user.SecurityStamp);
		}

        /// <summary>
        /// Sets the security stamp asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="stamp">The stamp.</param>
        /// <returns></returns>
		public Task SetSecurityStampAsync(JSUser user, string stamp)
		{
            user.SecurityStamp = stamp;

            return Task.FromResult(user);
		}

		/// <summary>
		/// Gets the access failed count asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<int> GetAccessFailedCountAsync(JSUser user)
        {
            return Task.FromResult(user.FailedLoginAttempts);
		}

		/// <summary>
		/// Gets the lockout enabled asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<bool> GetLockoutEnabledAsync(JSUser user)
		{
            return Task.FromResult(true);
		}

		/// <summary>
		/// Gets the lockout end date asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<DateTimeOffset> GetLockoutEndDateAsync(JSUser user)
		{
            return Task.FromResult(new DateTimeOffset(user.LockoutEndOn ?? DateTime.MinValue));
		}

		/// <summary>
		/// Increments the access failed count asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public async Task<int> IncrementAccessFailedCountAsync(JSUser user)
        {
            user.FailedLoginAttempts += 1;

            await JSUserBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, user);

            return user.FailedLoginAttempts;
		}

		/// <summary>
		/// Resets the access failed count asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task ResetAccessFailedCountAsync(JSUser user)
        {
            user.FailedLoginAttempts = 0;

            return Task.FromResult(user);
		}

		/// <summary>
		/// Sets the lockout enabled asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		/// <returns></returns>
		public Task SetLockoutEnabledAsync(JSUser user, bool enabled)
        {
            return Task.FromResult(user);
		}

		/// <summary>
		/// Sets the lockout end date asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="lockoutEnd">The lockout end.</param>
		/// <returns></returns>
		public Task SetLockoutEndDateAsync(JSUser user, DateTimeOffset lockoutEnd)
        {
            if (lockoutEnd == DateTimeOffset.MinValue)
            {
                user.LockoutEndOn = null;
                user.LockoutFlag = false;
            }
            else
            {
                user.LockoutEndOn = lockoutEnd.UtcDateTime;
                user.LockoutFlag = true;
            }

            return Task.FromResult(user);
		}

		/// <summary>
		/// Finds the by email asynchronous.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public Task<JSUser> FindByEmailAsync(string email)
		{
            return SelectJSUserByFilter(new JSUser()
            {
                Email = email,
                EmailConfirmedFlag = true
            });
		}

		/// <summary>
		/// Gets the email asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<string> GetEmailAsync(JSUser user)
		{
            return Task.FromResult(user.Email);
		}

		/// <summary>
		/// Gets the email confirmed asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public Task<bool> GetEmailConfirmedAsync(JSUser user)
        {
            return Task.FromResult(user.EmailConfirmedFlag);
		}

		/// <summary>
		/// Sets the email asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public Task SetEmailAsync(JSUser user, string email)
        {
            user.Email = email;

            return Task.FromResult(user);
		}

		/// <summary>
		/// Sets the email confirmed asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="confirmed">if set to <c>true</c> [confirmed].</param>
		/// <returns></returns>
		public Task SetEmailConfirmedAsync(JSUser user, bool confirmed)
        {
            user.EmailConfirmedFlag = confirmed;

            return Task.FromResult(user);
		}
	}
}
