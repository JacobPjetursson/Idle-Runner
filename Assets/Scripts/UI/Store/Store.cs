using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject itemTab;
    public GameObject upgradeTab;

    private List<Upgrade> upgrades;
    // Start is called before the first frame update
    void Start()
    {
        upgrades = new List<Upgrade>(GetComponentsInChildren<Upgrade>(true));
        // sort upgrades based on price
        upgrades.Sort(delegate (Upgrade a, Upgrade b)
        {
            return a.price.CompareTo(b.price);
        });
        // move upgrades to enforce those with lowest price first
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].transform.SetSiblingIndex(i);
        }
        // all upgrades are initially disabled
        foreach (Upgrade u in upgrades)
        {
            u.gameObject.SetActive(false);
        }
        
    }

    private void OnEnable()
    {
        // check if upgrades are unlocked
        StartCoroutine(UnlockUpgradesRoutine());
    }

    private IEnumerator UnlockUpgradesRoutine()
    {
        // todo - consider if it is better to check every frame, or alternatively for every time player makes an action that might unlock new upgrade
        // this approach is simpler since we are not spreading the code out to all the various places where an upgrade might be unlocked, but it is very inefficient
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            foreach (Upgrade u in upgrades)
            {
                if (!u.IsBought() && !u.gameObject.activeInHierarchy && u.IsUnlocked())
                    u.gameObject.SetActive(true);
            }
        }
    }
}
