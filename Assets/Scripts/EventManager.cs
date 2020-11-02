using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private bool activated = false;
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            if (!activated)
                activated = true;
            FindObjectOfType<Player>().Jump();
        }
        else
        {
            activated = false;
        }

    }
}
