# StateMachines
Provides Scriptable Objects (accessible through Create Asset Menu) and MonoBehaviors to give client a way to build state machines for game objects

## How to use
First, use the create asset menu to navigate to FSM
This will contain all the states you create (Like creating an animator controller)
Then, after creating all your custom states (Instructions on how to do this later)
Add them all to the FSM asset you've created

Then, add a component using 'Add Component' to the GameObject you wish to run your FSM and search 'FSM Runner' and add it
Drag your FSM into the FSM runner's 'State machine' exposed field

## Creating States
These will be custom c# classes that need to inherit from 'Sickbow.StateMachines.State's
Override the abstract method Run() to give the state the desired behavior
Override the virtual methods Enter() and Exit() if you want code to execute once during the entrance/exit
It is recommended to give these a "[CreateAssetMenu(fileName = "Air", menuName = "FSM/States/Air")]" attribute like this
In order to create your custom states within the create asset menu

## Creating Transitions
These are all created within the State scriptable objects you've created.
Click the plus button to add a new transition
At the bottom you specify which 'State' object the transition destination will be
in the middle, you name the condition for this transition
This condtion evaluates to true only when every 'condition value' evaluates to true at the same time
These are added by clicking the plus button

## Creating Condition Values
These are created from the create asset menu and are simply scriptable objects with a single bool 'value'. add references to all used
ConditionValues inside of the FSM ScriptableObject "conditionValues" list for everything to work properly
