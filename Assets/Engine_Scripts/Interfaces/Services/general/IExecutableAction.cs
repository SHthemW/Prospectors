using Game.Interfaces.Data;
using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IExecutableAction
    {
        void Execute();
        Dictionary<SActionDataTag, object> RuntimeKwargs { get; set; }

        void ExecuteWith(Dictionary<SActionDataTag, object> kwargs)
        {
            RuntimeKwargs = kwargs ?? throw new ArgumentNullException(nameof(kwargs));
            Execute();
        }
    }
}
