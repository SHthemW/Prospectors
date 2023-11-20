using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Util
{
    public readonly struct Fmt
    {
        public const string RED    = "<color=red>";
        public const string YELLOW = "<color=yellow>";

        public const string RESET  = "</color>";
    }

    public readonly struct Prefix
    {
        public const string DATA = "<b>[data]</b> ";
    }
}
