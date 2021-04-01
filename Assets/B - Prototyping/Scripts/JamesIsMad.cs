using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class JamesIsMad : SerializedMonoBehaviour
{
    public Action<float, bool, GameObject> e;

    // Start is called before the first frame update
    void Start()
    {
        e.Invoke(5, true, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventFired(float _thing, bool _b, GameObject _go)
    {
        Debug.Log($"{_thing} : {_b} : {_go.name}");
    }
}
