using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIFridgeRemovalText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        tmp.text = text;
    }
}
