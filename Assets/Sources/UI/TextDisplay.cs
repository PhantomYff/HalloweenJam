using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextDisplay : MonoBehaviour, ITextDisplay
{
    [SerializeField] private TMP_Text _display;
    [SerializeField] private float _fadeOutSeconds;

    public void Display(string text)
    {
        _display.text = text;
        _display.DOFade(1f, _fadeOutSeconds).From();
    }
}
