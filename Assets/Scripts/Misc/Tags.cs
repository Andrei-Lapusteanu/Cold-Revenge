using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tags
{
    public const string Player = "Player";
    public const string PlayerWeapon = "PlayerWeapon";
    public const string MainCamera = "MainCamera";
    public const string RequirementIcon = "ReqIcon";
    public const string RequirementCountText = "ReqCountText";
    public const string RequirementProgressBar = "ReqProgressBar";
    public const string AmmoCabbage = "AmmoCabbage";
    public const string AmmoTomato = "AmmoTomato";
    public const string AmmoCarrot = "AmmoCarrot";
    public const string AmmoOnion = "AmmoOnion";
    public const string EnemyFridge = "EnemyFridge";
    public const string EnemyProjectileCabbage = "EnemyProjCabbage";
    public const string EnemyProjectileTomato = "EnemyProjTomato";
    public const string EnemyProjectileCarrot = "EnemyProjCarrot";
    public const string EnemyProjectileOnion = "EnemyProjOnion";
    public const string UICashRectTransform = "UICashRectTransform";
    public const string UIBuyMenu = "UIBuyMenu";

    public static bool IsNotAnyProjectile(string tag)
    {
        if (IsNotPlayerProjectile(tag) == true && IsNotEnemyProjectile(tag) == true)
            return true;
        else return false;
    }

    public static bool IsNotPlayerProjectile(string tag)
    {
        if (tag != AmmoCabbage &&
            tag != AmmoCarrot  &&
            tag != AmmoOnion   &&
            tag != AmmoTomato)
            return true;
        else return false;
    }

    public static bool IsNotEnemyProjectile(string tag)
    {
        if (tag != EnemyProjectileCabbage &&
            tag != EnemyProjectileCarrot  &&
            tag != EnemyProjectileOnion   &&
            tag != EnemyProjectileTomato)
            return true;
        else return false;
    }
}
