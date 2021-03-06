using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Testbed : MonoBehaviour {
    public bool startProcess = true;

    private void Start() {
        TestAll(); // warm up for Mono.JIT
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            startProcess = true;
        }
        if (!startProcess) { return; }
            startProcess = false;

        TestAll();
        Debug.Break(); // pause game.
    }

    private void TestAll() {
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
        Test6();
        Test7();
        Test8();
        Test9();
        Test10();
        Test11();
        TestUniTask();
        TestUnityAPI();
    }

    private void Test1() {
        Case1 case1 = new Case1() {
            source = this.transform,
            destinations = new Transform[] { this.transform, this.transform }
        };
        CaseFix1 caseFix1 = new CaseFix1() {
            source = this.transform,
            destinations = new Transform[] { this.transform, this.transform }
        };

        Profiler.BeginSample("Test1");
        case1.Process();
        caseFix1.Process();
        Profiler.EndSample();
    }

    private void Test2() {
        Case2 case2 = new Case2();
        Profiler.BeginSample("Test2");
        case2.Process();
        Profiler.EndSample();
    }

    private void Test3() {
        Case3 case3 = new Case3();
        Profiler.BeginSample("Test3");
        case3.Process();
        Profiler.EndSample();
    }

    private void Test4() {
        Case4 case4 = new Case4(){
            coroutineProxy = this
        };
        Profiler.BeginSample("Test4");
        case4.Process();
        Profiler.EndSample();
    }

    private void Test5() {
        Case5 case5 = new Case5(){
            coroutineProxy = this
        };
        Profiler.BeginSample("Test5");
        case5.Process();
        Profiler.EndSample();
    }

    private void Test6() {
        Case6 case6 = new Case6();
        Profiler.BeginSample("Test6");
        case6.Process();
        Profiler.EndSample();
    }

    private void Test7() {
        Case7 case7 = new Case7();
        Profiler.BeginSample("Test7");
        case7.Process();
        Profiler.EndSample();
    }

    private void Test8() {
        Case8 case8 = new Case8();
        Profiler.BeginSample("Test8");
        case8.Process();
        Profiler.EndSample();
    }

    private void Test9() {
        Case9 case9 = new Case9();
        Profiler.BeginSample("Test9");
        case9.Process();
        Profiler.EndSample();
    }

    private void Test10() {
        Case10 case10 = new Case10();
        Profiler.BeginSample("Test10");
        case10.Process();
        Profiler.EndSample();
    }

    private void Test11() {
        Case11 case11 = new Case11();
        Profiler.BeginSample("Test11");
        case11.Process();
        Profiler.EndSample();
    }


    private void TestUniTask() {
        CaseUniTask caseUniTask = new CaseUniTask(){
            coroutineProxy = this
        };

        Profiler.BeginSample("TestUniTask");
        caseUniTask.Process();
        Profiler.EndSample();
    }


    private void TestUnityAPI() {
        CaseUnityAPIs caseUnityAPI = new CaseUnityAPIs(){
            sampleObject = this.gameObject,
            sampleRenderer = this.gameObject.AddComponent<MeshRenderer>(),
        };

        Profiler.BeginSample("TestUnityAPI");
        caseUnityAPI.Process();
        Profiler.EndSample();
        Destroy(caseUnityAPI.sampleRenderer);
    }
}

public interface ICase {
    void Process();
}