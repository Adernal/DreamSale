using System;
using System.Collections.Generic;
using System.Linq;
using DreamSale.Model.Topics;
using DreamSale.Data.DataRepository;

namespace DreamSale.Services.Topics
{
    /// <summary>
    /// Topic template service
    /// </summary>
    public partial class TopicTemplateService : ITopicTemplateService
    {
        #region Fields

        private readonly IRepository<TopicTemplate> _topicTemplateRepository;
        //private readonly IEventPublisher _eventPublisher;

        #endregion
        
        #region Ctor
        public TopicTemplateService(IRepository<TopicTemplate> topicTemplateRepository)
        {
            this._topicTemplateRepository = topicTemplateRepository;
        }

        #endregion

        #region Methods
        
        public virtual IList<TopicTemplate> GetAllTopicTemplates()
        {
            var tems = _topicTemplateRepository.Table.OrderBy(tt => tt.DisplayOrder).OrderBy(tt => tt.Id).ToList();
            var query = from pt in _topicTemplateRepository.Table
                        orderby pt.DisplayOrder, pt.Id
                        select pt;

            var templates = query.ToList();
            return templates;
        }

        public virtual TopicTemplate GetTopicTemplateById(int topicTemplateId)
        {
            if (topicTemplateId == 0)
                return null;

            return _topicTemplateRepository.GetById(topicTemplateId);
        }
        
        public virtual void InsertTopicTemplate(TopicTemplate topicTemplate)
        {
            if (topicTemplate == null)
                throw new ArgumentNullException("topicTemplate");

            _topicTemplateRepository.Insert(topicTemplate);

            //event notification
            //_eventPublisher.EntityInserted(topicTemplate);
        }

        public virtual void UpdateTopicTemplate(TopicTemplate topicTemplate)
        {
            if (topicTemplate == null)
                throw new ArgumentNullException("topicTemplate");

            _topicTemplateRepository.Update(topicTemplate);

            //event notification
            //_eventPublisher.EntityUpdated(topicTemplate);
        }

        public virtual void DeleteTopicTemplate(TopicTemplate topicTemplate)
        {
            if (topicTemplate == null)
                throw new ArgumentNullException("topicTemplate");

            _topicTemplateRepository.Delete(topicTemplate);

            //event notification
            //_eventPublisher.EntityDeleted(topicTemplate);
        }
        #endregion
    }
}
