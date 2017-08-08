using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Security
{
    public partial class PermissionMappingVM
    {
        public PermissionMappingVM()
        {
            AvailablePermissions = new List<PermissionRecordVM>();
            AvailableCustomerRoles = new List<CustomerRoleVM>();
            Allowed = new Dictionary<string, IDictionary<int, bool>>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        public IList<PermissionRecordVM> AvailablePermissions { get; set; }
        public IList<CustomerRoleVM> AvailableCustomerRoles { get; set; }

        //[permission system name] / [customer role id] / [allowed]
        public IDictionary<string, IDictionary<int, bool>> Allowed { get; set; }
    }
}
