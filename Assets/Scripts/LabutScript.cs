using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabutScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 5f;
    [SerializeField] private float destroyTime = 5f;
    [SerializeField] private float scaleTime = 2f;
    bool isScaling;

    private void Start()
    {
        isScaling = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Labut") || collision.transform.CompareTag("Player"))
        {
            ScoreManager.Instance.IncreaseScore(9);
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
