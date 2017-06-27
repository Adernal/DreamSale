using System.Collections.Generic;
using DreamSale.Model.Topics;

namespace DreamSale.Services.Topics
{
    /// <summary>
    /// Topic template service interface
    /// </summary>
    public partial interface ITopicTemplateService
    {
        IList<TopicTemplate> GetAllTopicTemplates();
        TopicTemplate GetTopicTemplateById(int topicTemplateId);
        void InsertTopicTemplate(TopicTemplate topicTemplate);
        void UpdateTopicTemplate(TopicTemplate topicTemplate);
        void DeleteTopicTemplate(TopicTemplate topicTemplate);
    }
}
