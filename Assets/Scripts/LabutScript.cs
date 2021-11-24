using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabutScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 5f;
    [SerializeField] private float destroyTime = 5f;
    [SerializeField] private float scaleTime = 2f;
    bool isScaling;
    public bool isHit = false;

    private void Start()
    {
        isScaling = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isHit && (collision.transform.CompareTag("Labut") || collision.transform.CompareTag("Player")))
        {
            transform.parent.GetComponent<LabutParent>().Devrildim();
            ScoreManager.Instance.IncreaseScore(10);
            isHit = true;
            StartCoroutine(StartDestroy());
        }
    }

    private void Update()
    {
        if(isScaling)
        {
            transform.localScale -= Vector3.one *  scaleSpeed;
        }
        if(transform.localScale.magnitude <= 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(scaleTime);
        isScaling = true;
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
    }
}
