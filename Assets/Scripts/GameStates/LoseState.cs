using UnityEngine;

public class LoseState : GameState
{
    [SerializeField] private LoseWindow _loseWindow;

    public override void EnterInState()
    {
        base.EnterInState();
        
        _loseWindow.Show();
    }

    public override void ExitInState()
    {
        base.ExitInState();
        
        _loseWindow.Hide();
    }
}
