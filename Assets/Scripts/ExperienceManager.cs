using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private float _currentvalue = 0f;
    [SerializeField] private float _nextLevelValue;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _scale;

    [SerializeField] private AnimationCurve _experienceCurve;
    [SerializeField] private EffectsManager _effectsManager;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private ParticleSystem _levelUpEffectParticleSystem;

    private int _level = -1;
    
    private void Awake()
    {
        _nextLevelValue = _experienceCurve.Evaluate(0);
        _scale.fillAmount = 0;
    }

    public void AddExperience(int value)
    {
        _currentvalue += value;

        if (_currentvalue >= _nextLevelValue)
        {
            UpLevel();
        }

        DisplayExperience();
    }

    private void DisplayExperience()
    {
        _scale.fillAmount = _currentvalue / _nextLevelValue;
    }

    public void UpLevel()
    {
        _level++;
        _levelText.text = _level.ToString();
        _currentvalue = 0f;
        _nextLevelValue = _experienceCurve.Evaluate(_level);
        _enemyManager.WaveLevelUp(_level);
        //PauseHandler.Instanse.PauseGame();
        StartCoroutine(ShowSkillsCardsAfterEffect());
    }

    private IEnumerator ShowSkillsCardsAfterEffect()
    {
        _levelUpEffectParticleSystem.Play();

        float delay = GetMaxParticleDuration(_levelUpEffectParticleSystem);
        
        yield return new WaitForSecondsRealtime(delay);
        
        _effectsManager.ShowCardsWithEffects();
    }

    private float GetMaxParticleDuration(ParticleSystem particleSystem)
    {
        float maximumDuration = 0f;

        ParticleSystem[] allParticles = particleSystem.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in allParticles)
        {
            float duration = particle.main.duration;
            //float lifeTime = particle.main.startLifetime.constantMax;
            float lifeTime = 0f;

            float totalDuration = duration + lifeTime;

            if (totalDuration > maximumDuration)
            {
                maximumDuration = totalDuration;
            }
        }

        return maximumDuration;
    }
}
