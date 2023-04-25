using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float drainTime;
    [SerializeField] Image healthBarFill;

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxHunger = 100f;
    [SerializeField] float maxthrist = 100f;

    [Range(0, 100)] public float health = 100f;
    [Range(0, 100)] public float hunger = 100f;
    [Range(0, 100)] public float thirst = 100f;

    [SerializeField] float nutrition = 25f;

    [SerializeField] float nutritionDistance = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask waterMask;
    [SerializeField] LayerMask foodMask;

    Color color;

    bool isInWater;
    bool isInFood;

    // Start is called before the first frame update
    void Start()
    {
        color = healthBarFill.color;

        health = maxHealth;
        hunger = maxHunger;
        thirst = maxthrist;

        healthSlider.maxValue = maxHealth;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxthrist;

    }

    [SerializeField] Slider healthSlider;
    [SerializeField] Slider hungerSlider;
    [SerializeField] Slider thirstSlider;
    [SerializeField] Slider interactSlider;

    [SerializeField] GameObject interactCanvas;
    public float duriation = 5f;
    float timer;

    // Update is called once per frame
    void Update()
    {
        isInWater = Physics.CheckSphere(groundCheck.position, nutritionDistance, waterMask);
        isInFood = Physics.CheckSphere(groundCheck.position, nutritionDistance, foodMask);

        hunger -= -drainTime * Time.deltaTime;
        thirst -= -drainTime * Time.deltaTime;

        thirst = Mathf.Min(thirst, maxthrist, 100);
        hunger = Mathf.Min(hunger, maxthrist, 100);

        #region If Statements

        if (hunger <= 1 || thirst <= 1)
        {
            health += drainTime * 2f * Time.deltaTime;
        }

        if (hunger <= 1 && thirst <= 1)
        {
            health += drainTime * 5f * Time.deltaTime;
        }

        if (health <= 0)
        {
            GetComponent<RagdollActive>().DoRagdoll(true);
            StartCoroutine(DeathTime());
        }

        if(isInWater)
        {
            //Display Drink
            interactCanvas.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                ShowInteract();
            } else
            {
                timer = 0;
            }
        }

        if (!isInWater)
            interactCanvas.SetActive(false);

        if (isInFood)
        {
            //Display Eat
            interactCanvas.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                ShowInteract();
            }
            else if(Input.GetKeyUp(KeyCode.E))
            {
                timer = 0;
                interactSlider.value = timer;
            }
        }

        if (!isInFood)
            interactCanvas.SetActive(false);

        if (pois == true)
        {
            health += drainTime * 3f * Time.deltaTime;
        }

        #endregion


        #region StatsBar

        healthSlider.value = health;
        hungerSlider.value = hunger;
        thirstSlider.value = thirst;

        #endregion

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,nutritionDistance);
    }

    #region Collision

    [SerializeField] GameObject destroyPartical;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Förgiftad");
            Instantiate(destroyPartical, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            StartCoroutine(Poisoned());
        }
    }

    bool pois;

    IEnumerator Poisoned()
    {
        pois = true;
        healthBarFill.color = Color.magenta;
        yield return new WaitForSeconds(5);
        healthBarFill.color = color;
        pois = false;
    }

    #endregion

    IEnumerator DeathTime()
    {
        FindObjectOfType<GameManager>().playerDead = true;
        yield return new WaitForSeconds(7);
        //Activate RigidBody
        Destroy(this.gameObject);

    }

    void ShowInteract()
    {
        timer += Time.deltaTime;

        interactSlider.value = timer;
        interactSlider.maxValue = duriation;

        if (timer >= duriation && isInWater)
        {
            thirst += nutrition;
            timer = 0;
        }
        if (timer >= duriation && isInFood)
        {
            hunger += nutrition;
            timer = 0;
        }
    }
}
