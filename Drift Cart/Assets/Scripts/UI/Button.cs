using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public bool isPressed;
    private void Start()
    {
        SetUpButton(); 
    }
    void SetUpButton()
    {
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID=EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => OnClickDown());
        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => OnClickUp());

        trigger.triggers.Add(pointerDown);
        trigger.triggers.Add(pointerUp);
    }

    public void OnClickDown()
    {
        isPressed = true;
    }
    public void OnClickUp()
    {
        isPressed = false;
    }
}
