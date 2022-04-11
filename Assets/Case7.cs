using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case7: Captured Variable Pitfall
/// </summary>
public class Case7 : ICase {
    public void Process() {
        Profiler.BeginSample("Capture Variable");
        CapturedVariable();
        Profiler.EndSample();

        Profiler.BeginSample("Capture Variable Pitfall");
        CapturedVariablePitfall(false);
        Profiler.EndSample();

        Profiler.BeginSample("Capture Variable Pitfall (Fix)");
        CapturedVariablePitfallFix(false);
        Profiler.EndSample();
    }

    private void ActionParameter(System.Action action) {
        action?.Invoke();
    }

    private void DoSomething() {
        // do something.
    }

    private void CapturedVariable() {
        int var1 = 100;
        ActionParameter(
            () => {
                int var2 = var1; // <--- captured variable.
                DoSomething();
            });
    }

    private void CapturedVariablePitfall(bool condition) {
        int var1 = 100; // GC.Alloc at this line for captured variable below.
        if (condition) { // Even if condition==false, there is still GC.Alloc
            ActionParameter(
                () => {
                    int var2 = var1; // <--- because of this captured variable.
                    DoSomething();
                });
        }
    }

    private void CapturedVariablePitfallFix(bool condition) {
        int var1 = 100; 
        if (condition) {
            int var1Copy = var1; // GC.Alloc at this line for captured variable below.
            ActionParameter(
                () => {
                    int var2 = var1Copy; // <--- captured variable.
                    DoSomething();
                });
        }
    }
}

