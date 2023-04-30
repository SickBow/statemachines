using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sickbow.StateMachines {
[CreateAssetMenu(fileName = "FSM", menuName = "FSM/FSM")]
public class FSM : ScriptableObject
{
    [SerializeField] List<State> states;
    [SerializeField] List<ConditionValue> conditionValues;
    public List<State> GetStates() => states;
    public List<ConditionValue> GetConditionValues() => conditionValues;
}
}
