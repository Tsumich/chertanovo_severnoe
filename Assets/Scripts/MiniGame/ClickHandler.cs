using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public void OnClick()
    {
        VentilationMiniGame.Instance.RegisterClick();
    }
}