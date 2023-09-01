
using UnityEngine;
using System.Collections.Generic;
using System;

public class WaterCleaner : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] List<GameObject> dirtyWaterInRange = new List<GameObject>();
    [SerializeField] float range;

    SpriteRenderer rangeCircle;

    bool isTimerActive;

    [SerializeField] float remainingTime;

    [SerializeField] int startMinutes = 1;

    void Awake()
    {
        StartTimer();
        GetComponent<CircleCollider2D>().radius = range;
        rangeCircle = transform.Find("RangeCircle").GetComponent<SpriteRenderer>();
        rangeCircle.enabled = false;
    }
    void Start()
    {
        remainingTime = startMinutes * 60;
    }
    void Update()
    {
        rangeCircle.transform.localScale = new Vector3(GetComponent<CircleCollider2D>().radius + 3, GetComponent<CircleCollider2D>().radius + 3, GetComponent<CircleCollider2D>().radius + 3);

        #region TIMER


        if (isTimerActive)
        {
            remainingTime = remainingTime - Time.deltaTime;
            if (remainingTime <= 0)
            {
                CleanWater();
                Start();
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(remainingTime);

        #endregion
    }
    private void OnMouseEnter()
    {
        rangeCircle.enabled = true;
    }
    private void OnMouseExit()
    {
        rangeCircle.enabled = false;
    }
    void CleanWater()
    {
        foreach (GameObject cleanableSurface in dirtyWaterInRange)
        {
            GameObject water = Instantiate(waterPrefab, cleanableSurface.transform, false);
            water.transform.position = new Vector3(cleanableSurface.transform.position.x, cleanableSurface.transform.position.y, cleanableSurface.transform.position.z);
        }
    }
    void StartTimer()
    {
        isTimerActive = true;
    }
    void StopTimer()
    {
        isTimerActive = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DirtyWater")
        {
            dirtyWaterInRange.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "DirtyWater")
        {
            dirtyWaterInRange.Remove(col.gameObject);
        }
    }
}