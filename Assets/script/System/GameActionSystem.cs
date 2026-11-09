using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public enum ReactionTiming
{
    PRE,
    POST,
}

public abstract class GameAction
{
    public List<GameAction> PreAction { get; private set; } = new();
    public List<GameAction> Action { get; private set; } = new();
    public List<GameAction> PostAction { get; private set; } = new();
}
public class GameActionSystem : Singleton<GameActionSystem>
{
    private List<GameAction> _actions = new();
    public bool isPerforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();
    public void Perform(GameAction action, System.Action OnPerformFinished = null)
    {
        if (isPerforming) return;
        isPerforming = true;
        StartCoroutine(Flow(action, () => 
        {
            isPerforming=false;
            OnPerformFinished?.Invoke();
        }));
    }

    private IEnumerator Flow(GameAction action, Action OnFlowFinish = null)
    {
        // perform pre_action
        _actions = action.PreAction;
        PerformSubcribers(action, preSubs);
        yield return PerformReaction();

        // perform the main action
        _actions = action.Action;
        yield return PerformPerformer(action);
        yield return PerformReaction();

        // perform the post action
        _actions = action.PostAction;
        PerformSubcribers(action, postSubs);
        yield return PerformReaction();

        OnFlowFinish?.Invoke();
    }

    private IEnumerator PerformReaction()
    {
        foreach (var reaction in _actions) { 
            yield return Flow(reaction);
        }
    }

    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (performers.ContainsKey(type))
        {
            yield return performers[type](action);
        }
    }

    private void PerformSubcribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action);
            }
        }
    }

    public void AddReaction(GameAction gameAction)
    {
        _actions?.Add(gameAction);
    }

    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T: GameAction
    {
        Type type = typeof(T);
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        if (performers.ContainsKey(type)) performers[type]= wrappedPerformer;
        else performers.Add(type, wrappedPerformer);
    }

    public static void DetachPerformer<T>() where T : GameAction
    {
        Type type= typeof(T);
        if (performers.ContainsKey(type)) performers.Remove(type);
    }

    public static void SubcribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE? preSubs:postSubs;

        void wrappedReaction(GameAction action)=> reaction((T)action);
        if (subs.ContainsKey(typeof(T)))
        {
            subs[typeof(T)].Add(wrappedReaction);
        }
        else
        {
            subs.Add(typeof(T), new());
            subs[typeof(T)].Add(wrappedReaction);
        }
    }

    public static void UnsubcribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;

        if (subs.ContainsKey(typeof(T)))
        {
            void wrappedReaction(GameAction action) => reaction((T)action);
            subs[typeof (T)].Remove(wrappedReaction);
        }
    }
}
