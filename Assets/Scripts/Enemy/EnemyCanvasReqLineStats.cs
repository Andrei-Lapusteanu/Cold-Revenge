using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvasReqLineStats : MonoBehaviour
{
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public RectTransform GetRectTransf()
    {
        return rectTransform;
    }
}
