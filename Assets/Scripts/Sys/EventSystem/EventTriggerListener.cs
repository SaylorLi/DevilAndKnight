using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;

    public VoidDelegate onDragBeginHandler;
    public VoidDelegate onDragHandler;
    public VoidDelegate onDragEndHandler;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null)
            listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onDragBeginHandler != null)
            onDragBeginHandler(gameObject);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (onDragHandler != null)
            onDragHandler(gameObject);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (onDragEndHandler != null)
            onDragEndHandler(gameObject);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
            onDown(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
            onEnter(gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
            onExit(gameObject);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
            onUp(gameObject);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
            onSelect(gameObject);
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null)
            onUpdateSelect(gameObject);
    }
}