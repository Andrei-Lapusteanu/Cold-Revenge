using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const int MAX_HP = 100;
    private int HP;

    private const float DEFAULT_MOVE_SPEED = 4.0f;
    private const float SPRINT_VEL_BOOST = 1.5f;

    private AudioSource[] audioSources;
    private AudioSource audioFlashlightClick;
    private AudioSource audioPlayerHit;

    private Camera mainCamera;
    private RaycastHit ray;
    private GameObject playerWeapon;
    private PlayerAmmoTypes ammoTypes;
    private GameObject selectedAmmoType;
    private ItemBarUIController itemBarCtrl;
    private GameObject uiBuyMenu;
    private GameObject uiIntro;
    private GameObject uiEndMenu;
    private PlayerPowerUps powerUps;
    private GameObject powerUpsDisplay;
    private int cash = 0;

    bool isGameFrozen;

    private Light flashLight;
    private WeaponController playerWeaponCtrl;
    private CharacterController characterCtrl;
    private Vector3 vectorMove;
    private int killCount = 0;

    public float moveSpeed = DEFAULT_MOVE_SPEED;
    public float jumpHeight = 0.14f;
    public float gravityForce = 3.25f;

    public bool sceneJustLoaded = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
        playerWeapon = GameObject.FindGameObjectWithTag(Tags.PlayerWeapon);
        ammoTypes = GameObject.Find("AmmoController").GetComponent<PlayerAmmoTypes>();
        selectedAmmoType = ammoTypes.GetAmmoType(Ammo.Type.Cabbage);
        itemBarCtrl = GameObject.Find("ItemBarUI").GetComponent<ItemBarUIController>();
        uiBuyMenu = GameObject.FindGameObjectWithTag(Tags.UIBuyMenu);
        uiBuyMenu.SetActive(false);
        uiIntro = GameObject.Find("UIIntro");
        uiIntro.SetActive(true);
        uiEndMenu = GameObject.Find("UIEndGame");
        uiEndMenu.SetActive(false);

        powerUps = GetComponent<PlayerPowerUps>();
        powerUpsDisplay = GameObject.Find("PowerUpsDisplay");
        isGameFrozen = false;

        HP = MAX_HP;

        characterCtrl = GetComponent<CharacterController>();
        playerWeaponCtrl = GameObject.FindGameObjectWithTag(Tags.PlayerWeapon).GetComponent<WeaponController>();

        FridgeRemovalCount.ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneJustLoaded == true)
        {
            Time.timeScale = 0.0f;
            GetComponent<CameraRotation>().Disable();
            itemBarCtrl.SelectItemBarSlot(1);
        }
        else
        {
            if (HP > 0)
            {
                // Change currelty selected ammo type
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    selectedAmmoType = ammoTypes.GetAmmoType(Ammo.Type.Cabbage);
                    itemBarCtrl.SelectItemBarSlot(1);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    selectedAmmoType = ammoTypes.GetAmmoType(Ammo.Type.Tomato);
                    itemBarCtrl.SelectItemBarSlot(2);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    selectedAmmoType = ammoTypes.GetAmmoType(Ammo.Type.Carrot);
                    itemBarCtrl.SelectItemBarSlot(3);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    selectedAmmoType = ammoTypes.GetAmmoType(Ammo.Type.Onion);
                    itemBarCtrl.SelectItemBarSlot(4);
                }

                if (Input.GetKeyDown(KeyCode.Tab))
                    PauseGame();

                if (characterCtrl.isGrounded)
                {
                    // Player ground movement
                    vectorMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    vectorMove = transform.TransformDirection(vectorMove);

                    // Jump
                    if (Input.GetButton("Jump"))
                        vectorMove.y += Mathf.Sqrt(jumpHeight * 2f * gravityForce);

                    // Sprint
                    if (Input.GetKey(KeyCode.LeftShift))
                        moveSpeed = DEFAULT_MOVE_SPEED + SPRINT_VEL_BOOST;
                    else moveSpeed = DEFAULT_MOVE_SPEED;
                }

                CheckForActivePowerUps();

                // Reload
                //if (Input.GetKeyDown(KeyCode.R) && weaponCtrl.GetCurrentState() != WeaponController.WEAPON_STATE_FIRE)
                //    if (weaponCtrl.IsClipFull() == false)
                //        weaponCtrl.UpdateState(WeaponController.WEAPON_STATE_REALOAD);

                // Flashlight
                if (Input.GetKeyDown(KeyCode.F))
                    ToggleFlashlight();

                // Apply gravity
                vectorMove.y -= gravityForce * Time.deltaTime;

                // Apply movement
                characterCtrl.Move(vectorMove * Time.deltaTime * moveSpeed);
            }
            else
            {
                Time.timeScale = 0.0f;
                GetComponent<CameraRotation>().Disable();
                uiEndMenu.SetActive(true);
                int score = FridgeRemovalCount.GetCount();
                GameObject.Find("UIFridgeRemovalCount").GetComponent<UIFridgeRemovalText>().SetText(score.ToString());
            }
        }
    }

    public void StartGame()
    {
        sceneJustLoaded = false;
        Time.timeScale = 1.0f;
        uiIntro.SetActive(false);
        GetComponent<CameraRotation>().Enable();
    }

    public Vector3 GetRaycastDirection()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out ray, Mathf.Infinity))
        {
            Debug.DrawRay((playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f)), (ray.point - (playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f))), Color.red, 1.0f, false);
            return (ray.point - (playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f))).normalized;
        }
        else
        {
            var secondaryRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(playerWeapon.transform.position, (secondaryRay.GetPoint(10000) - playerWeapon.transform.position), Color.green, 1.0f, false);
            return secondaryRay.GetPoint(10000).normalized;
        }
    }

    public GameObject GetSelectedAmmoType()
    {
        return selectedAmmoType;
    }

    public GameObject GetAmmoType(Ammo.Type ammoType)
    {
        return ammoTypes.GetAmmoType(ammoType);
    }

    public void PauseGame()
    {
        if (isGameFrozen == false)
        {
            Time.timeScale = 0.0f;
            GetComponent<CameraRotation>().Disable();
            uiBuyMenu.SetActive(true);
            isGameFrozen = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            GetComponent<CameraRotation>().Enable();
            var go = GameObject.FindGameObjectWithTag(Tags.UIBuyMenu);//.SetActive(false);
            uiBuyMenu.SetActive(false);
            isGameFrozen = false;
        }
    }

    private void CheckForActivePowerUps()
    {
        if (powerUps.PowerDoubleFireRate.IsActive() == true)
            playerWeaponCtrl.FireRateSetMultiplier(2);
        else
            playerWeaponCtrl.FireRateReset();
    }

    public void TakeDamage(int damageAmount)
    {
        if(powerUps.PowerInvulnerability.IsActive() == false)
        {
            HP -= damageAmount;
        }

        //audioPlayerHit.Play();
    }

    public void Heal(int healAmount)
    {
        if (HP + healAmount > 100)
            HP = 100;
        else
            HP += healAmount;
    }

    public void IncreaseAmmo(int ammoAmount)
    {
        //weaponCtrl.IncreaseAmmo(ammoAmount);
    }

    public void ToggleFlashlight()
    {
        if (flashLight.enabled == true)
            flashLight.enabled = false;

        else
            flashLight.enabled = true;

        audioFlashlightClick.Play();
    }

    public int GetHealthPoints()
    {
        return HP;
    }

    public int GetInitHP()
    {
        return MAX_HP;
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int cashValue)
    {
        this.cash += cashValue;
    }

    public void SubtractCash(int cashAmount)
    {
        this.cash -= cashAmount;
    }

    public bool IsFullHealth()
    {
        if (HP == MAX_HP)
            return true;
        else
            return false;
    }

    //public int GetBulletCount()
    //{
    //    return weaponCtrl.GetBulletCount();
    //}

    //public int GetClipCount()
    //{
    //    return weaponCtrl.GetClipCount();
    //}

    //public int GetLeftoverBulletsCount()
    //{
    //    return weaponCtrl.GetLeftoverBulletsCount();
    //}


    public void IncrementKillCount()
    {
        killCount++;
    }

    public int GetKillCount()
    {
        return killCount;
    }
}
