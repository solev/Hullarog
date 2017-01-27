using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChaliceFill : MonoBehaviour
{

    public Image chaliceMask;

    RectTransform maskRect;
    float targetH = 30, targetW = 60;

    // Use this for initialization
    void Start()
    {
        maskRect = chaliceMask.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentBlood = GameManager.Instance.blood;
        float bloodPerc = currentBlood / (float)GameManager.MaxBlood;


        float wdt = Mathf.Clamp(targetW * bloodPerc, 48, targetW), heig = targetH * bloodPerc;        

        maskRect.sizeDelta = new Vector2(wdt, heig);
    }

}
