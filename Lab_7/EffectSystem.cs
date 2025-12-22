using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    internal delegate void EffectHandler(CharacterContext ctx);
    internal class EffectSystem
    {
        public event EffectHandler OnEffect = null!;
        public void Run(CharacterContext ctx)
        {
            OnEffect?.Invoke(ctx);
        }
    }
}
