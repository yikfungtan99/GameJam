using System.Collections.Generic;
using UnityEngine;

namespace MyStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected AState currentState;

        public void Update()
        {
            UpdateState();
        }

        public void SetState(AState state)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = state;
            currentState.EnterState();
        }

        private void UpdateState()
        {
            if (currentState == null) return;

            currentState.UpdateState();
        }

        private void ExitState()
        {
            if (currentState == null) return;

            currentState.ExitState();
        }
    }
}
