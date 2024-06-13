using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState _menuState;
    [SerializeField] private GameState _pauseState;
    [SerializeField] private GameState _actionState;
    [SerializeField] private GameState _winState;
    [SerializeField] private GameState _loseState;
    [SerializeField] private GameState _cardState;

    private GameState _currentState;

    public void Init()
    {
        _menuState?.Init(this);
        _pauseState?.Init(this);
        _actionState?.Init(this);
        _winState?.Init(this);
        _loseState?.Init(this);
        _cardState?.Init(this);
        
        SetMenu();
    }

    public void SetMenu()
    {
        SetGameState(_menuState);
    }
    
    public void SetPause()
    {
        SetGameState(_pauseState);
    }
    
    public void SetAction()
    {
        SetGameState(_actionState);
    }

    public void SetLose()
    {
        SetGameState(_loseState);
    }
    
    public void SetWin()
    {
        SetGameState(_winState);
    }
    
    public void SetCardsState()
    {
        SetGameState(_cardState);
    }

    private void SetGameState(GameState gameState)
    {
        if (_currentState)
        {
            _currentState.ExitInState();
        }

        _currentState = gameState;
        
        gameState.EnterInState();
    }
    
}
