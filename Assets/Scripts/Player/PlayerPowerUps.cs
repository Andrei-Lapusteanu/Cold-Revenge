using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private DoubleFireRate powerDoubleFireRate;
    private FireThree powerFireThree;
    private Invulnerability powerInvulnerability;

    public DoubleFireRate PowerDoubleFireRate { get => powerDoubleFireRate; set => powerDoubleFireRate = value; }
    public FireThree PowerFireThree { get => powerFireThree; set => powerFireThree = value; }
    public Invulnerability PowerInvulnerability { get => powerInvulnerability; set => powerInvulnerability = value; }

    void Start()
    {
        PowerDoubleFireRate = gameObject.AddComponent<DoubleFireRate>();
        PowerFireThree = gameObject.AddComponent<FireThree>();
        PowerInvulnerability = gameObject.AddComponent<Invulnerability>();
    }

    void Update()
    {

    }
}

public class DoubleFireRate : MonoBehaviour, IPlayerPowerUps
{
    private const int defaultTimer = 30;
    private bool isActive = false;
    private int timer = defaultTimer;
    private int cost = 150;

    public void Activate()
    {
        if (isActive == false)
        {
            isActive = true;
            StartCoroutine(CountDown());
        }
    }

    public bool CheckIfActive()
    {
        if (timer > 0)
            return true;
        else
            return false;
    }

    public IEnumerator CountDown()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f);

            if (CheckIfActive() == true)
                timer--;
            else
            {
                Deactivate();
                break;
            }
        }
    }

    public void Deactivate()
    {
        isActive = false;
        timer = defaultTimer;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public int GetTimer()
    {
        return timer;
    }

    public int GetCost()
    {
        return cost;
    }
}

public class FireThree : MonoBehaviour, IPlayerPowerUps
{
    private const int defaultTimer = 30;
    private bool isActive = false;
    private int timer = defaultTimer;
    private int cost = 200;

    public void Activate()
    {
        if (isActive == false)
        {
            isActive = true;
            StartCoroutine(CountDown());
        }
    }

    public bool CheckIfActive()
    {
        if (timer > 0)
            return true;
        else
            return false;
    }

    public IEnumerator CountDown()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f);

            if (CheckIfActive() == true)
                timer--;
            else
            {
                Deactivate();
                break;
            }
        }
    }

    public void Deactivate()
    {
        isActive = false;
        timer = defaultTimer;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public int GetTimer()
    {
        return timer;
    }

    public int GetCost()
    {
        return cost;
    }
}

public class Invulnerability : MonoBehaviour, IPlayerPowerUps
{
    private const int defaultTimer = 60;
    private bool isActive = false;
    private int timer = defaultTimer;
    private int cost = 200;

    public void Activate()
    {
        if (isActive == false)
        {
            isActive = true;
            StartCoroutine(CountDown());
        }
    }

    public bool CheckIfActive()
    {
        if (timer > 0)
            return true;
        else
            return false;
    }

    public IEnumerator CountDown()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1f);

            if (CheckIfActive() == true)
                timer--;
            else
            {
                Deactivate();
                break;
            }
        }
    }

    public void Deactivate()
    {
        isActive = false;
        timer = defaultTimer;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public int GetTimer()
    {
        return timer;
    }

    public int GetCost()
    {
        return cost;
    }
}
