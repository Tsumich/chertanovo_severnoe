// LightFixer.cs - повесьте ЭТОТ скрипт на Directional Light
using UnityEngine;

public class LightFixer : MonoBehaviour
{
    void Start()
    {
        Debug.Log("LightFixer on: " + gameObject.name);

        Light light = GetComponent<Light>();
        if (light != null)
        {
            light.color = Color.magenta; // ФИОЛЕТОВЫЙ
            light.intensity = 5f; // СВЕРХЯРКИЙ
            light.transform.rotation = Quaternion.Euler(20, -30, 0);

            Debug.Log($"Light DIRECTLY set to: {light.color}");
        }

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.yellow; // ЖЁЛТЫЙ туман
        RenderSettings.fogDensity = 0.3f;
    }

    void Update()
    {
        // Мерцание для проверки
        if (Time.time % 1f < 0.1f)
        {
            Light light = GetComponent<Light>();
            if (light != null)
            {
                light.intensity = Random.Range(3f, 8f);
            }
        }
    }
}