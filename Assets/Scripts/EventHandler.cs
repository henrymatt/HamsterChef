using System;
using UnityEngine;

public class EventHandler
{
    public static event Action DidInventoryChangeEvent;
    public static void CallDidInventoryChangeEvent() => DidInventoryChangeEvent?.Invoke();

    public static event Action DidPickUpIngredientEvent;
    public static void CallDidPickUpIngredientEvent() => DidPickUpIngredientEvent?.Invoke();

    public static event Action DidFailAttemptToHideEvent;
    public static void CallDidFailAttemptToHide() => DidFailAttemptToHideEvent?.Invoke();

    public static event Action DidHideEvent;
    public static void CallDidHideEvent() => DidHideEvent?.Invoke();

    public static event Action DidCreatureBeginChasingEvent;
    public static void CallDidCreatureBeginChasingEvent() => DidCreatureBeginChasingEvent?.Invoke();

    public static event Action DidCreatureStopChasingEvent;
    public static void CallDidCreatureStopChasingEvent() => DidCreatureStopChasingEvent?.Invoke();

    public static event Action<DialogueSceneSO, bool> ShouldPresentDialogueEvent;
    public static void CallShouldPresentDialogueEvent(DialogueSceneSO dialogueToDisplay, bool shouldPause) =>
        ShouldPresentDialogueEvent?.Invoke(dialogueToDisplay, shouldPause);
}
