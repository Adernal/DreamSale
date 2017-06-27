using DreamSale.Common;
using DreamSale.Model.Vendors;

namespace DreamSale.Services.Vendors
{
    public partial interface IVendorService
    {
        IPagedList<Vendor> GetAllVendors(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Vendor GetVendorById(int vendorId);
        VendorNote GetVendorNoteById(int vendorNoteId);

        void DeleteVendor(Vendor vendor);
        void DeleteVendorNote(VendorNote vendorNote);
        void InsertVendor(Vendor vendor);
        void UpdateVendor(Vendor vendor);
    }
}