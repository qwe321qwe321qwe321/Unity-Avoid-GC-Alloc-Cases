using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Testbed : MonoBehaviour {
    public bool startProcess = true;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            startProcess = true;
        }
        if (!startProcess) { return; }
            startProcess = false;
        Test1();
        Test2();
        Test3();
        Test4();
        Test5();
        TestUniTask();
        Debug.Break(); // pause game.
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

    private void TestUniTask() {
        CaseUniTask caseUniTask = new CaseUniTask(){
            coroutineProxy = this
        };

        Profiler.BeginSample("TestUniTask");
        caseUniTask.Process();
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
}

public interface ICase {
    void Process();
}