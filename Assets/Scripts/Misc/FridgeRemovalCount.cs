using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FridgeRemovalCount
{
    private static int count;

    public static void UpdateCount()
    {
        count++;
    }

    public  static int GetCount()
    {
        return count;
    }

    public static void ResetScore()
    {
        count = 0;
    }
}
