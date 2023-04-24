using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{  
    public Transform player;
    [SerializeField] GameObject DeathMenu;
    [SerializeField] Camera cam;
    [SerializeField] TMP_Text[] texts;


    // Start is called before the first frame update
    void Start()
    {
        DeathMenu.SetActive(false);
        cam.enabled = false;
    }

    int i;

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            DeathMenu.SetActive(true);
            cam.enabled = true;

            for (i = 0; i < texts.Length; i++)
            {
                StartCoroutine(firstText());
                StartCoroutine(secondText());
                StartCoroutine(thirdText());
                StartCoroutine(fourthText());
                StartCoroutine(fithText());
            }             
        }

    }

    IEnumerator firstText()
    {
        yield return new WaitForSeconds(1);
        texts[1].enabled = true;
    }
    IEnumerator secondText()
    {
        texts[1].enabled = false;
        yield return new WaitForSeconds(2);
        texts[2].enabled = true;
    }
    IEnumerator thirdText()
    {
        texts[2].enabled = false;
        yield return new WaitForSeconds(2);
        texts[3].enabled = true;
    }
    IEnumerator fourthText()
    {
        texts[3].enabled = false;
        yield return new WaitForSeconds(2);
        texts[4].enabled = true;
    }
    IEnumerator fithText()
    {
        texts[4].enabled = false;
        yield return new WaitForSeconds(2);
        texts[5].enabled = true;
    }
}
