using System.Collections;
using System.Collections.Generic;

namespace MyStateMachine
{
    public abstract class AState
    {
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
    }
}
