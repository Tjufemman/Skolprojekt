using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float drainTime;

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

    bool isInWater;
    bool isInFood;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        isInWater = Physics.CheckSphere(groundCheck.position, nutritionDistance, waterMask);
        isInFood = Physics.CheckSphere(groundCheck.position, nutritionDistance, foodMask);

        hunger -= -drainTime * Time.deltaTime;
        thirst -= -drainTime * Time.deltaTime;

        thirst = Mathf.Min(thirst, maxthrist, 100);
        hunger = Mathf.Min(hunger, maxthrist, 100);

        if (hunger <= 1 || thirst <= 1)
        {
            health += drainTime * 2f * Time.deltaTime;
        }

        if(health <= 0)
        {
            Debug.Log("Death");
        }

        if(isInWater)
        {
            //Display Drink
            if(Input.GetKeyDown(KeyCode.E))
            {
                thirst += nutrition;
            }
        }

        if (isInFood)
        {
            //Display Drink
            if (Input.GetKeyDown(KeyCode.E))
            {
                hunger += nutrition;
            }
        }

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
}
