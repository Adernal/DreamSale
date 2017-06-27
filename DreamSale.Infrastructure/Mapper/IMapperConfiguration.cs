using System;
using AutoMapper;

namespace DreamSale.Infrastructure.Mapper
{
    /// <summary>
    /// Mapper configuration registrar interface
    /// </summary>
    public interface IMapperConfiguration
    {
        Action<IMapperConfigurationExpression> GetConfiguration();

        int Order { get; }
    }
}
