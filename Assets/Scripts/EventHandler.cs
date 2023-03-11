using System;
using UnityEngine;

public class EventHandler
{
    public static event Action DidInventoryChangeEvent;
    public static void CallDidInventoryChangeEvent() => DidInventoryChangeEvent?.Invoke();

}
