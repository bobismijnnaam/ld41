﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public const string SEE_NORTH_WALL = "SEE_NORTH_WALL";
    public const string SEE_WEST_WALL = "SEE_WEST_WALL";
    public const string SEE_EAST_WALL = "SEE_EAST_WALL";
    public const string SEE_SOUTH_WALL = "SEE_SOUTH_WALL";
    public const string SEE_PICTURE = "SEE_PICTURE";
    public const string SEE_NOTHING = "SEE_NOTHING";
    public const string DISABLE_FPS_CAMERA = "DISABLE_FPS_CAMERA";
    public const string ENABLE_PICTURE1_CAMERA = "ENABLE_PICTURE1_CAMERA";
    public const string ENTER_OBSERVE_MODE = "ENTER_OBSERVE_MODE";
    public const string LEAVE_OBSERVE_MODE = "LEAVE_OBSERVE_MODE";
    public const string KEEP_INFO_ALIVE = "KEEP_INFO_ALIVE";
    public const string KNOB_TWISTED = "KNOB_TWISTED";
    public const string BLACKING_OUT_PHASE_START = "BLACKING_OUT_PHASE_START";
    public const string PICTURE_BLACKED_OUT = "PICTURE_BLACKED_OUT";

    private Dictionary <string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init (); 
                }
            }

            return eventManager;
        }
    }

    void Init ()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener (listener);
        } 
        else
        {
            thisEvent = new UnityEvent ();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName, thisEvent);
        }
    }

    public static void StopListening (string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener (listener);
        }
    }

    public static void TriggerEvent (string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke ();
        }
    }
}
