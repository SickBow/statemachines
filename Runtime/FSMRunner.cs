using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sickbow.StateMachines {
public class FSMRunner : MonoBehaviour
{    

    [SerializeField] FSM stateMachine;
    
    [SerializeField, HideInInspector] List<State> states;
    [SerializeField, HideInInspector] List<ConditionValue> conditionValues;
    [SerializeField] State currentState;
    [SerializeField] GameObject owner;
    
    private Dictionary<string,ConditionValue> _valuePairs = new Dictionary<string, ConditionValue>();
    private State _lastState;
    private State _nextState;

    public State GetCurrentState() => currentState;
    public State GetLastState() => (_lastState != null)? _lastState : currentState;
    public State GetNextState() => (_nextState != null)? _nextState : currentState;
    
    private void InitConditionValuePairs(){
        foreach(ConditionValue cv in conditionValues)
            _valuePairs.Add(cv.name, cv);
    }
    public void SetConditionValue(string name, bool value){
        _valuePairs[name].value = value;
    }
    public bool GetConditionValue(string name){
        return _valuePairs[name].value;
    }

    public void CloneMachine(){
        CloneStates();
        CloneConditionValues();
        CloneTransitions();
    }

    void CloneConditionValues(){
        var templateValues = stateMachine.GetConditionValues();
        List<ConditionValue> instancedValues = new List<ConditionValue>(templateValues.Count);
        
        for ( int i = 0; i < templateValues.Count; i++){
            var valueInstance = ScriptableObject.CreateInstance(templateValues[i].GetType());
            valueInstance.name = templateValues[i].name;
            instancedValues.Insert(i,(ConditionValue)valueInstance);
        }
        conditionValues = instancedValues;
    }

    void CloneTransitions(){
        var templateStates = stateMachine.GetStates();
        var templateValues = stateMachine.GetConditionValues();
        for ( int i = 0; i < templateStates.Count; i++){
            var templateTransitions = templateStates[i].GetTransitions(); 
            states[i].SetTransitions(new List<Transition>( templateStates[i].GetTransitions().Count ));   
            
            for (int j = 0; j < templateTransitions.Count; j++){    
                var refTransition = templateTransitions[j];
                var refCondition = refTransition.GetCondition();
                
                Transition copyTransition = new Transition(); // cloning transition
                Condition copyCondition = new Condition(); // cloning condition inside of cloned transition
                List<ConditionValue> copyConditionValues = new List<ConditionValue>(refCondition.GetConditionValues().Count); //cloning condition values inside of cloned transition
                foreach( ConditionValue cv in refCondition.GetConditionValues())
                    copyConditionValues.Insert(refCondition.GetConditionValues().IndexOf(cv), conditionValues.Find(x => x.name == cv.name));
                
                copyCondition.Init(refCondition.GetName(), refCondition.GetInvert(), copyConditionValues);
                var toStateType = refTransition.GetTo().GetType();
                copyTransition.Init(states.Find(x => x.GetType() == toStateType), copyCondition);

                states[i].GetTransitions().Insert(j, copyTransition);
            }
        }
    }

    void CloneStates(){
        var templateStates = stateMachine.GetStates();
        List<State> instancedStates = new List<State>(templateStates.Count);
        
        for ( int i = 0; i < templateStates.Count; i++){
            var stateInstance = templateStates[i].Clone();
            instancedStates.Insert(i,(State)stateInstance);
        }
        states = instancedStates;
    }

    void OnValidate(){
        if (states != null)
            states = stateMachine.GetStates();
    }
    void Awake(){
        CloneMachine();
        InitConditionValuePairs();
        if (states.Count > 0){
            currentState = states[0];
        }
        if (owner == null)
            owner = gameObject;
    }

    void Update()
    {
        State nextState = _nextState = NextState();

        if (nextState != currentState){
            currentState.Exit(owner);
            _lastState = currentState;
            nextState.Enter(owner);
            
            currentState = nextState;
        }

        currentState.Run(owner);
    }

    private State NextState()
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
