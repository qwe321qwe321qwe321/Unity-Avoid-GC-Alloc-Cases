using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case5: WaitForSeconds/WaitForSecondsRealtime/WaitForEndOfFrame/WaitForFixedUpdate
/// </summary>
public class Case5 : ICase {
    public MonoBehaviour coroutineProxy;
    public void Process() {
        IEnumerator delayWaitForSeconds = DelayWaitForSeconds(1f);
        Profiler.BeginSample("DelayWaitForSeconds");
        RunCompletely(delayWaitForSeconds);
        Profiler.EndSample();

        IEnumerator delayWaitForSecondsFix = DelayWaitForSecondsFix(1f);
        Profiler.BeginSample("DelayWaitForSecondsFix");
        RunCompletely(delayWaitForSecondsFix);
        Profiler.EndSample();

        IEnumerator delayWaitForSecondsRealtime = DelayWaitForSecondsRealtime(1f);
        Profiler.BeginSample("DelayWaitForSecondsRealtime");
        RunCompletely(delayWaitForSecondsRealtime);
        Profiler.EndSample();

        IEnumerator delayWaitForSecondsRealtimeFix = DelayWaitForSecondsRealtimeFix(1f);
        Profiler.BeginSample("DelayWaitForSecondsRealtimeFix");
        RunCompletely(delayWaitForSecondsRealtimeFix);
        Profiler.EndSample();

        IEnumerator delayWaitForEndOfFrame = DelayWaitForEndOfFrame();
        Profiler.BeginSample("DelayWaitForEndOfFrame");
        RunCompletely(delayWaitForEndOfFrame);
        Profiler.EndSample();

        IEnumerator delayWaitForEndOfFrameFix = DelayWaitForEndOfFrameFix();
        Profiler.BeginSample("DelayWaitForEndOfFrameFix");
        RunCompletely(delayWaitForEndOfFrameFix);
        Profiler.EndSample();

        IEnumerator delayWaitForFixedUpdate = DelayWaitForFixedUpdate();
        Profiler.BeginSample("DelayWaitForFixedUpdate");
        RunCompletely(delayWaitForFixedUpdate);
        Profiler.EndSample();

        IEnumerator delayWaitForFixedUpdateFix = DelayWaitForFixedUpdateFix();
        Profiler.BeginSample("DelayWaitForFixedUpdateFix");
        RunCompletely(delayWaitForFixedUpdateFix);
        Profiler.EndSample();
    }

    private void RunCompletely(IEnumerator enumerator) {
        while (enumerator.MoveNext()) ;
    }

    private IEnumerator DelayWaitForSeconds(float seconds) {
        yield return new WaitForSeconds(seconds); // GC.Alloc
    }

    private IEnumerator DelayWaitForSecondsFix(float seconds) {
        float time = 0;
        while (time < seconds) {
            yield return null; // free.
            time += Time.deltaTime;
        }
    }

    private IEnumerator DelayWaitForSecondsRealtime(float seconds) {
        yield return new WaitForSecondsRealtime(seconds);
    }

    private IEnumerator DelayWaitForSecondsRealtimeFix(float seconds) {
        float time = 0;
        while (time < seconds) {
            yield return null; // free.
            time += Time.unscaledTime;
        }
    }

    private IEnumerator DelayWaitForEndOfFrame() {
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator DelayWaitForFixedUpdate() {
        yield return new WaitForFixedUpdate();
    }

    private IEnumerator DelayWaitForEndOfFrameFix() {
        yield return WaitForInstances.WaitForEndOfFrame;
    }

    private IEnumerator DelayWaitForFixedUpdateFix() {
        yield return WaitForInstances.WaitForFixedUpdate;
    }

    public static class WaitForInstances {
        public static WaitForFixedUpdate WaitForFixedUpdate { get; } = new WaitForFixedUpdate();
        public static WaitForEndOfFrame WaitForEndOfFrame { get; } = new WaitForEndOfFrame();
    }
}

