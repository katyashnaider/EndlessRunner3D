using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IInput
    {
        bool IsTapped { get; }
        bool IsSwipedLeft { get; }
        bool IsSwipedRight { get; }
        bool IsSwipedUp { get; }
        bool IsSwipedDown { get; }
    }
}
