using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Vagon))]
public class VagonExpander : MonoBehaviour
{
    [SerializeField] private Wall[] _whiteWall;
    [SerializeField] private Wall[] _wall;
    [SerializeField] private FrontWall[] _frontWall;
    [SerializeField] private Wall _previousWall;
    [Space(20)][SerializeField] private float _xPositiontWhiteWall;
    [Space(20)][SerializeField] private float _xPosiionWall;
    [SerializeField] private float _zPositionFrontWall;
    [SerializeField] private float _xPositiontWhiteWall2;
    [Space(20)][SerializeField] private float _xPosiionWall2;
    [SerializeField] private float _xPositionFrontWall2;

    private float _duration1 = 0.2f;
    private float _duration2 = 0.3f;
    private int _leftSide = 0;
    private int _rightSide = 1;

    public void ExpandTheVagon()
    {
        float targetScaleY1 = -1.4f;
        float targetScaleY2 = 1.1f;
        float targetScaleZ1 = 1.6f;
        float targetScaleZ2 = 1.7f;
        float targetPositionY = 0.381f;
        float xPosition = 3.05f;
        float yPosition = 0.147f;
        float zPosition = 1.587f;
        Vector3 targetPositionOfLeftWall = new Vector3(xPosition, yPosition, zPosition);

        for (int i = 0; i < _whiteWall.Length; i++)
        {
            _wall[_leftSide].transform.DOScaleY(targetScaleY1, _duration2);
            _wall[_leftSide].transform.DOLocalMoveX(_xPosiionWall, _duration1);
            _wall[_leftSide].transform.DOScaleZ(targetScaleZ1, _duration2);
            _wall[_rightSide].transform.DOScaleY(targetScaleY1, _duration2);
            _wall[_rightSide].transform.DOLocalMoveX(_xPosiionWall2, _duration1);
            _wall[_rightSide].transform.DOScaleZ(targetScaleZ1, _duration2);

            _frontWall[_leftSide].transform.DOLocalMove(targetPositionOfLeftWall, _duration1);
            _frontWall[_leftSide].transform.DOScaleY(targetScaleY2, _duration2);
            _frontWall[_rightSide].transform.DOLocalMoveX(_xPositionFrontWall2, _duration1);
            _frontWall[_rightSide].transform.DOScaleY(targetScaleY2, _duration2);

            _whiteWall[_leftSide].transform.DOScaleZ(targetScaleZ2, _duration2);
            _whiteWall[_leftSide].transform.DOLocalMoveX(_xPositiontWhiteWall, _duration1);
            _whiteWall[_leftSide].transform.DOLocalMoveY(targetPositionY, _duration1);
            _whiteWall[_rightSide].transform.DOScaleZ(targetScaleZ2, _duration2);
            _whiteWall[_rightSide].transform.DOLocalMoveX(_xPositiontWhiteWall2, _duration1);
        }
    }

    public void NarrowVagon()
    {
        float duration = 0.1f;
        float targetPositionX = 3.125f;

        if (_previousWall != null)
            _previousWall.transform.DOMoveX(targetPositionX, duration);
    }
}
