using UnityEngine;
using DG.Tweening;

public class InkStain : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveY(0, 2).SetEase(Ease.InOutSine);
    }
}