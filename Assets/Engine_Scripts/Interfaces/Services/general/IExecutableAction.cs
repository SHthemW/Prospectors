using Game.Interfaces.Data;
using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IExecutableAction
    {
        void Init(Dictionary<SActionDataTag, object> kwargs);
        void Execute();

        void ExecuteWith(Dictionary<SActionDataTag, object> kwargs)
        {
            Init(kwargs);
            Execute();
        }
    }
}
