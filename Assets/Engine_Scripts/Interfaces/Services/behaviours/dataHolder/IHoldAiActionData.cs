using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IHoldAiActionData : IDataHolder
    {
        FSMActionData AI { get; }

        T As<T> () where T : FSMActionData
        {
            if (AI == null)
                throw new ArgumentNullException();

            return (T)AI;
        }
    }
}
