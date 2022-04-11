using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case6: Delegate(Lambda expression / delegate operator / anonymous function)
/// </summary>
public class Case6 : ICase {
    private System.Action m_ActionDoSomething;
    public void Process() {
        Profiler.BeginSample("delegate implicit operator");
        ActionParameter(DoSomething);
        Profiler.EndSample();
        Profiler.BeginSample("delegate explicit operator");
        ActionParameter((System.Action)DoSomething);
        Profiler.EndSample();

        Profiler.BeginSample("Lambda Experssion");
        ActionParameter(
            () => {
                DoSomething();
            });
        Profiler.EndSample();

        Profiler.BeginSample("delegate operator");
        ActionParameter(delegate { DoSomething(); });
        Profiler.EndSample();

        if (m_ActionDoSomething == null) {
            m_ActionDoSomething = DoSomething;
        }
        Profiler.BeginSample("Cached Action (Fix)");
        ActionParameter(m_ActionDoSomething);
        Profiler.EndSample();

        //Profiler.BeginSample("Capture Variable");
        //CapturedVariable();
        //Profiler.EndSample();

        //Profiler.BeginSample("Capture Variable Pitfall");
        //CapturedVariablePitfall(false);
        //Profiler.EndSample();

        //Profiler.BeginSample("Capture Variable Pitfall (Fix)");
        //CapturedVariablePitfallFix(false);
        //Profiler.EndSample();
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
        if (condition) {
            int var1 = 100; // GC.Alloc at this line for captured variable below.
            ActionParameter(
                () => {
                    int var2 = var1; // <--- captured variable.
                    DoSomething();
                });
        }
    }
}

