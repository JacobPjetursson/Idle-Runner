using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : StoreEntry
{
    private bool isBought;

    public abstract void Effect();
    // this is called in a routine by the 'Store' script
    public abstract bool IsUnlocked();

    public override void Buy()
    {
        ResourceManager.Instance.UseCoins(price);
        isBought = true;
        Effect();
        gameObject.SetActive(false);
        // todo: re-arrange the remaining entries
    }

    public bool IsBought()
    {
        return isBought;
    }
}
