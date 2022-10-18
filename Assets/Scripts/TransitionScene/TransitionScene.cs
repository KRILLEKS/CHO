using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionScene : MonoBehaviour
{
    private Image _backgroundImage;
    private Transform[] _loadingTransforms = new Transform[5];
    void Start()
    {
        _backgroundImage = this.transform.GetChild(0).GetComponent<Image>();
        for (int i = 0; i < 5; i++)
            _loadingTransforms[i] = this.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Transform>();
    }

    public void TransitionAnimationStart() // change to private
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_backgroundImage.DOColor(new Color(1, 1, 1, 1), 0.25f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(0.25f);
        foreach (Transform tfm in _loadingTransforms)
        {
            sequence.Append(tfm.DOScale(1.0f, 0.1f).SetEase(Ease.InOutSine));
        }
        sequence.AppendInterval(0.5f);
        foreach (Transform tfm in _loadingTransforms)
        {
            sequence.Append(tfm.DOLocalJump(Vector3.zero, 50.0f, 1, 0.25f).SetEase(Ease.OutSine));
        }
    }

    public void TransitionAnimationEnd() // change to private
    {
        var sequence = DOTween.Sequence();

        foreach (Transform tfm in _loadingTransforms)
        {
            tfm.DOScale(0.0f, 0.1f).SetEase(Ease.InOutSine);
        }
        sequence.AppendInterval(0.25f);
        sequence.Append(_backgroundImage.DOColor(new Color(1, 1, 1, 0), 0.25f).SetEase(Ease.InOutSine));
    }

    public void SwapToScene(int scenenum)
    {
        StartCoroutine("TransitionAnimation", scenenum);
    }

    IEnumerator TransitionAnimation(int scenenum)
    {
        TransitionAnimationStart();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scenenum);
        yield return new WaitForSeconds(0.25f);
        TransitionAnimationEnd();
    }
}
