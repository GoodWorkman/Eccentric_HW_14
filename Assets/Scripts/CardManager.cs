using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private GameObject _cardsParent;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private GameStateManager _gameStateManager;

    private void Awake()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            _cards[i].Init(_effectsManager, this);
        }
    }

    public void ShowCards(List<Effect> effects)
    {
        _cardsParent.SetActive(true);

        for (int i = 0; i < effects.Count; i++)
        {
            _cards[i].gameObject.SetActive(true);
            
            _cards[i].Show(effects[i]);
        }
        
        _gameStateManager.SetCardsState();
    }

    public void HideCards()
    {
        foreach (Card card in _cards)
        {
            card.gameObject.SetActive(false);
        }
        
        _cardsParent.SetActive(false);
        
        _gameStateManager.SetAction();
    }
}
