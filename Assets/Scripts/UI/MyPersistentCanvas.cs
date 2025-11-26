using UnityEngine;

public class MyPersistentCanvas : MonoBehaviour
{
    public static MyPersistentCanvas Instance;

    private void Awake()
    {
        // Реализуем Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Важно! Не уничтожать при загрузке новой сцены
        }
        else
        {
            // Если уже существует другой PersistentCanvas - уничтожаем этот
            Destroy(gameObject);
        }
    }
}