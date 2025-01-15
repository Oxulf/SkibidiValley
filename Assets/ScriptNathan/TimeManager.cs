using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public int currentDay = 1; 
    public float dayDuration = 10f; 
    private float timeElapsed = 0f;
    public event Action OnNewDay; 
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= dayDuration)
        {
            timeElapsed = 0;
            currentDay++;
            Debug.Log("Jour " + currentDay);
            OnNewDay?.Invoke(); 
        }
    }
}