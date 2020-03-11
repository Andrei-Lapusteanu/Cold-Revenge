using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICashCanvasController : MonoBehaviour
{
    RectTransform rectTransform;
    Vector3 defaultAnimScale;
    Vector3 maxAnimScale;
    float animSpeed;
    bool isAnimPlaying;
    bool isExpanding;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultAnimScale = rectTransform.transform.localScale;
        maxAnimScale = new Vector3(1.3f, 1.3f, 1.3f);
        animSpeed = 0.02f;
        isAnimPlaying = true;
        isExpanding = true;
    }

    public void PlayCashCanvasAnimation()
    {
        // Play animation
        StartCoroutine(PlayCashCanvasAnimationAsync());
    }

    public IEnumerator PlayCashCanvasAnimationAsync()
    {
        isAnimPlaying = true;
        isExpanding = true;

        for(; ; )
        {
            if(isAnimPlaying == true)
            {
                if(isExpanding == true)
                {
                    if (rectTransform.transform.localScale.x < maxAnimScale.x)
                    {
                        rectTransform.transform.localScale += new Vector3(animSpeed, animSpeed, animSpeed);
                    }
                    else
                        isExpanding = false;
                }
                else
                {
                    if (rectTransform.transform.localScale.x > defaultAnimScale.x)
                    {
                        rectTransform.transform.localScale -= new Vector3(animSpeed, animSpeed, animSpeed);
                    }
                    else
                        isAnimPlaying = false;
                }
            }
            else break;

            yield return new WaitForEndOfFrame();
        }
    }
}
