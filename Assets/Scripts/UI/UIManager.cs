using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text coinText;
    public Text soulText;
    public Text cpsText;
    public Button sprintButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetCoins(double coins)
    {
        coinText.text = "Coins: " + coins.ToString("0.0");
    }

    public void SetSouls(double souls)
    {
        soulText.text = "Souls: " + souls.ToString("0.0");
    }

    public void Sprint()
    {
        float sprintCooldown = GameManager.Instance.sprintCooldown;
        sprintButton.interactable = false;
        StartCoroutine(SprintButtonTimer(sprintCooldown));
        // todo - do something based on sprintCooldown
    }

    // todo - consider not doing this as a co-routine and just call it from GameManager
    private IEnumerator SprintButtonTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        sprintButton.interactable = true;
    }

    public void SetCps(float cps)
    {
        cpsText.text = "Coins per second: " + cps.ToString("0.0");
    }
}
