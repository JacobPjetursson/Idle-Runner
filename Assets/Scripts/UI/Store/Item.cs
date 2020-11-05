public class Item : StoreEntry
{
    public float baseCps = 1.0f;
    public float priceIncreaseRate = 1.1f; // how much more expensive is the next purchase?

    private int level;
    private float multiplier = 1.0f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        level = 0;
        UpdateUI();
    }

    public override void Buy()
    {
        int number = 1;
        // number represents how many the player wants to buy (only 1 for now)
        level += number;
        ResourceManager.Instance.UseCoins(price);
        price *= number * priceIncreaseRate;
        ResourceManager.Instance.UpdateTotalCps();
        UpdateUI();
    }

    public void MultiplyCps(float multiplier)
    {
        this.multiplier += (multiplier - 1.0f); // relative increase in multiplier
        ResourceManager.Instance.UpdateTotalCps();
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateUI();
        priceText.text = price.ToString("0.0") + " coins";
        infoText.text = "You have: " + level;
        iconText.text = name;
    }

    public int GetLevel()
    {
        return level;
    }

    public double GetCps()
    {
        return baseCps * level * multiplier;
    }
}
