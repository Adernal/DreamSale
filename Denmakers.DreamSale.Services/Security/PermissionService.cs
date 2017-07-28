using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Security;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Security
{
    public partial class PermissionService : IPermissionService
    {
        #region Fields

        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        //private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public PermissionService(IRepository<PermissionRecord> permissionRecordRepository,
            ICustomerService customerService,
            IWorkContext workContext,
             ILocalizationService localizationService,
            ILanguageService languageService,
            IUnitOfWork unitOfWork)
        {
            this._permissionRecordRepository = permissionRecordRepository;
            this._customerService = customerService;
            this._workContext = workContext;
            this._localizationService = localizationService;
            this._languageService = languageService;
            //this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Utilities

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

            foreach (var permission1 in customerRole.PermissionRecords)
                if (permission1.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                    return true;

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Delete(permission);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="permissionId">Permission identifier</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        /// <summary>
        /// Gets a permission
        /// </summary>
        /// <param name="systemName">Permission system name</param>
        /// <returns>Permission</returns>
        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
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

        /// <summary>
        /// Gets all permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }

        /// <summary>
        /// Inserts a permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Insert(permission);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the permission
        /// </summary>
        /// <param name="permission">Permission</param>
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRecordRepository.Update(permission);

            //_unitOfWork.Commit();
        }

        ///// <summary>
        ///// Install permissions
        ///// </summary>
        ///// <param name="permissionProvider">Permission provider</param>
        //public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        //{
        //    //install new permissions
        //    var permissions = permissionProvider.GetPermissions();
        //    foreach (var permission in permissions)
        //    {
        //        var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
        //        if (permission1 == null)
        //        {
        //            //new permission (install it)
        //            permission1 = new PermissionRecord
        //            {
        //                Name = permission.Name,
        //                SystemName = permission.SystemName,
        //                Category = permission.Category,
        //            };


        //            //default customer role mappings
        //            var defaultPermissions = permissionProvider.GetDefaultPermissions();
        //            foreach (var defaultPermission in defaultPermissions)
        //            {
        //                var customerRole = _customerService.GetCustomerRoleBySystemName(defaultPermission.CustomerRoleSystemName);
        //                if (customerRole == null)
        //                {
        //                    //new role (save it)
        //                    customerRole = new CustomerRole
        //                    {
        //                        Name = defaultPermission.CustomerRoleSystemName,
        //                        Active = true,
        //                        SystemName = defaultPermission.CustomerRoleSystemName
        //                    };
        //                    _customerService.InsertCustomerRole(customerRole);
        //                }


        //                var defaultMappingProvided = (from p in defaultPermission.PermissionRecords
        //                                              where p.SystemName == permission1.SystemName
        //                                              select p).Any();
        //                var mappingExists = (from p in customerRole.PermissionRecords
        //                                     where p.SystemName == permission1.SystemName
        //                                     select p).Any();
        //                if (defaultMappingProvided && !mappingExists)
        //                {
        //                    permission1.CustomerRoles.Add(customerRole);
        //                }
        //            }

        //            //save new permission
        //            InsertPermissionRecord(permission1);

        //            //save localization
        //            permission1.SaveLocalizedPermissionName(_localizationService, _languageService);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Uninstall permissions
        ///// </summary>
        ///// <param name="permissionProvider">Permission provider</param>
        //public virtual void UninstallPermissions(IPermissionProvider permissionProvider)
        //{
        //    var permissions = permissionProvider.GetPermissions();
        //    foreach (var permission in permissions)
        //    {
        //        var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
        //        if (permission1 != null)
        //        {
        //            DeletePermissionRecord(permission1);

        //            //delete permission locales
        //            permission1.DeleteLocalizedPermissionName(_localizationService, _languageService);
        //        }
        //    }

        //}

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, _workContext.CurrentCustomer);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(PermissionRecord permission, Customer customer)
        {
            if (permission == null || customer == null)
                return false;

            return Authorize(permission.SystemName, customer);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string permissionRecordSystemName, Customer customer)
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

        #endregion
    }
}
