using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.Common;

namespace Denmakers.DreamSale.Services.Vendors
{
    public partial interface IVendorService
    {
        Vendor GetVendorById(int vendorId);

        IPagedList<Vendor> GetAllVendors(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        void InsertVendor(Vendor vendor);

        void UpdateVendor(Vendor vendor);

        VendorNote GetVendorNoteById(int vendorNoteId);

        void DeleteVendor(Vendor vendor);

        void DeleteVendorNote(VendorNote vendorNote);
    }
}
