using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float moveSpeed = 9f;
    public float mouseSensitivity = 200f;
    
    public Transform playerCamera; // Ссылка на камеру (перетащи её в это поле в инспекторе)
    private CharacterController controller;
    private float xRotation = 0f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        // === ДВИЖЕНИЕ МЫШЬЮ (ВЗГЛЯД) ===
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем угол, чтобы не смотреть слишком высоко/низко
        
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        // === ДВИЖЕНИЕ КЛАВИШАМИ ===
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void LockPlayerMovement()
    {  
        this.enabled = false; // ВЫКЛЮЧАЕМ скрипт движения
    }

    public void UnlockPlayerMovement()
    {
        this.enabled = true; // ВКЛЮЧАЕМ скрипт движения
    }
}