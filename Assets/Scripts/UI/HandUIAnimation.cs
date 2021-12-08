using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class HandUIAnimation : MonoBehaviour
    {
        public bool enableLoop;
        public float animationDistance;
        public float loopTime;

        private void OnEnable()
        {
            if(!enableLoop) return;
            var transform1 = transform;
            var position = transform1.localPosition;
            transform.DOLocalMove(new Vector3(position.x,position.y-animationDistance,position.z),loopTime).SetLoops(-1,LoopType.Restart);
        }
    }
}
