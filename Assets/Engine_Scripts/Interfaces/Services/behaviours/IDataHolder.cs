using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IDataHolder<TData>
    {
        TData Data { get; }
    }
}
