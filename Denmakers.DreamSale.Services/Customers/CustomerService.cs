using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Denmakers.DreamSale.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPassword> _customerPasswordRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<GenericAttribute> _gaRepository;
        private readonly IRepository<Order> _orderRepository;
        //private readonly IRepository<ForumPost> _forumPostRepository;
        //private readonly IRepository<ForumTopic> _forumTopicRepository;
        //private readonly IRepository<BlogComment> _blogCommentRepository;
        //private readonly IRepository<NewsComment> _newsCommentRepository;
        //private readonly IRepository<PollVotingRecord> _pollVotingRecordRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IRepository<ProductReviewHelpfulness> _productReviewHelpfulnessRepository;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDbContext _dbContext;
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly CustomerSettings _customerSettings;
        private readonly CommonSettings _commonSettings;
        #endregion

        #region Ctor

        public CustomerService(/*ICacheManager cacheManager,*/
            IRepository<Customer> customerRepository,
            IRepository<CustomerPassword> customerPasswordRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<GenericAttribute> gaRepository,
            IRepository<Order> orderRepository,
            IUnitOfWork unitOfWork,
            IDbContext dbContext,
            ISettingService settingService,
            //IRepository<ForumPost> forumPostRepository,
            //IRepository<ForumTopic> forumTopicRepository,
            //IRepository<BlogComment> blogCommentRepository,
            //IRepository<NewsComment> newsCommentRepository,
            //IRepository<PollVotingRecord> pollVotingRecordRepository,
            IRepository<ProductReview> productReviewRepository,
            IRepository<ProductReviewHelpfulness> productReviewHelpfulnessRepository,
            IGenericAttributeService genericAttributeService,
            IWebHelper webHelper
            )
        {
            this._unitOfWork = unitOfWork;
            //this._cacheManager = cacheManager;
            this._customerRepository = customerRepository;
            this._customerPasswordRepository = customerPasswordRepository;
            this._customerRoleRepository = customerRoleRepository;
            this._gaRepository = gaRepository;
            this._orderRepository = orderRepository;
            //this._forumPostRepository = forumPostRepository;
            //this._forumTopicRepository = forumTopicRepository;
            //this._blogCommentRepository = blogCommentRepository;
            //this._newsCommentRepository = newsCommentRepository;
            //this._pollVotingRecordRepository = pollVotingRecordRepository;
            this._productReviewRepository = productReviewRepository;
            this._productReviewHelpfulnessRepository = productReviewHelpfulnessRepository;
            this._genericAttributeService = genericAttributeService;
            this._settingService = settingService;
            this._dbContext = dbContext;
            this._webHelper = webHelper;
            this._customerSettings = _settingService.LoadSetting<CustomerSettings>();
            this._commonSettings = _settingService.LoadSetting<CommonSettings>();
        }

        #endregion

        #region Methods

        #region Customers

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="email">Email; null to load all customers</param>
        /// <param name="username">Username; null to load all customers</param>
        /// <param name="firstName">First name; null to load all customers</param>
        /// <param name="lastName">Last name; null to load all customers</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all customers</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all customers</param>
        /// <param name="company">Company; null to load all customers</param>
        /// <param name="phone">Phone; null to load all customers</param>
        /// <param name="zipPostalCode">Phone; null to load all customers</param>
        /// <param name="ipAddress">IP address; null to load all customers</param>
        /// <param name="loadOnlyWithShoppingCart">Value indicating whether to load customers only with shopping cart</param>
        /// <param name="sct">Value indicating what shopping cart type to filter; userd when 'loadOnlyWithShoppingCart' param is 'true'</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
        public virtual IPagedList<Customer> GetAllCustomers(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
            int[] customerRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null,
            string ipAddress = null, bool loadOnlyWithShoppingCart = false, ShoppingCartType? sct = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            if (affiliateId > 0)
                query = query.Where(c => affiliateId == c.AffiliateId);
            if (vendorId > 0)
                query = query.Where(c => vendorId == c.VendorId);
            query = query.Where(c => !c.Deleted);
            if (customerRoleIds != null && customerRoleIds.Length > 0)
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Intersect(customerRoleIds).Any());
            if (!String.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!String.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username.Contains(username));
            if (!String.IsNullOrWhiteSpace(firstName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.FirstName &&
                        z.Attribute.Value.Contains(firstName)))
                    .Select(z => z.Customer);
            }
            if (!String.IsNullOrWhiteSpace(lastName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.LastName &&
                        z.Attribute.Value.Contains(lastName)))
                    .Select(z => z.Customer);
            }
            //date of birth is stored as a string into database.
            //we also know that date of birth is stored in the following format YYYY-MM-DD (for example, 1983-02-18).
            //so let's search it as a string
            if (dayOfBirth > 0 && monthOfBirth > 0)
            {
                //both are specified
                string dateOfBirthStr = monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-" + dayOfBirth.ToString("00", CultureInfo.InvariantCulture);
                //EndsWith is not supported by SQL Server Compact
                //so let's use the following workaround http://social.msdn.microsoft.com/Forums/is/sqlce/thread/0f810be1-2132-4c59-b9ae-8f7013c0cc00

                //we also cannot use Length function in SQL Server Compact (not supported in this context)
                //z.Attribute.Value.Length - dateOfBirthStr.Length = 5
                //dateOfBirthStr.Length = 5
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Substring(5, 5) == dateOfBirthStr))
                    .Select(z => z.Customer);
            }
            else if (dayOfBirth > 0)
            {
                //only day is specified
                string dateOfBirthStr = dayOfBirth.ToString("00", CultureInfo.InvariantCulture);
                //EndsWith is not supported by SQL Server Compact
                //so let's use the following workaround http://social.msdn.microsoft.com/Forums/is/sqlce/thread/0f810be1-2132-4c59-b9ae-8f7013c0cc00

                //we also cannot use Length function in SQL Server Compact (not supported in this context)
                //z.Attribute.Value.Length - dateOfBirthStr.Length = 8
                //dateOfBirthStr.Length = 2
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Substring(8, 2) == dateOfBirthStr))
                    .Select(z => z.Customer);
            }
            else if (monthOfBirth > 0)
            {
                //only month is specified
                string dateOfBirthStr = "-" + monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-";
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Contains(dateOfBirthStr)))
                    .Select(z => z.Customer);
            }
            //search by company
            if (!String.IsNullOrWhiteSpace(company))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.Company &&
                        z.Attribute.Value.Contains(company)))
                    .Select(z => z.Customer);
            }
            //search by phone
            if (!String.IsNullOrWhiteSpace(phone))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.Phone &&
                        z.Attribute.Value.Contains(phone)))
                    .Select(z => z.Customer);
            }
            //search by zip
            if (!String.IsNullOrWhiteSpace(zipPostalCode))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.ZipPostalCode &&
                        z.Attribute.Value.Contains(zipPostalCode)))
                    .Select(z => z.Customer);
            }

            //search by IpAddress
            if (!String.IsNullOrWhiteSpace(ipAddress) && CommonHelper.IsValidIpAddress(ipAddress))
            {
                query = query.Where(w => w.LastIpAddress == ipAddress);
            }

            if (loadOnlyWithShoppingCart)
            {
                int? sctId = null;
                if (sct.HasValue)
                    sctId = (int)sct.Value;

                query = sct.HasValue ?
                    query.Where(c => c.ShoppingCartItems.Any(x => x.ShoppingCartTypeId == sctId)) :
                    query.Where(c => c.ShoppingCartItems.Any());
            }

            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var customers = new PagedList<Customer>(query, pageIndex, pageSize);
            return customers;
        }

        /// <summary>
        /// Gets online customers
        /// </summary>
        /// <param name="lastActivityFromUtc">Customer last activity date (from)</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
        public virtual IPagedList<Customer> GetOnlineCustomers(DateTime lastActivityFromUtc,
            int[] customerRoleIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.Table;
            query = query.Where(c => lastActivityFromUtc <= c.LastActivityDateUtc);
            query = query.Where(c => !c.Deleted);
            if (customerRoleIds != null && customerRoleIds.Length > 0)
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Intersect(customerRoleIds).Any());

            query = query.OrderByDescending(c => c.LastActivityDateUtc);
            var customers = new PagedList<Customer>(query, pageIndex, pageSize);
            return customers;
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void DeleteCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (customer.IsSystemAccount)
                throw new DreamSaleException(string.Format("System customer account ({0}) could not be deleted", customer.SystemName));

            customer.Deleted = true;

            if (_customerSettings.SuffixDeletedCustomers)
            {
                if (!String.IsNullOrEmpty(customer.Email))
                    customer.Email += "-DELETED";
                if (!String.IsNullOrEmpty(customer.Username))
                    customer.Username += "-DELETED";
            }

            UpdateCustomer(customer);
            _unitOfWork.Commit();

            //event notification
            //_eventPublisher.EntityDeleted(customer);
        }

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer</returns>
        public virtual Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return _customerRepository.GetById(customerId);
        }

        /// <summary>
        /// Get customers by identifiers
        /// </summary>
        /// <param name="customerIds">Customer identifiers</param>
        /// <returns>Customers</returns>
        public virtual IList<Customer> GetCustomersByIds(int[] customerIds)
        {
            if (customerIds == null || customerIds.Length == 0)
                return new List<Customer>();

            var query = from c in _customerRepository.Table
                        where customerIds.Contains(c.Id) && !c.Deleted
                        select c;
            var customers = query.ToList();
            //sort by passed identifiers
            var sortedCustomers = new List<Customer>();
            foreach (int id in customerIds)
            {
                var customer = customers.Find(x => x.Id == id);
                if (customer != null)
                    sortedCustomers.Add(customer);
            }
            return sortedCustomers;
        }

        /// <summary>
        /// Gets a customer by GUID
        /// </summary>
        /// <param name="customerGuid">Customer GUID</param>
        /// <returns>A customer</returns>
        public virtual Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;

            var query = from c in _customerRepository.Table
                        where c.CustomerGuid == customerGuid
                        orderby c.Id
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.SystemName == systemName
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Insert a guest customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer InsertGuestCustomer()
        {
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                LastIpAddress = _webHelper.GetCurrentIpAddress()
            };

            //add to 'Guests' role
            var guestRole = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);
            if (guestRole == null)
                throw new DreamSaleException("'Guests' role could not be loaded");
            customer.CustomerRoles.Add(guestRole);

            _customerRepository.Insert(customer);
            _unitOfWork.Commit();
            return customer;
        }

        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Insert(customer);
            _unitOfWork.Commit();

            //event notification
            //_eventPublisher.EntityInserted(customer);
        }

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
            _unitOfWork.Commit();
            //event notification
            //_eventPublisher.EntityUpdated(customer);
        }

        /// <summary>
        /// Reset data required for checkout
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCouponCodes">A value indicating whether to clear coupon code</param>
        /// <param name="clearCheckoutAttributes">A value indicating whether to clear selected checkout attributes</param>
        /// <param name="clearRewardPoints">A value indicating whether to clear "Use reward points" flag</param>
        /// <param name="clearShippingMethod">A value indicating whether to clear selected shipping method</param>
        /// <param name="clearPaymentMethod">A value indicating whether to clear selected payment method</param>
        public virtual void ResetCheckoutData(Customer customer, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true)
        {
            if (customer == null)
                throw new ArgumentNullException();

            //clear entered coupon codes
            if (clearCouponCodes)
            {
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.DiscountCouponCode, null);
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.GiftCardCouponCodes, null);
            }

            //clear checkout attributes
            if (clearCheckoutAttributes)
            {
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.CheckoutAttributes, null, storeId);
            }

            //clear reward points flag
            if (clearRewardPoints)
            {
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.UseRewardPointsDuringCheckout, false, storeId);
            }

            //clear selected shipping method
            if (clearShippingMethod)
            {
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.SelectedShippingOption, null, storeId);
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.OfferedShippingOptions, null, storeId);
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.SelectedPickupPoint, null, storeId);
            }

            //clear selected payment method
            if (clearPaymentMethod)
            {
                _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.SelectedPaymentMethod, null, storeId);
            }

            UpdateCustomer(customer);
        }

        /// <summary>
        /// Delete guest customer records
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="onlyWithoutShoppingCart">A value indicating whether to delete customers only without shopping cart</param>
        /// <returns>Number of deleted customers</returns>
        public virtual int DeleteGuestCustomers(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart)
        {
            //stored procedures aren't supported. Use LINQ

            #region No stored procedure

            var guestRole = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);
            if (guestRole == null)
                throw new DreamSaleException("'Guests' role could not be loaded");

            var query = _customerRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Contains(guestRole.Id));
            if (onlyWithoutShoppingCart)
                query = query.Where(c => !c.ShoppingCartItems.Any());
            //no orders
            query = from c in query
                    join o in _orderRepository.Table on c.Id equals o.CustomerId into c_o
                    from o in c_o.DefaultIfEmpty()
                    where !c_o.Any()
                    select c;
            ////no blog comments
            //query = from c in query
            //        join bc in _blogCommentRepository.Table on c.Id equals bc.CustomerId into c_bc
            //        from bc in c_bc.DefaultIfEmpty()
            //        where !c_bc.Any()
            //        select c;
            ////no news comments
            //query = from c in query
            //        join nc in _newsCommentRepository.Table on c.Id equals nc.CustomerId into c_nc
            //        from nc in c_nc.DefaultIfEmpty()
            //        where !c_nc.Any()
            //        select c;
            //no product reviews
            query = from c in query
                    join pr in _productReviewRepository.Table on c.Id equals pr.CustomerId into c_pr
                    from pr in c_pr.DefaultIfEmpty()
                    where !c_pr.Any()
                    select c;
            //no product reviews helpfulness
            query = from c in query
                    join prh in _productReviewHelpfulnessRepository.Table on c.Id equals prh.CustomerId into c_prh
                    from prh in c_prh.DefaultIfEmpty()
                    where !c_prh.Any()
                    select c;
            //no poll voting
            //query = from c in query
            //        join pvr in _pollVotingRecordRepository.Table on c.Id equals pvr.CustomerId into c_pvr
            //        from pvr in c_pvr.DefaultIfEmpty()
            //        where !c_pvr.Any()
            //        select c;
            //no forum posts 
            //query = from c in query
            //        join fp in _forumPostRepository.Table on c.Id equals fp.CustomerId into c_fp
            //        from fp in c_fp.DefaultIfEmpty()
            //        where !c_fp.Any()
            //        select c;
            //no forum topics
            //query = from c in query
            //        join ft in _forumTopicRepository.Table on c.Id equals ft.CustomerId into c_ft
            //        from ft in c_ft.DefaultIfEmpty()
            //        where !c_ft.Any()
            //        select c;
            //don't delete system accounts
            query = query.Where(c => !c.IsSystemAccount);

            //only distinct customers (group by ID)
            query = from c in query
                    group c by c.Id
                        into cGroup
                    orderby cGroup.Key
                    select cGroup.FirstOrDefault();
            query = query.OrderBy(c => c.Id);
            var customers = query.ToList();


            int totalRecordsDeleted = 0;
            foreach (var c in customers)
            {
                try
                {
                    //delete attributes
                    var attributes = _genericAttributeService.GetAttributesForEntity(c.Id, "Customer");
                    _genericAttributeService.DeleteAttributes(attributes);

                    //delete from database
                    _customerRepository.Delete(c);
                    totalRecordsDeleted++;
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }
            _unitOfWork.Commit();
            return totalRecordsDeleted;

            #endregion

        }

        #endregion

        #region Customer roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void DeleteCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            if (customerRole.IsSystemRole)
                throw new DreamSaleException("System role could not be deleted");

            _customerRoleRepository.Delete(customerRole);
            _unitOfWork.Commit();

            //_cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityDeleted(customerRole);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleById(int customerRoleId)
        {
            if (customerRoleId == 0)
                return null;

            return _customerRoleRepository.GetById(customerRoleId);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Id
                        where cr.SystemName == systemName
                        select cr;
            var customerRole = query.FirstOrDefault();
            return customerRole;
        }

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer roles</returns>
        public virtual IList<CustomerRole> GetAllCustomerRoles(bool showHidden = false)
        {
            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Name
                        where showHidden || cr.Active
                        select cr;
            var customerRoles = query.ToList();
            return customerRoles;
        }

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Insert(customerRole);
            _unitOfWork.Commit();

            //_cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityInserted(customerRole);
        }

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Update(customerRole);

            //_cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            ////event notification
            //_eventPublisher.EntityUpdated(customerRole);
        }

        #endregion

        #region Customer passwords

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        public virtual IList<CustomerPassword> GetCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _customerPasswordRepository.Table;

            //filter by customer
            if (customerId.HasValue)
                query = query.Where(password => password.CustomerId == customerId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)(passwordFormat.Value));

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        public virtual CustomerPassword GetCurrentPassword(int customerId)
        {
            if (customerId == 0)
                return null;

            //return the latest password
            return GetCustomerPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual void InsertCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException("customerPassword");

            _customerPasswordRepository.Insert(customerPassword);
            _unitOfWork.Commit();

            //event notification
            //_eventPublisher.EntityInserted(customerPassword);
        }

        /// <summary>
        /// Update a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual void UpdateCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException("customerPassword");

            _customerPasswordRepository.Update(customerPassword);

            //event notification
            /*_eventPublisher.EntityUpdated(customerPassword);*/
        }

        #endregion

        #endregion
    }
}
