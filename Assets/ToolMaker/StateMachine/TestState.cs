using UnityEngine;

namespace MyStateMachine
{
    public class TestState : AState
    {
        public override void EnterState()
        {
            Debug.Log("EnterState");
        }

        public override void ExitState()
        {
            Debug.Log("ExitState");
        }

        public override void UpdateState()
        {
            Debug.Log("UpdateState");
        }
    }
}