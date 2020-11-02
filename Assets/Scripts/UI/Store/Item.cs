using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : StoreEntry
{
    public float baseCps = 1.0f;
    public float initialPrice = 10f;
    public float priceIncreaseRate = 1.2f; // how much more expensive is the next purchase?

    private int numberOfOwned;
    private float cps; // can be increased by upgrades

    // Start is called before the first frame update
    void Start()
    {
        cps = baseCps;
        numberOfOwned = 0;
        priceText.text = price.ToString("0.0") + " coins";
        infoText.text = "You have: " + numberOfOwned;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player can afford this item
        double currCoins = GameManager.Instance.GetCoins();
        if (currCoins < price)
        {
            if (buyButton.IsInteractable())
                buyButton.interactable = false;
        } else if (!buyButton.IsInteractable()) {
            buyButton.interactable = true;
        }
    }

    public override void Buy()
    {
        int number = 1;
        // number represents how many the player wants to buy (only 1 for now)
        numberOfOwned += number;
        // Notify GameManager of a change in CpS and coins
        GameManager.Instance.UpdateCps();
        GameManager.Instance.UseCoins(price);

        price *= number * priceIncreaseRate;

        priceText.text = price.ToString("0.0") + " coins";
        infoText.text = "You have: " + numberOfOwned;
    }

    public void MultiplyCps(float multiplier)
    {
        cps *= multiplier;
    }

    private void UpdateUI()
    {

    }
}
