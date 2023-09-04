using System.Collections;
using System.Collections.Generic;

namespace RPGame.Stats
{
    interface IModifierProvider
    {
        IEnumerable<float> GetAddictiveModifier(Stat stat);
    }
}
