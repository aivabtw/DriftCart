using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public int CDTime = 3;
    public GameObject CD;
    public TMP_Text CDText;

    public void Start()
    {
        StartCoroutine(CountDownStart());
    }

    IEnumerator CountDownStart()
    {
        while (CDTime > 0)
        {
            CDText.text = CDTime.ToString();
            yield return new WaitForSeconds(1f);
            CDTime--;
        }
        CDText.text = "GO!";
        yield return new WaitForSeconds(1f);
        CD.SetActive(false);
    }
}
