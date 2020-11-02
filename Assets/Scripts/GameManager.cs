using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject player;
    public float sprintCooldown;
    public float walkSpeed = 2f;
    public float sprintSpeed = 6f;
    public float sprintDuration = 5f;

    private double coins = 10;
    private double souls = 0;
    private float currSpeed;
    private float currCps = 0;
    private bool sprinting = false;
    private float totalDistanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = walkSpeed;
        UIManager.Instance.SetCoins(coins);
        UIManager.Instance.SetSouls(souls);
        UIManager.Instance.SetCps(currCps);

        // update coins and souls text
        StartCoroutine(UpdateUI());
        // gain coins every second
        StartCoroutine(CollectCps());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        totalDistanceTraveled += (currSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator CollectCps()
    {
        while (true)
        {
            // consider using exact time instead of this
            yield return new WaitForSeconds(1f);
            coins += currCps;
        }
    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UIManager.Instance.SetCoins(coins);
            UIManager.Instance.SetSouls(souls);
            UIManager.Instance.SetCps(currCps);
        }
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

    private void UpdateSpeed()
    {
        currSpeed = sprinting ? sprintSpeed : walkSpeed;
    }

    public void Sprint()
    {
        sprinting = true;
        UIManager.Instance.Sprint();
        UpdateSpeed();
        StartCoroutine(StopSprint());
    }

    private IEnumerator StopSprint()
    {
        yield return new WaitForSeconds(sprintDuration);
        sprinting = false;
        UpdateSpeed();
    }

    public float GetCurrentSpeed()
    {
        return currSpeed;
    }

    public void UseCoins(double coins)
    {
        this.coins -= coins;
        UIManager.Instance.SetCoins(coins);
    }

    public void UpdateCps()
    {
        UIManager.Instance.SetCps(currCps);
    }

    public double GetCoins()
    {
        return coins;
    }

    public float GetTotalDistanceTraveled()
    {
        return totalDistanceTraveled;
    }
}
