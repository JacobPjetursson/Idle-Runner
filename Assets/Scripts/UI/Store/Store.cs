using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : Singleton<Store>
{

    // Start is called before the first frame update
    void Start()
    {
        // the store window starts by being closed
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItemsTab()
    {

    }

    public void SelectUpgradesTab()
    {

    }
}
