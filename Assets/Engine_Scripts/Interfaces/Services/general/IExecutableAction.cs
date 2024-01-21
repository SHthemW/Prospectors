using Game.Interfaces.Data;
using System;
using System.Collections.Generic;

namespace Game.Interfaces
{
    public interface IExecutableAction : IDeepCloneable<IExecutableAction>
    {
        Dictionary<SActionDataTag, object> RuntimeKwargs { get; set; }
        void Execute();

        /// <summary>
        /// init an action by create DEEPCLONE of it, and REPLACE input by this clone.
        /// </summary>
        /// <typeparam name="TAction"></typeparam>
        /// <param name="action">action to init</param>
        /// <param name="kwargs">action arguments</param>
        /// <exception cref="ArgumentNullException"></exception>
        static void Init<TAction>(Dictionary<SActionDataTag, object> kwargs, ref TAction action) where TAction : IExecutableAction
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (kwargs == null) 
                throw new ArgumentNullException(nameof(kwargs));

            var instance = (TAction)action.DeepClone();
            instance.RuntimeKwargs = kwargs;
            action = instance;
        }

        /// <summary>
        /// init some action array by create DEEPCLONE of it, and REPLACE them by this clone.
        /// </summary>
        /// <typeparam name="TAction"></typeparam>
        /// <param name="kwargs"></param>
        /// <param name="actionArrs"></param>
        /// <exception cref="ArgumentNullException"></exception>
        static void BatchInit<TAction>(Dictionary<SActionDataTag, object> kwargs, params TAction[][] actionArrs) where TAction : IExecutableAction
        {
            if (actionArrs == null) 
                throw new ArgumentNullException(nameof(actionArrs));

            if (kwargs == null)
                throw new ArgumentNullException(nameof(kwargs));

            foreach (var actionArr in actionArrs)
                for (int i = 0; i < actionArr.Length; i++)
                    Init(kwargs, ref actionArr[i]);
        }
    }
}
