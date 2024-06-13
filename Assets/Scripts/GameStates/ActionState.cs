using UnityEngine;

public class ActionState : GameState
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private ExperienceManager _experienceManager;

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _experienceManager.UpLevel();
    }

    public override void EnterInState()
    {
        base.EnterInState();
        
        Time.timeScale = 1f;

        _joystick.Activate();
    }

    public override void ExitInState()
    {
        base.ExitInState();
        
        Time.timeScale = 0f;
        
        _joystick.Deactivate();
    }
}
