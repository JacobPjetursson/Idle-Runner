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

    private float currSpeed;
    private bool sprinting = false;
    private float totalDistanceTraveled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = walkSpeed;
        // disable the store
        Store store = FindObjectOfType<Store>();
        if (store != null)
            store.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        totalDistanceTraveled += (currSpeed * Time.fixedDeltaTime);
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

    public float GetTotalDistanceTraveled()
    {
        return totalDistanceTraveled;
    }
}
