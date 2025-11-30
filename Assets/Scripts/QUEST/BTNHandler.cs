using UnityEngine;
using UnityEngine.SceneManagement;

public class BTNHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RunGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void CloseGame()
    {
        Debug.Log("Никто не сможет покинуть саратов");
        //SceneManager.LoadScene("chert_city");
    }
}
