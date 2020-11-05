using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StoreEntry : MonoBehaviour
{
    // reference to UI
    public Text priceText;
    public Text infoText;
    public Text iconText;
    public Button buyButton;
    public double price;

    public virtual void Start()
    {
        priceText.text = price.ToString("0.0") + " coins";
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        // check if player can afford this item
        double currCoins = ResourceManager.Instance.GetCoins();
        if (currCoins < price)
        {
            if (buyButton.IsInteractable())
                buyButton.interactable = false;
        }
        else if (!buyButton.IsInteractable())
        {
            buyButton.interactable = true;
        }
    }

    public abstract void Buy();
}
