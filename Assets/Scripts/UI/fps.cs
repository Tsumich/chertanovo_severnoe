using UnityEngine;
using System.Collections.Generic;

public class LimitFPS : MonoBehaviour
{
    public int targetFPS = 60; // Укажите нужное значение FPS
    public static LimitFPS Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
      
    }

    void Start()
    {
        Application.targetFrameRate = targetFPS;
    }
}