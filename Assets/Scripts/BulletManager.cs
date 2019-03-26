using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
    private void Update()
    {
		UnityEngine.Profiling.Profiler.BeginSample("BulletManager Script Update Profiling");
        if (transform.localPosition.y <= -100f)
        {
            Destroy(gameObject);
        }
		UnityEngine.Profiling.Profiler.EndSample();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

