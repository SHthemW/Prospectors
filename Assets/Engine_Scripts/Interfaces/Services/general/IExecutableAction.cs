using Game.Interfaces.Data;
using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IExecutableAction
    {
        IExecutableAction New(Dictionary<SActionDataTag, object> kwargs);
        void Execute();

        void ExecuteWith(Dictionary<SActionDataTag, object> kwargs)
        {
            // TODO: delete
        }

        static void Init<TAction>(ref TAction action, Dictionary<SActionDataTag, object> kwargs) where TAction : IExecutableAction
        {
            var instance = (TAction)action.New(kwargs);
            action = instance;
        }
    }
}
