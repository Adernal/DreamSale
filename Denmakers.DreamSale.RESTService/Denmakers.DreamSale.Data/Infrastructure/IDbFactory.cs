using Denmakers.DreamSale.Data.Context;
using System;

namespace Denmakers.DreamSale.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        IDbContext Init();
    }
}
