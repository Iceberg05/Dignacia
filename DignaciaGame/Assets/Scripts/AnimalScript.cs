using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    public enum AnimalType { Cow, Chicken, Horse };
    public AnimalType animalType;
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 0.1f;
    public float thirstDecreaseRate = 0.2f;
    public float milkProductionInterval = 500f;
    public float eggProductionInterval = 500f;
    public GameObject milkPrefab;
    public GameObject eggPrefab;
    public Transform milkSpawnPoint;
    public Transform eggSpawnPoint;
    bool coroutineCowCalled;
    bool coroutineChickenCalled;

    public float hungerLevel;
    public float thirstLevel;
    [SerializeField] private bool isProducingMilk;
    [SerializeField] private bool isProducingEgg;
    [SerializeField] private float lastMilkProductionTime;
    [SerializeField] private float lastEggProductionTime;

    void Start()
    {
        hungerLevel = maxHunger;
        thirstLevel = 100f;
        lastMilkProductionTime = Time.time;
        lastEggProductionTime = Time.time;
    }

    void Update()
    {
        hungerLevel -= hungerDecreaseRate * Time.deltaTime;
        thirstLevel -= thirstDecreaseRate * Time.deltaTime;

        if (animalType == AnimalType.Cow && hungerLevel > 25 && thirstLevel > 25)
        {
            coroutineCowCalled = true;
        }
        else if (animalType == AnimalType.Cow && hungerLevel <= 25 && thirstLevel <= 25)
        {
            StopCoroutine(ProduceMilk());
        }
        if (animalType == AnimalType.Chicken && hungerLevel > 25 && thirstLevel > 25)
        {
            coroutineChickenCalled = true;
        }
        else if (animalType == AnimalType.Chicken && hungerLevel <= 25 && thirstLevel <= 25)
        {
            StopCoroutine(ProduceEgg());
        }
        if (coroutineCowCalled)
        {
            StartCoroutine(ProduceMilk());
            coroutineCowCalled = false;
        }
        else if (coroutineChickenCalled)
        {
            StartCoroutine(ProduceEgg());
            coroutineChickenCalled = false;
        }
    }

    IEnumerator ProduceMilk()
    {

        GameObject newMilk = Instantiate(milkPrefab, milkSpawnPoint.position, Quaternion.identity);
        isProducingMilk = true;
        yield return new WaitForSeconds(10f);
        StartCoroutine(ProduceMilk());

    }

    IEnumerator ProduceEgg()
    {

        GameObject newEgg = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
        isProducingEgg = true;
        yield return new WaitForSeconds(10f);
        StartCoroutine(ProduceEgg());
    }





}