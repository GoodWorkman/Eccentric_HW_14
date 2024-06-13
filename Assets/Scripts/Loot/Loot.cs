using System;
using System.Collections;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private bool _isCollected;

    private Coroutine _coroutine;

    private void Awake()
    {
        _isCollected = false;
    }

    public void Collect(Collector collector)
    {
        if (_isCollected) return;

        _isCollected = true;

        _coroutine = StartCoroutine(MoveToCollector(collector));
    }

    private IEnumerator MoveToCollector(Collector collector)
    {
        Vector3 a = transform.position;
        Vector3 b = a + Vector3.up * 3f;

        for (float t = 0; t < 1f; t += Time.deltaTime * 2f)
        {
            Vector3 d = collector.transform.position;
            Vector3 c = d + Vector3.up * 3f;

            Vector3 position = Bezier.GetPoint(a, b, c, d, t);

            transform.position = position;

            yield return null;
        }

        TakeItem(collector);
    }

    public virtual void TakeItem(Collector collector)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}