using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SimpleUIAnimation : MonoBehaviour
    {
        private const int LoopAmount = 100; // Increase if loop can repeat this amount in gameplay

        [Header("Scale Related Variables")] 
        [SerializeField] private bool scaleLoopEnabled;
        [SerializeField] private Vector3 startScale;
        [SerializeField] private Vector3 endScale;
        [SerializeField] private float scaleLoopTime;

        [Header("Rotation Related Variables")] 
        [SerializeField] private bool rotationLoopEnabled;
        [SerializeField] private Vector3 startRotation;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float rotationLoopTime;

        [Header("Self Destroy")] 
        [SerializeField] private bool selfDestroyEnabled;
        [SerializeField] private float selfDestroyDelay;


        [SerializeField] private bool isStrike = false;


        private void OnEnable()
        {
            #region Self Destroy

            if (selfDestroyEnabled)
                StartCoroutine(SelfDestroyCoroutine(selfDestroyDelay));

            #endregion


            #region Scale Loop

            if (scaleLoopEnabled)
            {
                transform.localScale = startScale;
                if (isStrike)
                {
                    gameObject.transform.DOScale(endScale, scaleLoopTime / 2)
                        .SetLoops(1, LoopType.Yoyo);
                } else
                {
                    gameObject.transform.DOScale(endScale, scaleLoopTime / 2)
                        .SetLoops(LoopAmount, LoopType.Yoyo);
                }
            }

            #endregion

            #region Rotation Loop

            if (rotationLoopEnabled)
            {
                transform.rotation = Quaternion.Euler(startRotation);
                if (isStrike)
                {
                    gameObject.transform.DOScale(endScale, scaleLoopTime / 2)
                        .SetLoops(1, LoopType.Yoyo);
                }
                else
                {
                    gameObject.transform.DORotate(endRotation, rotationLoopTime / 2, RotateMode.FastBeyond360)
                        .SetLoops(LoopAmount, LoopType.Yoyo);
                }
            }

            #endregion
            
        }

        private void OnDisable()
        {
            transform.localScale = startScale;
            transform.rotation = Quaternion.Euler(startRotation);
        }

        private IEnumerator SelfDestroyCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}
