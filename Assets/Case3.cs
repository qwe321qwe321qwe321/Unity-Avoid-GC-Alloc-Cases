using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case3: yield & IEnumerator
/// </summary>
public class Case3 : ICase {
    public void Process() {
        Profiler.BeginSample("SimpleEnumerator");
        IEnumerator simpleEnumerator = SimpleEnumerator(); // GC.Alloc
        Profiler.EndSample();

        Profiler.BeginSample("SimpleEnumeratorYieldBreak");
        IEnumerator simpleEnumeratorYieldBreak = SimpleEnumeratorYieldBreak(); // GC.Alloc
        Profiler.EndSample();

        Profiler.BeginSample("SimpleEnumeratorFree");
        IEnumerator simpleEnumeratorFree = SimpleEnumeratorFree(); // Free
        Profiler.EndSample();

        ClassBase classA = new ClassA();
        ClassBase classB = new ClassB();
        ClassBase classC = new ClassC();
        Profiler.BeginSample("classA.SimpleEnumerator");
        classA.SimpleEnumerator(); // GC.Alloc
        Profiler.EndSample();
        Profiler.BeginSample("classB.SimpleEnumerator");
        classB.SimpleEnumerator(); // GC.Alloc
        Profiler.EndSample();
        Profiler.BeginSample("classC.SimpleEnumerator");
        classC.SimpleEnumerator(); // Free
        Profiler.EndSample();
    }

    private IEnumerator SimpleEnumerator() {
        yield return null;
    }

    private IEnumerator SimpleEnumeratorYieldBreak() {
        yield break;
    }

    private IEnumerator SimpleEnumeratorFree() {
        return null;
    }

    public abstract class ClassBase {
        public abstract IEnumerator SimpleEnumerator();
    }

    public class ClassA : ClassBase {
        public override IEnumerator SimpleEnumerator() {
            yield return null;
        }
    }

    public class ClassB : ClassBase {
        public override IEnumerator SimpleEnumerator() {
            yield break;
        }
    }

    public class ClassC : ClassBase {
        public override IEnumerator SimpleEnumerator() {
            return null;
        }
    }
}

