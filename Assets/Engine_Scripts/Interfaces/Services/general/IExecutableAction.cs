using Game.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Interfaces
{
    public interface IExecutableAction
    {
        IExecutableAction New(Dictionary<SActionDataTag, object> kwargs);
        void Execute();

        /// <summary>
        /// init an action by create instance of it, and REPLACE input by this instance.
        /// </summary>
        /// <typeparam name="TAction"></typeparam>
        /// <param name="action">action to init</param>
        /// <param name="kwargs">action arguments</param>
        /// <exception cref="ArgumentNullException"></exception>
        static void Init<TAction>(ref TAction action, Dictionary<SActionDataTag, object> kwargs) where TAction : IExecutableAction
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (kwargs == null) 
                throw new ArgumentNullException(nameof(kwargs));

            var instance = (TAction)action.New(kwargs);
            action = instance;
        }
        static void BatchInit<TAction>(Dictionary<SActionDataTag, object> kwargs, params TAction[][] actionArrs) where TAction : IExecutableAction
        {
            if (actionArrs == null) 
                throw new ArgumentNullException(nameof(actionArrs));

            if (kwargs == null)
                throw new ArgumentNullException(nameof(kwargs));

            foreach (var actionArr in actionArrs)
                for (int i = 0; i < actionArr.Length; i++)
                    Init(ref actionArr[i], kwargs);
        }
        static void BatchInit<TAction>(Dictionary<SActionDataTag, object> kwargs, in IEnumerable<TAction[]> actionArrs) where TAction : IExecutableAction
        {
            if (kwargs == null)
                throw new ArgumentNullException(nameof(kwargs));

            if (actionArrs == null)
                throw new ArgumentNullException(nameof(actionArrs));

            foreach (var actionArr in actionArrs)
                BatchInit(kwargs, actionArr);
        }
    }
}
