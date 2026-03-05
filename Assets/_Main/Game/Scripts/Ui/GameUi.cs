using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameUi : MonoBehaviour
{
    protected IUiUser User;
    protected Dictionary<string, Action<object>> UiActions;

    public virtual void Appear(IUiUser user)
    {
        User = user;
        UiActions = new Dictionary<string, Action<object>>();
        var actions = User.GetActions();
        foreach (var action in actions)
        { UiActions.Add(action.name, action.action); }
    }

    public virtual void Disappear() { User = null; }
}