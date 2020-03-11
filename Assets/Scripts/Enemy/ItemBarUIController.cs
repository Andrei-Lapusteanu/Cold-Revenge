using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBarUIController : MonoBehaviour
{
    List<GameObject> uiSlots;
    int itemBarSloutCount;
    const float slotDefScale = 1.0f;
    const float slotUpScale = 1.125f;

    // Start is called before the first frame update
    void Start()
    {
        uiSlots = new List<GameObject>();
        itemBarSloutCount = 5;

        for (int i = 1; i <= itemBarSloutCount; i++)
            uiSlots.Add(transform.Find("Slot" + i.ToString()).gameObject);
    }

    public void SelectItemBarSlot(int numKey)
    {
        for (int i = 1; i <= uiSlots.Count; i++)
            if (i == numKey)
            {
                // Scale up selected slot
                uiSlots[i - 1].GetComponent<RectTransform>().localScale = new Vector3(slotUpScale, slotUpScale, slotUpScale);

                // Change margin color
                uiSlots[i - 1].transform.Find("BorderImg").GetComponent<Image>().color = new Color(43f / 255f, 1f, 166f / 255f);

                // Change bg color
                uiSlots[i - 1].GetComponent<Image>().color = new Color32(60, 60, 60, 160);
            }
            else
            {
                // All others scale to default value
                uiSlots[i - 1].GetComponent<RectTransform>().localScale = new Vector3(slotDefScale, slotDefScale, slotDefScale);

                // Change margin color
                uiSlots[i - 1].transform.Find("BorderImg").GetComponent<Image>().color = Color.black;

                // Change bg color
                uiSlots[i - 1].GetComponent<Image>().color = new Color32(30, 30, 30, 160);
            }
    }
}
