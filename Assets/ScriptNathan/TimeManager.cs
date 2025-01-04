using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    public int currentDay = 1; // Jour actuel
    public float dayDuration = 10f; // Dur�e d'un jour en secondes
    private float timeElapsed = 0f;

    public event Action OnNewDay; // �v�nement d�clench� chaque jour

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
            OnNewDay?.Invoke(); // Notifie les autres scripts qu'un jour passe
        }
    }
}
