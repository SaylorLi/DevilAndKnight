using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;

public class EventDispatcher
{
    public delegate void EventHandler(IEvent evt);
    private class EventOwner
    {
        public string evtID;
        public Type evtType;
        public EventHandler evtHandler;
        public void Trigger(IEvent evt)
        {
            if (evtHandler != null)
            {
                evtHandler(evt);
            }
        }
    }
    private Dictionary<string, EventOwner> dicOwner= new Dictionary<string, EventOwner>();
    private List<IEvent> listEvent= new List<IEvent>();

    public bool RegisterEvent(string ID, Type eventType)
    {
        if (eventType != null && !eventType.IsSubclassOf(typeof(IEvent)))
        {
            //LogHunter.Error(string.Format("EventDispatcher.registerEvent Failed, {0} is not a subclass of IEvent", eventType));
            return false;
        }

        EventOwner ew;
        if (dicOwner.TryGetValue(ID, out ew))
        {
            //LogHunter.Error(string.Format("EventDispatcher.registerEvent Failed,{0} is allways registered", ID));
            return false;
        }

        ew = new EventOwner();
        ew.evtID = ID;
        ew.evtType = eventType;
        dicOwner.Add(ID, ew);
        //LogHunter.Infor(string.Format("EventDispatcher.registerEvent Successful,ID={0},type={1}", ID, eventType));
        return true;
    }

    public void UnregisterEvent(string ID)
    {
        if (dicOwner.ContainsKey(ID) && dicOwner.Remove(ID))
        {
            //LogHunter.Infor(string.Format("EventDispatcher.unregisterEvent Successful,ID={0}", ID));
        }
    }
    public bool BindHandler(string ID, EventHandler handler)
    {
        EventOwner ew;
        if (dicOwner.TryGetValue(ID, out ew))
        {
            ew.evtHandler += handler;
            //LogHunter.Infor(string.Format("EventDispatcher.bindHandler key={0},handler={1}", ID, handler));
            return true;
        }
        //LogHunter.Infor(string.Format("EventDispatcher.bindHandler Failed--bind a not regisiter event,key={0}", ID));
        return false;
    }
    public void UnbindHandler(string ID, EventHandler handler)
    {
        EventOwner ew;
        if (dicOwner.TryGetValue(ID, out ew))
        {
            ew.evtHandler -= handler;
            //LogHunter.Infor(string.Format("EventDispatcher.unbindHandler key={0},handler={1}", ID, handler));
        }
    }

    public void Post(IEvent evt)
    {
        listEvent.Add(evt);
    }

    void Trigger(IEvent evt)
    {
        EventOwner ew;
        if (dicOwner.TryGetValue(evt.ID, out ew))
        {
            ew.Trigger(evt);
        }
        else
        {
            //LogHunter.Error(string.Format("EventDispatcher.postAndDispatch Failed,Try to post a not register event,id={0}", evt.ID));
        }
    }

    public void Dispatch()
    {
        int num = listEvent.Count;
        if (num > 0)
        {
            for (int i = num - 1; i >= 0; i--)
            {
                Trigger(listEvent[i]);
            }
            listEvent.Clear();
        }
      
        //foreach (IEvent evt in cur_events_)
        //{
        //    trigger(evt);
        //}
        //listEvent.Clear();
    }

}
