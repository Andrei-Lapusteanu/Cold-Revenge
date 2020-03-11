using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoTypes : MonoBehaviour
{
    private const string AMMO_PREFAB_PATH = "_Mine/Ammo/";
    private const string TOMATO_PREAFAB_NAME = "AmmoTomatoPrefab";
    private const string CABBAGE_PREFAB_NAME = "AmmoCabbagePrefab";
    private const string CARROT_PREFAB_NAME = "AmmoCarrotPrefab";

    private const string AMMO_IMG_PATH = "_Mine/Images/";
    private const string TOMATO_IMG_NAME = "ammoTomatoImg";
    private const string CABBAGE_IMG_NAME = "ammoCabbageImg";
    private const string CARROT_IMG_NAME = "ammoCarrotImg";

    private const string ENEMY_PROJ_PREFAB_PATH = "_Mine/Enemy/Projectile/";
    private const string ENEMY_PROJ_TOMATO_PREAFAB_NAME = "EnemyProjToamato";
    private const string ENEMY_PROJ_CABBAGE_PREFAB_NAME = "EnemyProjCabbage";
    private const string ENEMY_PROJ_CARROT_PREFAB_NAME = "EnemyProjCarrot";

    private const string PARTICLE_SPLASH_PREFAB_PATH = "_Mine/Particles/";
    private const string PARTICLE_SPLASH_TOMATO_PREFAB_NAME = "SplashTomato";
    private const string PARTICLE_SPLASH_CABBAGE_PREFAB_NAME = "SplashCabbage";
    private const string PARTICLE_SPLASH_CARROT_PREFAB_NAME = "SplashCarrot";

    private const string UI_PWR_UPS_PATH = "_Mine/UI/";
    private const string UI_PWR_UPS_DOUBLE_FIRE_RATE = "PowerUpDoubleFireRate";
    private const string UI_PWR_UPS_FIRE_THREE = "PowerUpThreeAtOnce";
    private const string UI_PWR_UPS_INVULNERABILITY = "PowerUpInvulnerability";

    private List<KeyValuePair<string, GameObject>> AmmoTypesPrefabsKVP;
    private List<KeyValuePair<string, Sprite>> AmmoTypesImgKVP;
    private List<KeyValuePair<string, GameObject>> EnemyProjPrefabsKVP;
    private List<KeyValuePair<string, GameObject>> ParticleSplashTypesKVP;
    private List<KeyValuePair<string, GameObject>> UIPowerUpsKVP;


    // Start is called before the first frame update
    void Start()
    {
        AmmoTypesPrefabsKVP = new List<KeyValuePair<string, GameObject>>();
        AmmoTypesImgKVP = new List<KeyValuePair<string, Sprite>>();
        EnemyProjPrefabsKVP = new List<KeyValuePair<string, GameObject>>();
        ParticleSplashTypesKVP = new List<KeyValuePair<string, GameObject>>();
        UIPowerUpsKVP = new List<KeyValuePair<string, GameObject>>();

        LoadAmmoPrefabs();
    }

    public void LoadAmmoPrefabs()
    {
        AmmoTypesPrefabsKVP = new List<KeyValuePair<string, GameObject>>();
        AmmoTypesImgKVP = new List<KeyValuePair<string, Sprite>>();
        EnemyProjPrefabsKVP = new List<KeyValuePair<string, GameObject>>();
        ParticleSplashTypesKVP = new List<KeyValuePair<string, GameObject>>();
        UIPowerUpsKVP = new List<KeyValuePair<string, GameObject>>();

        GameObject CabbagePrefab = Resources.Load(AMMO_PREFAB_PATH + CABBAGE_PREFAB_NAME) as GameObject;
        GameObject TomatoPrefab = Resources.Load(AMMO_PREFAB_PATH + TOMATO_PREAFAB_NAME) as GameObject;
        GameObject CarrotPrefab = Resources.Load(AMMO_PREFAB_PATH + CARROT_PREFAB_NAME) as GameObject;

        Sprite CabbageSprite = Resources.Load<Sprite>(AMMO_IMG_PATH + CABBAGE_IMG_NAME);
        Sprite TomatoSprite = Resources.Load<Sprite>(AMMO_IMG_PATH + TOMATO_IMG_NAME);
        Sprite CarrotSprite = Resources.Load<Sprite>(AMMO_IMG_PATH + CARROT_IMG_NAME);

        GameObject EnemyCabbageProj = Resources.Load(ENEMY_PROJ_PREFAB_PATH + ENEMY_PROJ_CABBAGE_PREFAB_NAME) as GameObject;
        GameObject EnemyTomatoProj = Resources.Load(ENEMY_PROJ_PREFAB_PATH + ENEMY_PROJ_TOMATO_PREAFAB_NAME) as GameObject;
        GameObject EnemyCarrotProj = Resources.Load(ENEMY_PROJ_PREFAB_PATH + ENEMY_PROJ_CARROT_PREFAB_NAME) as GameObject;

        GameObject particleSplashCabbage = Resources.Load(PARTICLE_SPLASH_PREFAB_PATH + PARTICLE_SPLASH_CABBAGE_PREFAB_NAME) as GameObject;
        GameObject particleSplashTomato = Resources.Load(PARTICLE_SPLASH_PREFAB_PATH + PARTICLE_SPLASH_TOMATO_PREFAB_NAME) as GameObject;
        GameObject particleSplashCarrot = Resources.Load(PARTICLE_SPLASH_PREFAB_PATH + PARTICLE_SPLASH_CARROT_PREFAB_NAME) as GameObject;

        GameObject powerUpDoubleFireRate = Resources.Load(UI_PWR_UPS_PATH + UI_PWR_UPS_DOUBLE_FIRE_RATE) as GameObject;
        GameObject powerUpThreeAtOnce = Resources.Load(UI_PWR_UPS_PATH + UI_PWR_UPS_FIRE_THREE) as GameObject;
        GameObject powerUpInvulnerability = Resources.Load(UI_PWR_UPS_PATH + UI_PWR_UPS_INVULNERABILITY) as GameObject;

        AmmoTypesPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Cabbage", CabbagePrefab));
        AmmoTypesPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Tomato", TomatoPrefab));
        AmmoTypesPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Carrot", CarrotPrefab));

        AmmoTypesImgKVP.Add(new KeyValuePair<string, Sprite>("Cabbage", CabbageSprite));
        AmmoTypesImgKVP.Add(new KeyValuePair<string, Sprite>("Tomato", TomatoSprite));
        AmmoTypesImgKVP.Add(new KeyValuePair<string, Sprite>("Carrot", CarrotSprite));

        EnemyProjPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Cabbage", EnemyCabbageProj));
        EnemyProjPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Tomato", EnemyTomatoProj));
        EnemyProjPrefabsKVP.Add(new KeyValuePair<string, GameObject>("Carrot", EnemyCarrotProj));

        ParticleSplashTypesKVP.Add(new KeyValuePair<string, GameObject>("Cabbage", particleSplashCabbage));
        ParticleSplashTypesKVP.Add(new KeyValuePair<string, GameObject>("Tomato", particleSplashTomato));
        ParticleSplashTypesKVP.Add(new KeyValuePair<string, GameObject>("Carrot", particleSplashCarrot));

        UIPowerUpsKVP.Add(new KeyValuePair<string, GameObject>("DoubleFireRate", powerUpDoubleFireRate));
        UIPowerUpsKVP.Add(new KeyValuePair<string, GameObject>("ThreeAtOnce", powerUpThreeAtOnce));
        UIPowerUpsKVP.Add(new KeyValuePair<string, GameObject>("Invulnerability", powerUpInvulnerability));
    }

    public GameObject GetAmmoType(Ammo.Type ammoType)
    {
        if (AmmoTypesPrefabsKVP == null)
            LoadAmmoPrefabs();

        foreach (KeyValuePair<string, GameObject> kvp in AmmoTypesPrefabsKVP)
            if (kvp.Key == ammoType.ToString())
                return kvp.Value;

        return new GameObject();
    }

    public GameObject GetEnemyProjectileType(Ammo.Type ammoType)
    {
        if (EnemyProjPrefabsKVP == null)
            LoadAmmoPrefabs();

        foreach (KeyValuePair<string, GameObject> kvp in EnemyProjPrefabsKVP)
            if (kvp.Key == ammoType.ToString())
                return kvp.Value;

        return new GameObject();
    }

    public GameObject GetSplashParticleType(Ammo.Type ammoType)
    {
        if (ParticleSplashTypesKVP == null)
            LoadAmmoPrefabs();

        foreach (KeyValuePair<string, GameObject> kvp in ParticleSplashTypesKVP)
            if (kvp.Key == ammoType.ToString())
                return kvp.Value;

        return new GameObject();
    }

    public GameObject GetUIPowerUp(Ammo.PowerUp powerUp)
    {
        if (UIPowerUpsKVP == null)
            LoadAmmoPrefabs();

        foreach (KeyValuePair<string, GameObject> kvp in UIPowerUpsKVP)
            if (kvp.Key == powerUp.ToString())
                return kvp.Value;

        return new GameObject();
    }

    public Sprite GetAmmoSprite(Ammo.Type ammoType)
    {
        if (AmmoTypesImgKVP == null)
            LoadAmmoPrefabs();

        foreach (KeyValuePair<string, Sprite> kvp in AmmoTypesImgKVP)
            if (kvp.Key == ammoType.ToString())
                return kvp.Value;

        return null;
    }
}
