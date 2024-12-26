using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button enterButton;

    private bool moveLeft;
    private bool moveRight;
    private bool jump;
    private bool enter;

    private void Start()
    {
        leftButton.gameObject.AddComponent<EventTrigger>();
        rightButton.gameObject.AddComponent<EventTrigger>();
        jumpButton.gameObject.AddComponent<EventTrigger>();
        enterButton.gameObject.AddComponent<EventTrigger>();

        AddEventTrigger(leftButton, EventTriggerType.PointerDown, () => moveLeft = true);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, () => moveLeft = false);

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, () => moveRight = true);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, () => moveRight = false);

        AddEventTrigger(jumpButton, EventTriggerType.PointerDown, () => jump = true);
        AddEventTrigger(jumpButton, EventTriggerType.PointerUp, () => jump = false);

        AddEventTrigger(enterButton, EventTriggerType.PointerDown, () => enter = true);
        AddEventTrigger(enterButton, EventTriggerType.PointerUp, () => enter = false);
    }

    private void AddEventTrigger(Button button, EventTriggerType eventType, System.Action action)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }

    public bool IsMovingLeft() => moveLeft;
    public bool IsMovingRight() => moveRight;
    public bool IsJumping() => jump;
    public bool IsEntering() => enter;
}