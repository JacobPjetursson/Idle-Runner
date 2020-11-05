using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionUpgrade : Upgrade
{
    public float productionMultiplier;
    public int unlockLvl;
    public GameObject item;

    public override void Start()
    {
        base.Start();
        // set info text
        string multiplier = (100 * (productionMultiplier - 1)).ToString("0");
        infoText.text = "Increase production of the " + item.name + " by " + multiplier + "%";
    }

    public override void Effect()
    {
        item.GetComponent<Item>().MultiplyCps(productionMultiplier);
    }

    public override bool IsUnlocked()
    {
        return item.GetComponent<Item>().GetLevel() >= unlockLvl;
    }
}
