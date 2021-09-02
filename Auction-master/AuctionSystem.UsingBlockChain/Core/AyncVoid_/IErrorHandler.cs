using System;

namespace AuctionSystem.UsingBlockChain.Core
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
