using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Nop.Services.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private Customer _cachedCustomer;

        public FormsAuthenticationService(HttpContextBase httpContext,
            ICustomerService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>Customer</returns>
        protected virtual Customer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var email = ticket.UserData;

            if (String.IsNullOrWhiteSpace(email))
                return null;
            var customer = _customerService.GetCustomerByEmail(email);
            return customer;
        }

        public void SignIn(Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                customer.Email,
                now,
                now.Add(this._expirationTimeSpan),
                createPersistentCookie,
                customer.Email,
                FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }

        public void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        public Customer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null && customer.Active && !customer.Deleted && customer.IsRegistered())
                _cachedCustomer = customer;
            return _cachedCustomer;
        }
    }
}
