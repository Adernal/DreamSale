using DreamSale.Common;
using DreamSale.Core.DataSetting;
using System;

namespace DreamSale.Data.DataProvider
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = Settings.DataProvider;
            if (String.IsNullOrWhiteSpace(providerName))
                throw new DreamSaleException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new DreamSaleException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
