using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerButton : MonoBehaviour
{
    public bool isPressed;
    public float dampenPress = 0;
    public float sensentivity = 2f;
    private void Update()
    {
        if (isPressed)
        {
            dampenPress += sensentivity * Time.deltaTime;
        }
        else
        {
            dampenPress-=sensentivity * Time.deltaTime; 
        }
        dampenPress = Mathf.Clamp01(dampenPress);
    }
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
