using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case9: Boxing.
/// </summary>
public class Case9 : ICase {
    public void Process() {
        KindOfStruct kindOfStructA = new KindOfStruct();
        KindOfStruct kindOfStructB = new KindOfStruct();
        KindOfStructFix kindOfStructFixA = new KindOfStructFix();
        KindOfStructFix kindOfStructFixB = new KindOfStructFix();


        Profiler.BeginSample("Object boxing: Parameter");
        ObjectParameter(kindOfStructA); // boxing: KindOfStruct -> object
        Profiler.EndSample();

        Profiler.BeginSample("Object boxing: Equals");
        kindOfStructA.Equals(kindOfStructB); // double boxing: KindOfStruct -> System.ValueType and KindOfStruct -> object
        Profiler.EndSample();

        Profiler.BeginSample("Object boxing: GetHashCode");
        kindOfStructA.GetHashCode(); // boxing: KindOfStruct -> System.ValueType
        Profiler.EndSample();

        Profiler.BeginSample("Object boxing: Equals (Fix)");
        kindOfStructFixA.Equals(kindOfStructFixB); // No boxing as it's implemented method.
        Profiler.EndSample();

        Profiler.BeginSample("Object boxing: GetHashCode (Fix)");
        kindOfStructFixA.GetHashCode(); // No boxing as it's implemented method.
        Profiler.EndSample();

        Profiler.BeginSample("Interface boxing: Parameter");
        InterfaceParameter(kindOfStructA); // boxing: KindOfStruct -> IKindOfInterface
        Profiler.EndSample();

    }

    private void ObjectParameter(object obj) {
        return;
    }

    private void InterfaceParameter(IKindOfInterface kindOfInterface) {
        kindOfInterface.DoSomething();
    }

    private interface IKindOfInterface {
        void DoSomething();
    }

    private struct KindOfStruct : IKindOfInterface {
        public int value;
        public void DoSomething() { }
    }

    private struct KindOfStructFix : IKindOfInterface, System.IEquatable<KindOfStructFix> {
        public int value;
        public void DoSomething() { }

        public bool Equals(KindOfStructFix other) {
            return value == other.value;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }
    }
}

