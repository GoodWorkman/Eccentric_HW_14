using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _iconContinuousBG;
    [SerializeField] private Image _iconOneTimeBG;
    [SerializeField] private Image _iconImage;

    [SerializeField] private TextMeshProUGUI _effectNameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private Button _button;

    private Effect _effect;
    private CardManager _cardManager;
    private EffectsManager _effectsManager;

    public void Init(EffectsManager effectsManager, CardManager cardManager)
    {
        _effectsManager = effectsManager;
        _cardManager = cardManager;

        _button.onClick.AddListener(Click);
    }

    public void Show(Effect effect)
    {
        _effect = effect;
        _effectNameText.text = effect.Name;
        _descriptionText.text = effect.Description;
        _levelText.text = effect.Level.ToString();
        _iconImage.sprite = effect.Sprite;

        _iconContinuousBG.gameObject.SetActive(effect is ContinuousEffect);
        _iconOneTimeBG.gameObject.SetActive(effect is OneTimeEffect);
    }

    private void Click()
    {
        _effectsManager.AddActivatedEffect(_effect);
        _cardManager.HideCards();
        //PauseHandler.Instanse.ContinueGame();
        Debug.Log("click " + gameObject.name);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Click);
    }
}