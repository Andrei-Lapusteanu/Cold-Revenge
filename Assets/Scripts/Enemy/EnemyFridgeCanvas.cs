using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFridgeCanvas : MonoBehaviour
{
    private GameObject targetCamera;
    private RectTransform canvasRectTransform;

    List<KeyValuePair<Ammo.Type, GameObject>> reqBarsInstancesKVP;

    private float defaultCanvasPosY;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera);
        canvasRectTransform = GetComponent<Canvas>().GetComponent<RectTransform>();

        if (reqBarsInstancesKVP == null)
            InitLists();

        defaultCanvasPosY = canvasRectTransform.position.y;
    }

    private void InitLists()
    {
        reqBarsInstancesKVP = new List<KeyValuePair<Ammo.Type, GameObject>>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraPos = targetCamera.transform.position;
        transform.LookAt(targetCameraPos);
        transform.Rotate(Vector3.up, 180);

        float distanceToCam = (targetCameraPos - transform.position).magnitude;
        float scaleFactor = canvasRectTransform.localScale.x;
        float posY = canvasRectTransform.position.y;

        if (distanceToCam > 10.0f)
        {
            scaleFactor = distanceToCam / 500.0f;
            posY = defaultCanvasPosY + ((distanceToCam - 26.0f) / 9.5f);
        }

        canvasRectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        canvasRectTransform.position = new Vector3(
            canvasRectTransform.position.x,
            posY,
            canvasRectTransform.position.z);
    }

    public void GenerateCanvas(List<KeyValuePair<Ammo.Type, int>> reqsList, PlayerAmmoTypes ammoTypes)
    {
        if (reqBarsInstancesKVP == null)
            InitLists();

        GameObject statusBar = Resources.Load("_Mine/Enemy/EnemyReqLine") as GameObject;
        statusBar.GetComponent<RectTransform>().transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        for (int i = 0; i < reqsList.Count; i++)
        {
            // Instatiate graphic
            GameObject statusBarInst = Instantiate(statusBar, gameObject.transform);

            // Set graphic scale 
            statusBarInst.GetComponent<RectTransform>().transform.localScale = new Vector3(1.0f, 1.6f, 1.0f);

            // Set graphic position 
            statusBarInst.GetComponent<RectTransform>().transform.localPosition = new Vector3(0.0f, -30 + 7.5f + (25.0f * (reqsList.Count - 1 - i)), 0.0f);

            // Set graphic icon
            statusBarInst.transform.Find("AmmoIcon").GetComponent<Image>().sprite = ammoTypes.GetAmmoSprite(reqsList[i].Key);


            // Set graphic text
            statusBarInst.transform.Find("Count (TMP)").GetComponent<TextMeshProUGUI>().text = "0/" + reqsList[i].Value.ToString();

            // Set graphic progress bar
            statusBarInst.transform.Find("ReqProgressBar/BG/FillBar").GetComponent<RectTransform>().localScale = new Vector3(0f, 1f, 0f);

            // Add to instances list
            reqBarsInstancesKVP.Add(new KeyValuePair<Ammo.Type, GameObject>(reqsList[i].Key, statusBarInst));
        }
    }

    public void UpdateReqBarForInstance(Ammo.Type ammoType, EnemyRemovalRequirements enemyRemovalReqs, bool isIncrement)
    {
        foreach (KeyValuePair<Ammo.Type, GameObject> kvp in reqBarsInstancesKVP)
            if (kvp.Key == ammoType)
            {
                // Update text
                // Increment/Decrement count (if requirement is not yet fulfilled)
                if (isIncrement == true)
                {
                    if (enemyRemovalReqs.GetCurrentRequirementCount(ammoType) != enemyRemovalReqs.GetRequirementsForAmmoType(ammoType))
                        enemyRemovalReqs.IncrementRemovalRequirements(ammoType);
                }
                else
                    enemyRemovalReqs.DecrementRemovalRequirements(ammoType);

                // Get counts
                int currentCount = enemyRemovalReqs.GetCurrentRequirementCount(ammoType);
                int finalCount = enemyRemovalReqs.GetRequirementsForAmmoType(ammoType);

                // Display on screen
                kvp.Value.transform.Find("Count (TMP)").GetComponent<TextMeshProUGUI>().text = currentCount + "/" + finalCount;

                // Update progress bar
                float progressBarScaleX = (float)enemyRemovalReqs.GetCurrentRequirementCount(ammoType) / (float)enemyRemovalReqs.GetRequirementsForAmmoType(ammoType);
                kvp.Value.transform.Find("ReqProgressBar/BG/FillBar").GetComponent<RectTransform>().localScale = new Vector3(progressBarScaleX, 1f, 0f);
            }
    }
}