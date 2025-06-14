using System.Collections.Generic;

using UnityEngine;
// using Meta.XR.MRUtilityKit;

public class GlobalStateManager : MonoBehaviour
{
    public class _GlobalStateManager
    {
        public class _State
        {
            private static void _LogEnter(_State state) { Debug.Log($"GSM entered {state.name}State"); }

            private static void _LogExit(_State state) { Debug.Log($"GSM exited {state.name}State"); }

            public readonly string name;

            public _State(string name)
            {
                this.name = name;
                OnEnter += _LogEnter;
                OnExit += _LogExit;
            }

            public delegate void Handler(_State state);

            public event Handler OnEnter;
            public event Handler OnExit;

            internal void _Enter() { OnEnter?.Invoke(this); }

            internal void _Exit() { OnExit?.Invoke(this); }
        }

        private static void _LogInit() { Debug.Log($"GSM initializing"); }

        public static readonly _GlobalStateManager Instance;

        static _GlobalStateManager() { (Instance = new()).OnInit += _LogInit; }

        private Dictionary<string, _State> _states = new();
        private Dictionary<_State, List<_State>> _transitions = new();

        public delegate void Handler();

        public event Handler OnInit;

        public _State State { private set; get; }

        private _GlobalStateManager() { State = AddState("Initial"); }

        internal void _Init()
        {
            OnInit?.Invoke();
            State._Enter();
        }

        public _State GetState(string name)
        {
            if (_states.TryGetValue(name, out _State state)) return state;
            return null;
        }

        public _State AddState(string name)
        {
            if (_states.TryGetValue(name, out _State state)) return state;
            return _states[name] = new(name);
        }

        public _State AddTransition(_State stateFrom, _State stateTo) => AddTransition(stateFrom.name, stateTo.name);

        public _State AddTransition(_State stateFrom, string nameTo) => AddTransition(stateFrom.name, nameTo);

        public _State AddTransition(string nameFrom, string nameTo)
        {
            _State stateFrom = AddState(nameFrom);
            _State stateTo = AddState(nameTo);
            if (!_transitions.TryGetValue(stateFrom, out List<_State> states))
            {
                (_transitions[stateFrom] = new()).Add(stateTo);
                return stateTo;
            }
            if (!states.Contains(stateTo)) states.Add(stateTo);
            return stateTo;
        }

        public _State TransitionTo(_State stateTo) => TransitionTo(stateTo.name);

        public _State TransitionTo(string nameTo)
        {
            if (!_states.TryGetValue(nameTo, out _State stateTo)) return null;
            if (!_transitions.TryGetValue(State, out List<_State> states)) return null;
            if (!states.Contains(stateTo)) return null;
            State._Exit();
            State = stateTo;
            State._Enter();
            return State;
        }
    }

    public static readonly _GlobalStateManager Instance = _GlobalStateManager.Instance;
    public static readonly _GlobalStateManager._State InitialState = Instance.State;
    public static readonly _GlobalStateManager._State ReadyState = Instance.AddTransition(InitialState, "Ready");
    public static readonly _GlobalStateManager._State ListeningState = Instance.AddTransition(ReadyState, "Listening");
    public static readonly _GlobalStateManager._State PstKidState = Instance.AddTransition(ListeningState, "PstKid");
    public static readonly _GlobalStateManager._State WaitingForPlayerToTurnAroundState = Instance.AddTransition(PstKidState, "WaitingForPlayerToTurnAround");
    public static readonly _GlobalStateManager._State PossessedState = Instance.AddTransition(WaitingForPlayerToTurnAroundState, "Possessed");
    public static readonly _GlobalStateManager._State SpottedState = Instance.AddTransition(ListeningState, "Spotted");

    public static _GlobalStateManager._State State { get => Instance.State; }

    private static void _SceneLoaded() => Instance.TransitionTo(ReadyState);

    private void Start()
    {
        Instance._Init();
        _SceneLoaded();
        // MRUK.Instance.SceneLoadedEvent.AddListener(_SceneLoaded);
    }

    private void OnDisable()
    {
        // MRUK.Instance.SceneLoadedEvent.RemoveListener(_SceneLoaded);
    }
}
