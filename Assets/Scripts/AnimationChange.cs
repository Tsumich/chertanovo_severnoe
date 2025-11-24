using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    private Animation anim;
    public string playerAppear = "Armature|rotatehead";
    public string ideAnimation = "Armature|ArmatureAction";

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Когда игрок подходит
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play(playerAppear); // Запускаем анимацию поворота головы
        }
    }

    // Когда игрок уходит
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play(ideAnimation); // Возвращаем исходную анимацию
        }
    }
}