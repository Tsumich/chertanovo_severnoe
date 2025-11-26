using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    public string playerAppear = "Armature|rotatehead";
    public string ideAnimation = "Armature|ArmatureAction";

    private Animator animator;
    private int playerNearHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false; // Добавь эту строку!
        playerNearHash = Animator.StringToHash("PlayerNear");
    }

    // Когда игрок подходит
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger");
            animator.SetBool(playerNearHash, true);
        } 
    }

    // Когда игрок уходит
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("no trigger");
            animator.SetBool(playerNearHash, false);
        }
    }
}