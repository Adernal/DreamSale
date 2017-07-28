using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Messages
{
    public partial class MessageTemplateService : IMessageTemplateService
    {
        #region Fields
        private readonly IRepository<MessageTemplate> _messageTemplateRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly ILanguageService _languageService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly CatalogSettings _catalogSettings;
        private readonly ISettingService _settingService;
        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public MessageTemplateService(IRepository<StoreMapping> storeMappingRepository,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IStoreMappingService storeMappingService,
            IRepository<MessageTemplate> messageTemplateRepository,
            ISettingService settingService,
            IUnitOfWork unitOfWork)
        {
            this._storeMappingRepository = storeMappingRepository;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._storeMappingService = storeMappingService;
            this._messageTemplateRepository = messageTemplateRepository;
            this._settingService = settingService;
            //this._unitOfWork = unitOfWork;
            this._catalogSettings = _settingService.LoadSetting<CatalogSettings>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateId">Message template identifier</param>
        /// <returns>Message template</returns>
        public virtual MessageTemplate GetMessageTemplateById(int messageTemplateId)
        {
            if (messageTemplateId == 0)
                return null;

            return _messageTemplateRepository.GetById(messageTemplateId);
        }

        /// <summary>
        /// Gets a message template
        /// </summary>
        /// <param name="messageTemplateName">Message template name</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Message template</returns>
        public virtual MessageTemplate GetMessageTemplateByName(string messageTemplateName, int storeId)
        {
            if (string.IsNullOrWhiteSpace(messageTemplateName))
                throw new ArgumentException("messageTemplateName");

            var query = _messageTemplateRepository.Table;
            query = query.Where(t => t.Name == messageTemplateName);
            query = query.OrderBy(t => t.Id);
            var templates = query.ToList();

            //store mapping
            if (storeId > 0)
            {
                templates = templates
                    .Where(t => _storeMappingService.Authorize(t, storeId))
                    .ToList();
            }

            return templates.FirstOrDefault();
        }

        /// <summary>
        /// Gets all message templates
        /// </summary>
        /// <param name="storeId">Store identifier; pass 0 to load all records</param>
        /// <returns>Message template list</returns>
        public virtual IList<MessageTemplate> GetAllMessageTemplates(int storeId)
        {
            var query = _messageTemplateRepository.Table;
            query = query.OrderBy(t => t.Name);

            //Store mapping
            if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
            {
                query = from t in query
                        join sm in _storeMappingRepository.Table
                        on new { c1 = t.Id, c2 = "MessageTemplate" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into t_sm
                        from sm in t_sm.DefaultIfEmpty()
                        where !t.LimitedToStores || storeId == sm.StoreId
                        select t;

                //only distinct items (group by ID)
                query = from t in query
                        group t by t.Id
                        into tGroup
                        orderby tGroup.Key
                        select tGroup.FirstOrDefault();
                query = query.OrderBy(t => t.Name);
            }

            return query.ToList();
        }

        /// <summary>
        /// Create a copy of message template with all depended data
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        /// <returns>Message template copy</returns>
        public virtual MessageTemplate CopyMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            var mtCopy = new MessageTemplate
            {
                Name = messageTemplate.Name,
                BccEmailAddresses = messageTemplate.BccEmailAddresses,
                Subject = messageTemplate.Subject,
                Body = messageTemplate.Body,
                IsActive = messageTemplate.IsActive,
                AttachedDownloadId = messageTemplate.AttachedDownloadId,
                EmailAccountId = messageTemplate.EmailAccountId,
                LimitedToStores = messageTemplate.LimitedToStores,
                DelayBeforeSend = messageTemplate.DelayBeforeSend,
                DelayPeriod = messageTemplate.DelayPeriod
            };

            InsertMessageTemplate(mtCopy);

            var languages = _languageService.GetAllLanguages(true);

            //localization
            foreach (var lang in languages)
            {
                var bccEmailAddresses = messageTemplate.BccEmailAddresses;//.GetLocalized(x => x.BccEmailAddresses, lang.Id, false, false);
                if (!String.IsNullOrEmpty(bccEmailAddresses))
                    _localizedEntityService.SaveLocalizedValue(mtCopy, x => x.BccEmailAddresses, bccEmailAddresses, lang.Id);

                var subject = messageTemplate.Subject;//.GetLocalized(x => x.Subject, lang.Id, false, false);
                if (!String.IsNullOrEmpty(subject))
                    _localizedEntityService.SaveLocalizedValue(mtCopy, x => x.Subject, subject, lang.Id);

                var body = messageTemplate.Body;//.GetLocalized(x => x.Body, lang.Id, false, false);
                if (!String.IsNullOrEmpty(body))
                    _localizedEntityService.SaveLocalizedValue(mtCopy, x => x.Body, body, lang.Id);

                var emailAccountId = messageTemplate.EmailAccountId;//.GetLocalized(x => x.EmailAccountId, lang.Id, false, false);
                if (emailAccountId > 0)
                    _localizedEntityService.SaveLocalizedValue(mtCopy, x => x.EmailAccountId, emailAccountId, lang.Id);
            }

            //store mapping
            var selectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(messageTemplate);
            foreach (var id in selectedStoreIds)
            {
                _storeMappingService.InsertStoreMapping(mtCopy, id);
            }

            return mtCopy;
        }

        /// <summary>
        /// Inserts a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void InsertMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Insert(messageTemplate);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void UpdateMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Update(messageTemplate);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Delete a message template
        /// </summary>
        /// <param name="messageTemplate">Message template</param>
        public virtual void DeleteMessageTemplate(MessageTemplate messageTemplate)
        {
            if (messageTemplate == null)
                throw new ArgumentNullException("messageTemplate");

            _messageTemplateRepository.Delete(messageTemplate);
            //_unitOfWork.Commit();
        }

        #endregion
    }
}
