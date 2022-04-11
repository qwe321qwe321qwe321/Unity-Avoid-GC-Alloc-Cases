using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case10: params array as method paramters
/// </summary>
public class Case10 : ICase {
    public void Process() {
        Profiler.BeginSample("params int[] as method paramters");
        Max(0, 1);
        Max(0, 1, 4564);
        Max(0, 1, 4564, 12);
        Profiler.EndSample();

        Profiler.BeginSample("overload method paramters");
        MaxFix(0, 1);
        MaxFix(0, 1, 4564);
        MaxFix(0, 1, 4564, 12);
        Profiler.EndSample();
    }

    private int Max(params int[] values) { // GC.Alloc for int array.
        int max = values[0];
        for (int i = 1; i < values.Length; i++) {
            if (values[i] > max) { 
                max = values[i];
            }
        }
        return max;
    }

    private int MaxFix(int v1, int v2) {
        return v1 > v2 ? v1 : v2;
    }

    private int MaxFix(int v1, int v2, int v3) {
        return v1 > v2 ? (v1 > v3 ? v1 : v3) : (v2 > v3 ? v2 : v3);
    }

    private int MaxFix(int v1, int v2, int v3, int v4) {
        return v1 > v2 ? 
            (v1 > v3 ? (v1 > v4 ? v1 : v4) : (v3 > v4 ? v3 : v4)) : 
            (v2 > v3 ? (v2 > v4 ? v2 : v4) : (v3 > v4 ? v3 : v4));
    }
}

