using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : StoreEntry
{
    private bool isBought;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Effect();

    public override void Buy()
    {
        GameManager.Instance.UseCoins(price);
        Effect();
    }
}
