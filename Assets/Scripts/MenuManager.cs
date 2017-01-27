using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject[] arissaLayers;
    public GameObject[] paladinLayers;
    public GameObject[] combinedLayers;

    public GameObject ArissaMain;
    public GameObject PaladinMain;
    public GameObject CombiMain;

    public Animator menuAnim;

    void Start()
    {
        StartCoroutine(Assemble());
    }

    void Update()
    {

    }

    IEnumerator Assemble()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ArissaLayers());
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(PaladinLayers());
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(CombinedLayers());
        //yield return new WaitForSeconds(4f);
        ShowMenu();
    }

    IEnumerator ArissaLayers()
    {
        for (int i = 0; i < arissaLayers.Length; i++)
        {
            arissaLayers[i].SetActive(true);
            yield return new WaitForSeconds(0.07f);
        }
    }

    IEnumerator PaladinLayers()
    {
        ArissaMain.SetActive(false);
        for (int i = 0; i < paladinLayers.Length; i++)
        {
            paladinLayers[i].SetActive(true);
            yield return new WaitForSeconds(0.07f);
        }
    }

    IEnumerator CombinedLayers()
    {
        //PaladinMain.SetActive (false);
        for (int i = 0; i < combinedLayers.Length; i++)
        {
            combinedLayers[i].SetActive(true);
            yield return new WaitForSeconds(0.07f);
        }
    }

    void ShowMenu()
    {
        menuAnim.SetTrigger("Show");
    }
}
