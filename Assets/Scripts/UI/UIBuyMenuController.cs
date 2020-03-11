using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuyMenuController : MonoBehaviour
{
    PlayerPowerUps powerUps;
    private PlayerAmmoTypes ammoTypes;
    private PlayerController playerCtrl;
    private GameObject powerUpsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        powerUps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerUps>();
        ammoTypes = GameObject.Find("AmmoController").GetComponent<PlayerAmmoTypes>();
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUpsDisplay = GameObject.Find("PowerUpsDisplay");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateDoubleFireRate()
    {
        Instantiate(ammoTypes.GetUIPowerUp(Ammo.PowerUp.DoubleFireRate), powerUpsDisplay.transform);
        powerUps.PowerDoubleFireRate.Activate();
        playerCtrl.SubtractCash(powerUps.PowerDoubleFireRate.GetCost());
    }

    public void ActivateThreeAtOnce()
    {
        Instantiate(ammoTypes.GetUIPowerUp(Ammo.PowerUp.ThreeAtOnce), powerUpsDisplay.transform);
        powerUps.PowerFireThree.Activate();
        playerCtrl.SubtractCash(powerUps.PowerFireThree.GetCost());
    }

    public void ActivateInvulnerability()
    {
        Instantiate(ammoTypes.GetUIPowerUp(Ammo.PowerUp.Invulnerability), powerUpsDisplay.transform);
        powerUps.PowerInvulnerability.Activate();
        playerCtrl.SubtractCash(powerUps.PowerInvulnerability.GetCost());
    }
}
