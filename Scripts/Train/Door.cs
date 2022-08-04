using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isLeft;
    [SerializeField] private bool _isRight;

    public bool IsLeft => _isLeft;
    public bool IsRight => _isRight;
}
