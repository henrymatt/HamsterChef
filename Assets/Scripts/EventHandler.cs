using System;
using UnityEngine;

public class EventHandler
{
    public static event Action DidInventoryChangeEvent;
    public static void CallDidInventoryChangeEvent() => DidInventoryChangeEvent?.Invoke();

    public static event Action DidFailAttemptToHideEvent;
    public static void CallDidFailAttemptToHide() => DidFailAttemptToHideEvent?.Invoke();

    public static event Action DidHideEvent;
    public static void CallDidHideEvent() => DidHideEvent?.Invoke();

}
