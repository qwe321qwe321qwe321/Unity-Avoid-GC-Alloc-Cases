using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case4: StartCoroutine
/// </summary>
public class Case4 : ICase {
    public MonoBehaviour coroutineProxy;
    public void Process() {
        Profiler.BeginSample("SimpleEnumerator");
        IEnumerator simpleEnumerator = SimpleEnumerator(); // GC.Alloc
        Profiler.EndSample();

        Profiler.BeginSample("StartCoroutine");
        Coroutine co = coroutineProxy.StartCoroutine(simpleEnumerator); // GC.Alloc
        Profiler.EndSample();
    }

    private IEnumerator SimpleEnumerator() {
        yield return null;
    }
}

