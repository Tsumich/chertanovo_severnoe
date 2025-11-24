using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 9f;
    public float mouseSensitivity = 200f;
    
    public Transform playerCamera; // Ссылка на камеру (перетащи её в это поле в инспекторе)
    private CharacterController controller;
    private float xRotation = 0f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Курсор скрывается и блокируется в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
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
}