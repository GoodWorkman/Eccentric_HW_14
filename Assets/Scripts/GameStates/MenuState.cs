using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameState
{
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] private GameObject _startMenuObject;

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
        
        _tapToStartButton.onClick.AddListener(gameStateManager.SetAction);
        
    }

    public override void EnterInState()
    {
        base.EnterInState();
        
        _startMenuObject.SetActive(true);
    }

    public override void ExitInState()
    {
        base.ExitInState();
        
        _startMenuObject.SetActive(false);
    }
}
