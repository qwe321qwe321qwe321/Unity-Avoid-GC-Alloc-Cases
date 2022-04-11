using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case8: Temporary containers.
/// </summary>
public class Case8 : ICase {
    public void Process() {
        Profiler.BeginSample("Temporary Array");
        TemporaryArray();
        Profiler.EndSample();
        Profiler.BeginSample("Temporary Array (Fix1)");
        TemporaryArrayFix1();
        Profiler.EndSample();
        Profiler.BeginSample("Temporary Array (Fix2)");
        TemporaryArrayFix2();
        Profiler.EndSample();


        Profiler.BeginSample("Temporary List");
        TemporaryList();
        Profiler.EndSample();
        Profiler.BeginSample("Temporary List (Fix)");
        TemporaryListFix();
        Profiler.EndSample();
    }

    private void TemporaryArray() {
        int[] numbers = new int[10];
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i] = i + 1;
        }
    }

    private void TemporaryArrayFix1() {
        int[] numbers = System.Buffers.ArrayPool<int>.Shared.Rent(10);
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i] = i + 1;
        }
        System.Buffers.ArrayPool<int>.Shared.Return(numbers);
    }

    private void TemporaryArrayFix2() {
        System.Span<int> numbers = stackalloc int[10];
        for (int i = 0; i < numbers.Length; i++) {
            numbers[i] = i + 1;
        }
    }

    private void TemporaryList() {
        List<int> numbers = new List<int>();
        for (int i = 0; i < 10; i++) {
            numbers.Add(i + 1);
        }
    }

    private void TemporaryListFix() {
        List<int> numbers = ListPool<int>.Shared.Rent();
        for (int i = 0; i < 10; i++) {
            numbers.Add(i + 1);
        }
        ListPool<int>.Shared.Return(numbers);
    }


    public class ListPool<T> {
        public static ListPool<T> Shared = new ListPool<T>();

        private Stack<List<T>> m_Inactived = new Stack<List<T>>();

        public List<T> Rent() {
            while(m_Inactived.Count > 0) {
                List<T> pop = m_Inactived.Pop();
                if (pop != null) {
                    return pop;
                }
            }

            return new List<T>();
        }

        public void Return(List<T> list) {
            if (list == null) { return; }
            list.Clear();
            m_Inactived.Push(list);
        }
    }
}

