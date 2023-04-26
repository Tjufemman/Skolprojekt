using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject DeathMenu;
    [SerializeField] Camera cam2;
    private Camera cam1;

    [SerializeField] GameObject[] texts;

    public bool playerDead = false;
    private Animator[] anim;

    [SerializeField] GameObject[] disableThese;


    // Start is called before the first frame update
    void Start()
    {
        DeathMenu.SetActive(false);
        cam2.enabled = false;
        cam1 = FindObjectOfType<Camera>();
        anim = FindObjectsOfType<Animator>();

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerDead == true)
        {
            Destroy(GameObject.FindWithTag("Enemy"));
            StartCoroutine(DisplayScreen());

            cam1.enabled = false;
            cam2.enabled = true;
            
            AudioListener listen = FindObjectOfType<AudioListener>();
            listen.enabled = true;

            for (int i = 0; i < anim.Length; i++)
            {
                anim[1].SetBool("Dead", true);
            }

            for (int i = 0; i <= disableThese.Length; i++)
            {
                disableThese[i].SetActive(false);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                PausingGame();
            }
        }

    }

    int currentIndex = 0;

    IEnumerator DisplayScreen()
    {

        yield return new WaitForSeconds(4);
        DeathMenu.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        texts[currentIndex].SetActive(true);

        if (Input.anyKeyDown)
        {
            texts[currentIndex].SetActive(false);
            texts[currentIndex + 1].SetActive(true);

            currentIndex++;
        }

    }

    public static bool GameIsPaused = false;

    public GameObject pausMenu;

    public void PausingGame()
    {
        Cursor.lockState = CursorLockMode.None;

        pausMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        pausMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
