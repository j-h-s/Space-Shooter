using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Powerup
{
    [SerializeField]
    public string name;
    public Color  color;
    public float  duration;

    [SerializeField]
    public UnityEvent startAction;
    public UnityEvent endAction;


    public void End()
    {
        if (endAction != null) {
            endAction.Invoke();
        }
    }


    public void Start()
    {
        if (startAction != null) {
            startAction.Invoke();
        }
    }
}
