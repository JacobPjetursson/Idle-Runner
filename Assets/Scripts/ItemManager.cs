using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public GameObject[] items;

    void Start()
    {
        Populate();
    }

    void Update()
    {

    }

    void Populate()
    {

        foreach (GameObject item in items)
        {
            GameObject instance;
            // Create new instances of our prefab until we've created as many as we specified
            instance = (GameObject)Instantiate(item, transform);
            instance.GetComponent<Image>().color = Random.ColorHSV();
        }

    }
}