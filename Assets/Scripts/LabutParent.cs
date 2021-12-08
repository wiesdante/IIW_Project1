using System.Collections;
using UnityEngine;

public class LabutParent : MonoBehaviour
{
    private int _totalLabutAmount;
    private int _devrilenLabutAmount;
    [SerializeField] private float delay = 2f;

    private void Start()
    {
        _totalLabutAmount = transform.childCount;
    }

    public void Devrildim()
    {
        _devrilenLabutAmount++;
        if (_devrilenLabutAmount == _totalLabutAmount)
        {
            print("strike");
            StartCoroutine(SelfDestroyCoroutine(delay));
        }
    }

    IEnumerator SelfDestroyCoroutine(float delay)
    {
        PlayerUIHandler.Instance.SetStrikeImg(true);
        yield return new WaitForSeconds(delay);
        PlayerUIHandler.Instance.SetStrikeImg(false);
        Destroy(this.gameObject);
    }
}
