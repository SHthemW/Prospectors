using System;

namespace Game.Utils.Extensions
{
    public static class StringExtensions
    {
        public static UnityEngine.Vector3 ToVector3(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));

            var splited = str.Split(',');

            if (splited.Length != 3)
                throw new ArgumentException("cannot divided to three value by ','. ", nameof(str));

            var x = float.Parse(splited[0]);
            var y = float.Parse(splited[1]);
            var z = float.Parse(splited[2]);

            return new UnityEngine.Vector3(x, y, z);
        }
    }
}
