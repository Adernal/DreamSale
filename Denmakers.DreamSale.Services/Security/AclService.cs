using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Security;
using Denmakers.DreamSale.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Security
{
    public partial class AclService : IAclService
    {
        #region Fields

        private readonly IRepository<AclRecord> _aclRecordRepository;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor
        public AclService(IRepository<AclRecord> aclRecordRepository, ISettingService settingService, IWorkContext workContext, IUnitOfWork unitOfWork)
        {
            this._aclRecordRepository = aclRecordRepository;
            this._unitOfWork = unitOfWork;
            this._workContext = workContext;
            this._settingService = settingService;
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an ACL record
        /// </summary>
        /// <param name="aclRecordId">ACL record identifier</param>
        /// <returns>ACL record</returns>
        public virtual AclRecord GetAclRecordById(int aclRecordId)
        {
            if (aclRecordId == 0)
                return null;

            return _aclRecordRepository.GetById(aclRecordId);
        }

        /// <summary>
        /// Gets ACL records
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>ACL records</returns>
        public virtual IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in _aclRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName
                        select ur;
            var aclRecords = query.ToList();
            return aclRecords;
        }


        /// <summary>
        /// Inserts an ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void InsertAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Insert(aclRecord);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Inserts an ACL record
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="customerRoleId">Customer role id</param>
        /// <param name="entity">Entity</param>
        public virtual void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (customerRoleId == 0)
                throw new ArgumentOutOfRangeException("customerRoleId");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var aclRecord = new AclRecord
            {
                EntityId = entityId,
                EntityName = entityName,
                CustomerRoleId = customerRoleId
            };

            InsertAclRecord(aclRecord);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void UpdateAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Update(aclRecord);
        }

        /// <summary>
        /// Deletes an ACL record
        /// </summary>
        /// <param name="aclRecord">ACL record</param>
        public virtual void DeleteAclRecord(AclRecord aclRecord)
        {
            if (aclRecord == null)
                throw new ArgumentNullException("aclRecord");

            _aclRecordRepository.Delete(aclRecord);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Find customer role identifiers with granted access
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>Customer role identifiers</returns>
        public virtual int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in _aclRecordRepository.Table
                        where ur.EntityId == entityId &&
                        ur.EntityName == entityName
                        select ur.CustomerRoleId;
            return query.ToArray();
        }

        /// <summary>
        /// Authorize ACL permission
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported
        {
            return Authorize(entity, _workContext.CurrentCustomer);
            //return Authorize(entity, null);
        }

        /// <summary>
        /// Authorize ACL permission
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Wntity</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize<T>(T entity, Customer customer) where T : BaseEntity, IAclSupported
        {
            if (entity == null || customer == null)
                return false;

            if (_catalogSettings.IgnoreAcl || !entity.SubjectToAcl)
                return true;

            foreach (var role1 in customer.CustomerRoles.Where(cr => cr.Active))
                foreach (var role2Id in GetCustomerRoleIdsWithAccess(entity))
                    if (role1.Id == role2Id)
                        //yes, we have such permission
                        return true;

            //no permission found
            return false;
        }
        #endregion
    }
}
