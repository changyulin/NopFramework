using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Data;
using Nop.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Security
{
    public class PermissionService : IPermissionService
    {
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// {1} : permission system name
        /// </remarks>
        private const string PERMISSIONS_ALLOWED_KEY = "Nop.permission.allowed-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PERMISSIONS_PATTERN_KEY = "Nop.permission.";

        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;

        public PermissionService(IRepository<PermissionRecord> permissionRecordRepository,
            ICustomerService customerService,
            IWorkContext workContext,
            ICacheManager cacheManager)
        {
            this._permissionRecordRepository = permissionRecordRepository;
            this._customerService = customerService;
            this._workContext = workContext;
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customerRole">Customer role</param>
        /// <returns>true - authorized; otherwise, false</returns>
        protected virtual bool Authorize(string permissionRecordSystemName, CustomerRole customerRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            string key = string.Format(PERMISSIONS_ALLOWED_KEY, customerRole.Id, permissionRecordSystemName);
            return _cacheManager.Get(key, () =>
            {
                foreach (var permission1 in customerRole.PermissionRecords)
                    if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }

        public void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Delete(permission);

            _cacheManager.RemoveByPattern(PERMISSIONS_PATTERN_KEY);
        }

        public PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        public PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }

        public IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        public void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Insert(permission);

            _cacheManager.RemoveByPattern(PERMISSIONS_PATTERN_KEY);
        }

        public void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Update(permission);

            _cacheManager.RemoveByPattern(PERMISSIONS_PATTERN_KEY);
        }

        public void InstallPermissions(IPermissionProvider permissionProvider)
        {
            throw new NotImplementedException();
        }

        public void UninstallPermissions(IPermissionProvider permissionProvider)
        {
            throw new NotImplementedException();
        }

        public bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentCustomer);
        }

        public bool Authorize(PermissionRecord permission, Customer customer)
        {
            if (permission == null)
                return false;

            if (customer == null)
                return false;

            //old implementation of Authorize method
            //var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            //foreach (var role in customerRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;

            //return false;

            return Authorize(permission.SystemName, customer);
        }

        public bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }

        public bool Authorize(string permissionRecordSystemName, Customer customer)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            foreach (var role in customerRoles)
                if (Authorize(permissionRecordSystemName, role))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
    }
}
