using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class MoneyImage : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Image _image;
    private float _duration = 1f;
    private float _targetPosition = 4.2f;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        float _delay1 = 0.7f;
        float _delay2 = 0.5f;
        float duration = 0.5f;
        float targetValue = 0;
        WaitForSeconds waitForSeconds1 = new WaitForSeconds(_delay1);
        WaitForSeconds waitForSeconds2 = new WaitForSeconds(_delay2);

        _rectTransform.DOAnchorPosY(_targetPosition, _duration);
        yield return waitForSeconds1;
        _image.DOFade(targetValue, duration);
        yield return waitForSeconds2;
        Destroy(gameObject);
    }

}
