using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FinishPanel : MonoBehaviour
{
    [SerializeField] private Train _train;
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private RectTransform _money;

    private CanvasGroup _canvasGroup;
    private float _delay = 1.4f;

    private void OnEnable()
    {
        _train.TrainIsFull += OnTrainIsFull;
    }

    private void OnDisable()
    {
        _train.TrainIsFull -= OnTrainIsFull;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnTrainIsFull()
    {
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        float targetValue = 1;
        float duration1 = 0.5f;
        float duration2 = 1f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

        yield return waitForSeconds;

        _canvasGroup.DOFade(targetValue, duration1);
        _money.DOScale(Vector3.one, duration2);

        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].Play();
        }
    }
}
