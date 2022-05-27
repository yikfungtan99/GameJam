using System.Collections;
using System.Collections.Generic;

namespace MyStateMachine
{
    public class StateMachineTester : StateMachine
    {
        private TestState testState;

        private void Start()
        {
            this.GetComponent<ComponentLogger>().Log("Say Hi");
            testState = new TestState();
            SetState(testState);
        }
    }
}