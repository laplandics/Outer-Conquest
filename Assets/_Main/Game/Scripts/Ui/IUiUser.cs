using System;

public interface IUiUser
{
    public (string name, Action<object> action)[] GetActions();
    public void ShowUi();
    public void HideUi();
}