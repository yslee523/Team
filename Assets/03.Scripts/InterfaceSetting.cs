using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InterfaceSetting : MonoBehaviour
{

    public Text time;
    public Text money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.text = DateTime.Now.ToString(("HH:mm"));
        money.text = "10000";
    }
}
