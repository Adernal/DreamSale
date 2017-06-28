using System;

namespace DreamSale.Services.Tasks
{
    internal interface IRedisConnectionWrapper
    {
        bool PerformActionWithLock(string type, TimeSpan timeSpan, Action executeTaskAction);
    }
}