using UnityEngine;

public class DustParticlesManager : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position;
        }
    }
}
