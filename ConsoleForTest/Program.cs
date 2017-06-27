using DreamSale.Infrastructure;
using DreamSale.Core.DataSetting;

namespace ConsoleForTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EngineContext.Initialize(false);
            var settings = EngineContext.Current.Resolve<DataSettings>();
            if (settings != null && settings.IsValid())
            {
            }
        }
    }
}
