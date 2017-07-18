using Denmakers.DreamSale.Model.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Manufacturers
{
    public partial interface IManufacturerTemplateService
    {
        /// <summary>
        /// Gets all manufacturer templates
        /// </summary>
        /// <returns>Manufacturer templates</returns>
        IList<ManufacturerTemplate> GetAllManufacturerTemplates();

        /// <summary>
        /// Gets a manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplateId">Manufacturer template identifier</param>
        /// <returns>Manufacturer template</returns>
        ManufacturerTemplate GetManufacturerTemplateById(int manufacturerTemplateId);

        /// <summary>
        /// Inserts manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        void InsertManufacturerTemplate(ManufacturerTemplate manufacturerTemplate);

        /// <summary>
        /// Updates the manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        void UpdateManufacturerTemplate(ManufacturerTemplate manufacturerTemplate);

        /// <summary>
        /// Delete manufacturer template
        /// </summary>
        /// <param name="manufacturerTemplate">Manufacturer template</param>
        void DeleteManufacturerTemplate(ManufacturerTemplate manufacturerTemplate);

    }
}
