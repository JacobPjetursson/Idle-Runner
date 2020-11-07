using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    public Text boxText;
    public float textDuration;
    public float triggerChance;
    public UnityEvent effect;
    // Start is called before the first frame update
    void Start()
    {
        boxText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                //collision was from below
                effect.Invoke();
                Destroy(gameObject);
            }
            else if (Vector3.Dot(contact.normal, Vector3.right) > 0.5)
            {
                //player is leveled with the box
                gameObject.GetComponent<MovingObject>().SetFreeze(true);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.right) > 0.5)
            {
                //player is leveled with the box
                gameObject.GetComponent<MovingObject>().SetFreeze(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
            return;
        // box starts moving again
        gameObject.GetComponent<MovingObject>().SetFreeze(false);

    }

    private IEnumerator ShowBoxText(string text)
    {
        boxText.text = text;
        boxText.enabled = true;
        yield return new WaitForSeconds(textDuration);
        boxText.enabled = false;
    }

    // start of box functions
    // TODO - consider moving these to separate scripts, not sure what the best practice is here

    public void DefaultEffect()
    {
        // get 1 minute worth of coins
        double cps = ResourceManager.Instance.GetCps();
        long coins = (long)(cps * 60.0);
        ResourceManager.Instance.EarnCoins(coins);
        string earncoinsText = "You earned " + coins + " coins";
        ShowBoxText(earncoinsText);
    }

    public void HitJackpot()
    {
        double cps = ResourceManager.Instance.GetCps();
        // get 1 hour worth of coins
        long coins = (long)(cps * 3600.0);
        ResourceManager.Instance.EarnCoins(coins);
        string jackpotText = "You won the Jackpot of " + coins + " coins!!";
        ShowBoxText(jackpotText);
    }
}
