using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Categories
{
    public partial class CategoryTemplateService : ICategoryTemplateService
    {
        #region Fields
        private readonly IRepository<CategoryTemplate> _categoryTemplateRepository;
        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public CategoryTemplateService(IRepository<CategoryTemplate> categoryTemplateRepository, IUnitOfWork unitOfWork)
        {
            this._categoryTemplateRepository = categoryTemplateRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets all category templates
        /// </summary>
        /// <returns>Category templates</returns>
        public virtual IList<CategoryTemplate> GetAllCategoryTemplates()
        {
            var query = from pt in _categoryTemplateRepository.Table
                        orderby pt.DisplayOrder, pt.Id
                        select pt;

            var templates = query.ToList();
            return templates;
        }

        /// <summary>
        /// Gets a category template
        /// </summary>
        /// <param name="categoryTemplateId">Category template identifier</param>
        /// <returns>Category template</returns>
        public virtual CategoryTemplate GetCategoryTemplateById(int categoryTemplateId)
        {
            if (categoryTemplateId == 0)
                return null;

            return _categoryTemplateRepository.GetById(categoryTemplateId);
        }

        /// <summary>
        /// Inserts category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>
        public virtual void InsertCategoryTemplate(CategoryTemplate categoryTemplate)
        {
            if (categoryTemplate == null)
                throw new ArgumentNullException("categoryTemplate");

            _categoryTemplateRepository.Insert(categoryTemplate);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>
        public virtual void UpdateCategoryTemplate(CategoryTemplate categoryTemplate)
        {
            if (categoryTemplate == null)
                throw new ArgumentNullException("categoryTemplate");

            _categoryTemplateRepository.Update(categoryTemplate);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Delete category template
        /// </summary>
        /// <param name="categoryTemplate">Category template</param>
        public virtual void DeleteCategoryTemplate(CategoryTemplate categoryTemplate)
        {
            if (categoryTemplate == null)
                throw new ArgumentNullException("categoryTemplate");

            _categoryTemplateRepository.Delete(categoryTemplate);
            _unitOfWork.Commit();
        }

        #endregion
    }
}
