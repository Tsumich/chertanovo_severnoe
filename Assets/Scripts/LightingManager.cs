// LightingManager.cs
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [Header("Освещение")]
    public Light directionalLight; // Основной источник света
    public Color dayColor = Color.white;
    public Color eveningColor = new Color(1f, 0.8f, 0.6f); // Тёплый оранжевый
    public float eveningIntensity = 0.5f;

    void Start()
    {
        SetEveningLighting();
    }

    void SetEveningLighting()
    {
        if (directionalLight != null)
        {
            directionalLight.color = eveningColor;
            directionalLight.intensity = eveningIntensity;
            directionalLight.transform.rotation = Quaternion.Euler(20f, -30f, 0f); // Низкое солнце
        }

        RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.4f); // Синеватый ambient
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.3f, 0.3f, 0.35f); // СЕРЫЙ туман вместо фиолетового
        RenderSettings.fogDensity = 0.05f;
    }
}