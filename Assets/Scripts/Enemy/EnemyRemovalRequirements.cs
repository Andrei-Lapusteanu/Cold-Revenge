using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRemovalRequirements : MonoBehaviour
{
    private List<KeyValuePair<Ammo.Type, int>> removalReqsList;
    private List<KeyValuePair<Ammo.Type, int>> currentRemovalReqsList;
    private int baseCashValue;

    // Start is called before the first frame update
    void Start()
    {
        if (removalReqsList == null || currentRemovalReqsList == null)
            InitLists();
    }

    public void InitLists()
    {
        removalReqsList = new List<KeyValuePair<Ammo.Type, int>>();
        currentRemovalReqsList = new List<KeyValuePair<Ammo.Type, int>>();
        baseCashValue = 1;
    }

    public void GenerateFridgeRemovalReqs(int numberOfReqs)
    {
        if (removalReqsList == null || currentRemovalReqsList == null)
            InitLists();

        Ammo.Type ammoType = new Ammo.Type();

        for(int i = 0; i < numberOfReqs; i++)
        {
            if (i == 0)
                ammoType = Ammo.Type.Cabbage;
            else if (i == 1)
                ammoType = Ammo.Type.Tomato;
            else if (i == 2)
                ammoType = Ammo.Type.Carrot;
            else if (i == 3)
                ammoType = Ammo.Type.Onion;

            int rand = Random.Range(5, 10);

            removalReqsList.Add(new KeyValuePair<Ammo.Type, int>(ammoType, rand));
            currentRemovalReqsList.Add(new KeyValuePair<Ammo.Type, int>(ammoType, 0));
        }
    }

    public int GetCashValue()
    {
        int cashValue = 0;

        for(int i = 0; i < removalReqsList.Count; i++)
            cashValue += (removalReqsList[i].Value * baseCashValue);

        return cashValue;
    }

    public void IncrementRemovalRequirements(Ammo.Type ammoType)
    {
        for (int i = 0; i < currentRemovalReqsList.Count; i++)
            if (currentRemovalReqsList[i].Key == ammoType)
                currentRemovalReqsList[i] = new KeyValuePair<Ammo.Type, int>(ammoType, currentRemovalReqsList[i].Value + 1);
    }

    public void DecrementRemovalRequirements(Ammo.Type ammoType)
    {
        for (int i = 0; i < currentRemovalReqsList.Count; i++)
            if (currentRemovalReqsList[i].Key == ammoType)
                currentRemovalReqsList[i] = new KeyValuePair<Ammo.Type, int>(ammoType, currentRemovalReqsList[i].Value - 1);
    }

    public int GetCurrentRequirementCount(Ammo.Type ammoType)
    {
        foreach (KeyValuePair<Ammo.Type, int> kvp in currentRemovalReqsList)
            if (kvp.Key == ammoType)
                return kvp.Value;

        return -1;
    }

    public int GetRequirementsForAmmoType(Ammo.Type ammoType)
    {
        foreach (KeyValuePair<Ammo.Type, int> kvp in removalReqsList)
            if (kvp.Key == ammoType)
                return kvp.Value;

        return 0;
    }

    public bool CheckIfToBeRemoved()
    {
        for (int i = 0; i < removalReqsList.Count; i++)
            if (currentRemovalReqsList[i].Value != removalReqsList[i].Value)
                return false;

        // Removal is true
        return true;
    }
    public List<KeyValuePair<Ammo.Type, int>> GetRemovalReqsList()
    {
        return removalReqsList;
    }

    public bool HasAnyRequirements()
    {
        foreach (KeyValuePair<Ammo.Type, int> kvp in currentRemovalReqsList)
            if (kvp.Value > 0)
                return true;

        return false;
    }

    public Ammo.Type GetMostFulfilledRequirement()
    {
        int maxVal = int.MinValue;
        Ammo.Type ammoType = new Ammo.Type();

        for(int i = 0; i < currentRemovalReqsList.Count; i++)
            if (currentRemovalReqsList[i].Value > maxVal)
            {
                maxVal = currentRemovalReqsList[i].Value;
                ammoType = currentRemovalReqsList[i].Key;
            }
        
        return ammoType;
    }
}
