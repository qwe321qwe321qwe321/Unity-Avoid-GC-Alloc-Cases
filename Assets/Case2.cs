using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case2: non-constant string會造成GC.Alloc
/// </summary>
public class Case2 : ICase {
    public void Process() {
        Profiler.BeginSample("Constant strings"); // These cases are allocation-free.
        const string constant = "constant";
        StringParameter("constant");
        StringParameter(constant);
        string constantStringConcat = "string" + "concat"; // Compiler optimize it as "stringconcat" directly.
        string constantStringConcat2 = "string" + constant; // Compiler optimize it as "stringconcat" directly.
        Profiler.EndSample();

        Profiler.BeginSample("Non-constant string concat");
        string nonConstant = "nonConstant";
        string nonConstantConcat = "string" + nonConstant; // GC.Alloc
        Profiler.EndSample();

        Profiler.BeginSample("string.Concat");
        string stringConcat = string.Concat("string", "concat"); // GC.Alloc
        Profiler.EndSample();
        Profiler.BeginSample("MethodConcat");
        string methodConcat = MethodConcat("string", "concat"); // GC.Alloc
        Profiler.EndSample();

        Profiler.BeginSample("int.ToString()");
        string intString = 1.ToString(); // GC.Alloc
        Profiler.EndSample();
    }

    private void StringParameter(string nonConstant) {
        return;
    }

    private string MethodConcat(string lhs, string rhs) {
        return lhs + rhs;
    } 
}
