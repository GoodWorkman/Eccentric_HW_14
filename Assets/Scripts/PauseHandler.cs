using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static PauseHandler Instanse;

    [SerializeField] private Joystick _joystick;

    private void Awake()
    {
        if (Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        
        _joystick.Deactivate();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;

        _joystick.Activate();
    }
}
