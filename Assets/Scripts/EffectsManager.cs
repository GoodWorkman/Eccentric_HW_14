using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private List<ContinuousEffect> _continuousEffectsApplied = new();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffectsApplied = new();

    [SerializeField] private List<ContinuousEffect> _continuousEffects = new();
    [SerializeField] private List<OneTimeEffect> _oneTimeEffects = new();

    private CardManager _cardManager;
    private EnemyManager _enemyManager;
    private Player _player;
    private Transform _container;

    private int _maxActiveEffects = 4;
    private int _maxCardsToShow = 3;

    public void Init(CardManager cardManager, EnemyManager enemyManager, Player player, Transform container)
    {
        _cardManager = cardManager;
        _enemyManager = enemyManager;
        _player = player;
        _container = container;
        
        InitEffectsList();
    }

    private void InitEffectsList()
    {
        // заполняем массив копиями, чтобы не менять оригиналы

        for (int i = 0; i < _continuousEffects.Count; i++)
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
            _continuousEffects[i].Init(this, _enemyManager, _player, _container);
        }

        for (int i = 0; i < _oneTimeEffects.Count; i++)
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
            _oneTimeEffects[i].Init(this, _enemyManager, _player, _container);
        }
    }

    private void Update()
    {
        foreach (ContinuousEffect effect in _continuousEffectsApplied)
        {
            effect.ProcessFrame(Time.deltaTime);
        }
    }

    public void ShowCardsWithEffects()
    {
        // Лист эффектов из которого выбираем 3 случайных
        List<Effect> allEffectsToChoose = new();

        FillChoosingEffects(_continuousEffectsApplied, _continuousEffects, allEffectsToChoose);

        FillChoosingEffects(_oneTimeEffectsApplied, _oneTimeEffects, allEffectsToChoose);

        //Кол-во карт для показа (Если effectsToShow меньше 3 карт(например все прокачаны до 10 уровня))
        int numberOfCardsToShow = Mathf.Min(allEffectsToChoose.Count, _maxCardsToShow);

        //Рандомно сортируем общий лист, создаем List effectsForCards в котором 3 случайные карты из effectToShow
        int[] randomIndexes = RandomSort(allEffectsToChoose.Count, numberOfCardsToShow);

        List<Effect> effectsForCards = new();

        for (int i = 0; i < randomIndexes.Length; i++)
        {
            int index = randomIndexes[i];

            effectsForCards.Add(allEffectsToChoose[index]);
        }

        _cardManager.ShowCards(effectsForCards);
    }

    private void FillChoosingEffects<T>(List<T> activeEffects, List<T> allEffects, List<Effect> listToFill)
        where T : Effect
    {
        // примененные эффекты
        foreach (T effect in activeEffects)
        {
            if (effect.ReachedMaxLevel() == false)
            {
                listToFill.Add(effect);
            }
        }

        // НЕ примененные эффекты
        if (activeEffects.Count < _maxActiveEffects)
        {
            listToFill.AddRange(allEffects);
        }
    }

    public void AddActivatedEffect(Effect effect)
    {
        //перемещаем активированный эффект в свой лист
        if (effect is ContinuousEffect cEffect && _continuousEffectsApplied.Contains(cEffect) == false)
        {
            _continuousEffectsApplied.Add(cEffect);
            _continuousEffects.Remove(cEffect);
        }
        else if (effect is OneTimeEffect oEffect && _oneTimeEffectsApplied.Contains(oEffect) == false)
        {
            _oneTimeEffectsApplied.Add(oEffect);
            _oneTimeEffects.Remove(oEffect);
        }
        
        //активация эффекта
        effect.Activate();
    }

    private int[] RandomSort(int lenght, int count)
    {
        int[] array = new int[lenght];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++)
        {
            int transfer = array[i];
            int randomIndex = Random.Range(0, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = transfer;
        }

        int[] resultArray = new int[count];

        for (int i = 0; i < resultArray.Length; i++)
        {
            resultArray[i] = array[i];
        }

        return resultArray;
    }


    #region LectionRealize

    // public void ShowCards()
    // {
    //     // Лист эффектов из которого выбираем 3 случайных
    //     List<Effect> allEffectsToChoose = new();
    //     
    //     // примененные Continuous эффекты
    //     foreach (ContinuousEffect effect in _continuousEffectsApplied)
    //     {
    //         if (effect.ReachedMaxLevel() == false)
    //         {
    //             allEffectsToChoose.Add(effect);
    //         }
    //     }
    //     
    //     // примененные OneTime эффекты
    //     foreach (OneTimeEffect effect in _oneTimeEffectsApplied)
    //     {
    //         if (effect.ReachedMaxLevel() == false)
    //         {
    //             allEffectsToChoose.Add(effect);
    //         }
    //     }
    //     
    //     // НЕ примененные Continuous эффекты
    //     if (_continuousEffectsApplied.Count < _maxActiveEffects)
    //     {
    //         allEffectsToChoose.AddRange(_continuousEffects);
    //     }
    //     
    //     // НЕ примененные OneTime эффекты
    //     if (_oneTimeEffectsApplied.Count < _maxActiveEffects)
    //     {
    //         allEffectsToChoose.AddRange(_oneTimeEffects);
    //     }
    //     
    //     //Кол-во карт для показа (Если effectsToShow меньше 3 карт(например все прокачаны до 10 уровня))
    //     int numberOfCardsToShow = Mathf.Min(allEffectsToChoose.Count, _maxCardsToShow);
    //     
    //     //Рандомно сортируем общий лист, создаем List effectsForCards в котором 3 случайные карты из effectToShow
    //     int[] randomIndexes = RandomSort(allEffectsToChoose.Count, numberOfCardsToShow);
    //
    //     List<Effect> effectsForCards = new();
    //
    //     for (int i = 0; i < randomIndexes.Length; i++)
    //     {
    //         int index = randomIndexes[i];
    //         
    //         effectsForCards.Add(allEffectsToChoose[index]);
    //     }
    // }

    #endregion
}