using System.Collections.Generic;
using System.Linq;

namespace Game.Interfaces
{
    public interface IDeepCloneable<T> where T : IDeepCloneable<T>
    {
        T DeepClone();

        static T[] BatchDeepClone(IEnumerable<T> input)
        {
            return input.Select(act => act.DeepClone()).ToArray();
        }
        static TChild[] BatchDeepClone<TChild>(IEnumerable<TChild> input) where TChild : T
        {
            return input.Select(act => (TChild)act.DeepClone()).ToArray();
        }
    }
}
