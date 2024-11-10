using System;

public static class ReactiveReduxUtils
{
    // 1. Action class to represent Redux-style actions
    public class Action
    {
        public string Type { get; }
        public object Payload { get; }

        public Action(string type, params object[] payload)
        {
            Type = type;
            Payload = payload.Length == 1 ? payload[0] : payload;
        }
    }

    // 2. Reducer class to handle state updates
    public delegate TState Reducer<TState>(TState state, Action action);

    // 3. Selectors (utility functions for accessing state)
    public static class Selectors
    {
        public static Func<TState, TReturn> CreateSelector<TState, TReturn>(Func<TState, TReturn> selector)
        {
            return selector;
        }
    }
}
