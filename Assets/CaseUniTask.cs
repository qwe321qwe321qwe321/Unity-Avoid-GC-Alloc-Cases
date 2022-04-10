using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using Cysharp.Threading.Tasks;

/// <summary>
/// Case3: StartCoroutine
/// </summary>
public class CaseUniTask : ICase {
    public MonoBehaviour coroutineProxy;
    public void Process() {
        Profiler.BeginSample("StartCoroutine");
        coroutineProxy.StartCoroutine(Coroutine());
        Profiler.EndSample();

        Profiler.BeginSample("TaskAsync().Forget()");
        TaskAsync().Forget();
        Profiler.EndSample();
    }

    private IEnumerator Coroutine() {
        yield return null;
    }

    private async UniTaskVoid TaskAsync() {
        await UniTask.Yield();
    }
}

