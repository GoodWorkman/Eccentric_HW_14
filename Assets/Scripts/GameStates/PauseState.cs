using UnityEngine;
using UnityEngine.UI;

public class PauseState : GameState
{
    [SerializeField] private Button _enterToPauseButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private GameObject _pauseMenuObject;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        
        _enterToPauseButton.onClick.AddListener(gameStateManager.SetPause);
        _continueButton.onClick.AddListener(gameStateManager.SetAction);
        _goToMenuButton.onClick.AddListener(gameStateManager.SetMenu);
        
    }

    public override void EnterInState()
    {
        base.EnterInState();
        
        _pauseMenuObject.SetActive(true);
    }

    public override void ExitInState()
    {
        base.ExitInState();
        
        _pauseMenuObject.SetActive(false);
    }
}
