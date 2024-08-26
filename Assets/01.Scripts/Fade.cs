using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Fade : Singleton<Fade>
{
    private Image _fadeImage;

    private void Start()
    {
        _fadeImage = GetComponent<Image>();
        FadeIn();
    }
    private void FadeIn()
    {
        DOTween.Complete(this);
        _fadeImage.enabled = true;
        _fadeImage.DOFade(0, 0.5f).SetUpdate(true);
    }
    public void FadeOut()
    {
        DOTween.Complete(this);
        _fadeImage.DOFade(1, 0.5f).SetUpdate(true);
    }
    public void FadeOut(string sceneName)
    {
        DOTween.Complete(this);
        var sequence = DOTween.Sequence();
        sequence.Append(_fadeImage.DOFade(1, 0.5f).SetUpdate(true));
        sequence.AppendCallback(() => { SceneManager.LoadScene(sceneName); });
    }
}
