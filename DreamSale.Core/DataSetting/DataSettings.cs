using System;
using System.Collections.Generic;

namespace DreamSale.Core.DataSetting
{
    /// <summary>
    /// Data settings (connection string information)
    /// </summary>
    public partial class DataSettings
    {
        #region Properties
        public string DataProvider { get; set; }

        public string DataConnectionString { get; set; }

        public IDictionary<string, string> RawDataSettings { get; private set; }
        #endregion

        #region Constructor
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// A value indicating whether entered information is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !String.IsNullOrEmpty(this.DataProvider) && !String.IsNullOrEmpty(this.DataConnectionString);
        }
        #endregion
    }
}
