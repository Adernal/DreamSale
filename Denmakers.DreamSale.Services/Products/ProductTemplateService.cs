using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Products
{
    public partial class ProductTemplateService : IProductTemplateService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductTemplate> _productTemplateRepository;

        #endregion

        #region Ctor
        public ProductTemplateService(IRepository<ProductTemplate> productTemplateRepository, IUnitOfWork unitOfWork)
        {
            this._productTemplateRepository = productTemplateRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets all product templates
        /// </summary>
        /// <returns>Product templates</returns>
        public virtual IList<ProductTemplate> GetAllProductTemplates()
        {
            var query = from pt in _productTemplateRepository.Table
                        orderby pt.DisplayOrder, pt.Id
                        select pt;

            var templates = query.ToList();
            return templates;
        }

        /// <summary>
        /// Gets a product template
        /// </summary>
        /// <param name="productTemplateId">Product template identifier</param>
        /// <returns>Product template</returns>
        public virtual ProductTemplate GetProductTemplateById(int productTemplateId)
        {
            if (productTemplateId == 0)
                return null;

            return _productTemplateRepository.GetById(productTemplateId);
        }

        /// <summary>
        /// Inserts product template
        /// </summary>
        /// <param name="productTemplate">Product template</param>
        public virtual void InsertProductTemplate(ProductTemplate productTemplate)
        {
            if (productTemplate == null)
                throw new ArgumentNullException("productTemplate");

            _productTemplateRepository.Insert(productTemplate);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the product template
        /// </summary>
        /// <param name="productTemplate">Product template</param>
        public virtual void UpdateProductTemplate(ProductTemplate productTemplate)
        {
            if (productTemplate == null)
                throw new ArgumentNullException("productTemplate");

            _productTemplateRepository.Update(productTemplate);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Delete product template
        /// </summary>
        /// <param name="productTemplate">Product template</param>
        public virtual void DeleteProductTemplate(ProductTemplate productTemplate)
        {
            if (productTemplate == null)
                throw new ArgumentNullException("productTemplate");

            _productTemplateRepository.Delete(productTemplate);

            _unitOfWork.Commit();
        }
        #endregion
    }
}
