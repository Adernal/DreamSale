using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Manufacturers
{
    public partial class ManufacturerTemplateService : IManufacturerTemplateService
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ManufacturerTemplate> _manufacturerTemplateRepository;

        #endregion

        #region Ctor
        public ManufacturerTemplateService(IRepository<ManufacturerTemplate> manufacturerTemplateRepository, IUnitOfWork unitOfWork)
        {
            this._manufacturerTemplateRepository = manufacturerTemplateRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        public virtual void DeleteManufacturerTemplate(ManufacturerTemplate manufacturerTemplate)
        {
            if (manufacturerTemplate == null)
                throw new ArgumentNullException("manufacturerTemplate");

            _manufacturerTemplateRepository.Delete(manufacturerTemplate);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all manufacturer templates
        /// </summary>
        /// <returns>Manufacturer templates</returns>
        public virtual IList<ManufacturerTemplate> GetAllManufacturerTemplates()
        {
            var query = from pt in _manufacturerTemplateRepository.Table
                        orderby pt.DisplayOrder, pt.Id
                        select pt;

            var templates = query.ToList();
            return templates;
        }

        /// <summary>
        /// Gets a manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplateId">Manufacturer template identifier</param>
        /// <returns>Manufacturer template</returns>
        public virtual ManufacturerTemplate GetManufacturerTemplateById(int manufacturerTemplateId)
        {
            if (manufacturerTemplateId == 0)
                return null;

            return _manufacturerTemplateRepository.GetById(manufacturerTemplateId);
        }

        /// <summary>
        /// Inserts manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        public virtual void InsertManufacturerTemplate(ManufacturerTemplate manufacturerTemplate)
        {
            if (manufacturerTemplate == null)
                throw new ArgumentNullException("manufacturerTemplate");

            _manufacturerTemplateRepository.Insert(manufacturerTemplate);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        public virtual void UpdateManufacturerTemplate(ManufacturerTemplate manufacturerTemplate)
        {
            if (manufacturerTemplate == null)
                throw new ArgumentNullException("manufacturerTemplate");

            _manufacturerTemplateRepository.Update(manufacturerTemplate);
            _unitOfWork.Commit();
        }

        #endregion
    }
}
