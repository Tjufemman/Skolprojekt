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
    [SerializeField] Camera cam2;
    private Camera cam1;
    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text text2;
    [SerializeField] TMP_Text text3;
    [SerializeField] TMP_Text text4;
    [SerializeField] TMP_Text text5;




    public bool playerDead = false;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        DeathMenu.SetActive(false);
        cam2.enabled = false;
        cam1 = FindObjectOfType<Camera>();
        anim = FindObjectOfType<Animator>(); 
        
    }

    // Update is called once per frame
    void Update()
    {       

        if(playerDead == true)
        {
            DeathMenu.SetActive(true);
            cam1.enabled = false;
            cam2.transform.position = player.transform.position;
            anim.SetBool("Dead", true);
            cam2.enabled = true;

            StartCoroutine(DisplayScreen());
                     
        }

    }
    IEnumerator DisplayScreen()
    {
        yield return new WaitForSeconds(0.5f);
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        text1.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        text2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        text3.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        text3.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        text4.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        text4.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        text5.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        text5.gameObject.SetActive(false);

    }

}
