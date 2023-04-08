using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sickbow.StateMachines {
[CreateAssetMenu(fileName = "Condition Value", menuName = "FSM/Condition Value")]
public class ConditionValue : ScriptableObject
{
    public bool value;
}
}