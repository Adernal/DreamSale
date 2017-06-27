using AutoMapper;
using System;
using System.Collections.Generic;

namespace DreamSale.Infrastructure.Mapper
{
    public static class AutoMapperConfiguration
    {
        #region Fields
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;
        #endregion

        #region Properties
        public static IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }

        public static MapperConfiguration MapperConfiguration
        {
            get
            {
                return _mapperConfiguration;
            }
        }
        #endregion

        /// <summary>
        /// Initialize mapper
        /// </summary>
        /// <param name="configurationActions">Configuration actions</param>
        public static void Init(List<Action<IMapperConfigurationExpression>> configurationActions)
        {
            if (configurationActions == null)
                throw new ArgumentNullException("configurationActions");

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (var ca in configurationActions)
                    ca(cfg);
            });

            _mapper = _mapperConfiguration.CreateMapper();
        }
    }
}
