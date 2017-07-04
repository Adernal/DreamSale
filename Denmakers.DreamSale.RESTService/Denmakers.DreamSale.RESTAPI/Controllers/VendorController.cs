using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Vendor")]
    public class VendorController : ApiControllerBase
    {
        #region Fields

        private readonly IVendorService _vendorService;

        #endregion

        #region Ctor
        public VendorController(IVendorService vendorService, IRepository<Log> log, IUnitOfWork unitOfWork)
            : base(log, unitOfWork)
        {
            this._vendorService = vendorService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllVendors(HttpRequestMessage request,string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var query = _vendorService.GetAllVendors();

                var vendors = new PagedList<Vendor>(query, pageIndex, pageSize);

                response = request.CreateResponse<List<Vendor>>(HttpStatusCode.OK, vendors);

                return response;
            });


            //var query = _vendorRepository.Table;
            //if (!String.IsNullOrWhiteSpace(name))
            //    query = query.Where(v => v.Name.Contains(name));
            //if (!showHidden)
            //    query = query.Where(v => v.Active);
            //query = query.Where(v => !v.Deleted);
            //query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name);

            //var vendors = new PagedList<Vendor>(query, pageIndex, pageSize);
            //return vendors;
        }

        [Route("GetById/{vendorId}")]
        public Vendor GetVendorById(int vendorId)
        {
            if (vendorId == 0)
                return null;

            return _vendorService.GetVendorById(vendorId);
        }

        [Route("GetVendorNotById/{Id}")]
        public VendorNote GetVendorNoteById(int vendorNoteId)
        {
            if (vendorNoteId == 0)
                return null;

            return _vendorService.GetVendorNoteById(vendorNoteId);
        }

        public virtual void AddVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            _vendorService.InsertVendor(vendor);
            _unitOfWork.Commit();
        }

        public virtual void UpdateVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            _vendorService.UpdateVendor(vendor);
        }

        public virtual void DeleteVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            vendor.Deleted = true;
            UpdateVendor(vendor);
        }
        #endregion
    }
}
