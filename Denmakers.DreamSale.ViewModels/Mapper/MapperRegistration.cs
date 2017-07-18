using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.ViewModels.Mapper
{
    public static class MapperRegistration
    {
        public static void RegisterMapperConfiguration()
        {
            //register mapper configurations provided by other assemblies
            var assignTypeFrom = typeof(IMapperConfiguration);
            var assignTypeTo = typeof(AdminMapperConfiguration);
            var mcInstances = from t in assignTypeFrom.Assembly.GetTypes()
                              where assignTypeTo.IsAssignableFrom(t)
                              select (IMapperConfiguration)Activator.CreateInstance(t);


            //sort
            mcInstances = mcInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            //get configurations
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            foreach (var mc in mcInstances)
                configurationActions.Add(mc.GetConfiguration());

            //register
            AutoMapperConfiguration.Init(configurationActions);
        }
    }
}
