using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerPowerUps
{
    void Activate();
    void Deactivate();
    IEnumerator CountDown();
    bool CheckIfActive();
    bool IsActive();
    int GetTimer();
    int GetCost();
}
