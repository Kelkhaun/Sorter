using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class VagonWindow : MonoBehaviour
{
    [SerializeField] private Sprite _targetImage;
    [SerializeField] private Train _train;
    [SerializeField] private Vagon _vagon;

    private Image _image;

    public void OnEnable()
    {
        _vagon.VagonIsFull += ChangeSprite;
        _train.TrainIsFull += OnTrainIsFull;
    }

    private void OnDisable()
    {
        _vagon.VagonIsFull -= ChangeSprite;
        _train.TrainIsFull -= OnTrainIsFull;
    }

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void OnTrainIsFull()
    {
        StartCoroutine(TurnOffObject());
    }

    private void ChangeSprite()
    {
        _image.sprite = _targetImage;
    }
    private IEnumerator TurnOffObject()
    {
        float delay = 1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        yield return waitForSeconds;
        gameObject.SetActive(false);
    }
}
