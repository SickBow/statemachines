using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sickbow.StateMachines {
public class FSMRunner : MonoBehaviour
{    

    [SerializeField] FSM stateMachine;
    
    List<State> states;
    [SerializeField] State currentState;
    [SerializeField] GameObject owner;

    void OnValidate(){
        if (states != null)
            states = stateMachine.GetStates();
    }
    void Awake(){
        if (currentState == null && states.Count > 0){
            currentState = states[0];
        }
        if (owner == null)
            owner = gameObject;
    }

    void Update()
    {
        State nextState = GetNextState();

        if (nextState != currentState){
            currentState.Exit(owner);
            nextState.Enter(owner);

            currentState = nextState;
        }

        currentState.Run(owner);
    }

    private State GetNextState()
    {
        foreach (Transition transition in currentState.GetTransitions())
        {
            var condition = transition.GetCondition();
            if (condition.GetValue())
                return transition.GetTo();  
        }

        return currentState;
    }
}
}
