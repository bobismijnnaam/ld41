﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public static string SEE_NORTH_WALL = "SEE_NORTH_WALL";
    public static string SEE_WEST_WALL = "SEE_WEST_WALL";
    public static string SEE_EAST_WALL = "SEE_EAST_WALL";
    public static string SEE_SOUTH_WALL = "SEE_SOUTH_WALL";
    public static string SEE_PICTURE = "SEE_PICTURE";
    public static string SEE_NOTHING = "SEE_NOTHING";
    public static string DISABLE_FPS_CAMERA = "DISABLE_FPS_CAMERA";
    public static string ENABLE_PICTURE1_CAMERA = "ENABLE_PICTURE1_CAMERA";
    public static string ENTER_OBSERVE_MODE = "ENTER_OBSERVE_MODE";
    public static string LEAVE_OBSERVE_MODE = "LEAVE_OBSERVE_MODE";
    public static string KEEP_INFO_ALIVE = "KEEP_INFO_ALIVE";

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
