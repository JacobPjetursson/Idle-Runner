using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will compute the total CpS and soul rate based on all the different factors
// It will access all the items, upgrades, etc.
public class ResourceManager : Singleton<ResourceManager>
{
    private Item[] items;
    private double coins = 10000;
    private double souls = 0;
    private double currCps;

    // Start is called before the first frame update
    void Start()
    {
        // fetch all items and upgrades
        items = Resources.FindObjectsOfTypeAll<Item>();
        UpdateTotalCps();

        // gain coins every second
        StartCoroutine(CollectCoinsRoutine());
    }

    public double GetCoins()
    {
        return coins;
    }

    public double GetSouls()
    {
        return souls;
    }

    public void UseCoins(double usedCoins)
    {
        coins -= usedCoins;
        UIManager.Instance.SetCoins(coins);
    }

    public void EarnCoins(double earnedCoins)
    {
        coins += earnedCoins;
        UIManager.Instance.SetCoins(coins);
    }

    public double GetCps()
    {
        return currCps;
    }

    public void SlayEnemy(GameObject enemy)
    {
        souls += enemy.GetComponent<Enemy>().baseSoulValue;
        UIManager.Instance.SetSouls(souls);
        Destroy(enemy);
    }

    public void CollectCoin(GameObject coin)
    {
        coins += coin.GetComponent<Coin>().baseCoinValue;
        UIManager.Instance.SetCoins(coins);
        Destroy(coin);
    }

    private IEnumerator CollectCoinsRoutine()
    {
        // todo - collect coins at every frame but make same amount of resources over time
        while (true)
        {
            // consider using exact time instead of this
            yield return new WaitForSeconds(1f);
            coins += currCps;
        }
    }

    public void UpdateTotalCps()
    {
        double cps = 0.0;
        // get cps of all items
        foreach (Item item in items)
        {
            cps += item.GetCps();
        }
        currCps = cps;
        UIManager.Instance.SetCps(currCps);
    }

}
