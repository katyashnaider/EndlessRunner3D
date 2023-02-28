using DG.Tweening;
using UnityEngine;

public class RotationAnimator : MonoBehaviour
{
    public static void StartAnimation(Transform objectTransform)
    {
        objectTransform.DORotate(new Vector3(objectTransform.rotation.x, 360, objectTransform.rotation.z), 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}