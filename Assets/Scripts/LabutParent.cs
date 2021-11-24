using System.Collections;
using UnityEngine;

public class LabutParent : MonoBehaviour
{
    private int _totalLabutAmount;
    private int _devrilenLabutAmount;

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
            StartCoroutine(SelfDestroyCoroutine(2f));
            //STRIKE
        }
    }

    IEnumerator SelfDestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
