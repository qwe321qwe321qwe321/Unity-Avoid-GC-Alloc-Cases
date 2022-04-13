using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case11: Enum.HasFlag()
/// </summary>
public class Case11 : ICase {
    public void Process() {
        AttackAttribute attackAttribute = AttackAttribute.Poison | AttackAttribute.Death;
        Profiler.BeginSample("Enum.HasFlag 1");
        Attack1(attackAttribute);
        Profiler.EndSample();
        Profiler.BeginSample("Enum.HasFlag 2");
        Attack2(attackAttribute);
        Profiler.EndSample();

        Profiler.BeginSample("Enum.HasFlag 2 (Fix)");
        Attack2Fix(attackAttribute);
        Profiler.EndSample();
    }

    [System.Flags]
    private enum AttackAttribute {
        None = 0,
        Poison = 1 << 0,
        Burning = 1 << 1,
        Death = 1 << 2,
    }
    private void Attack1(AttackAttribute attackAttribute) {
        if (attackAttribute.HasFlag(AttackAttribute.Poison)) {
            // poison.
        }
        if (attackAttribute.HasFlag(AttackAttribute.Burning)) {
            // burning.
        }
        if (attackAttribute.HasFlag(AttackAttribute.Death)) {
            // death.
        }
    }

    private void Attack2(AttackAttribute attackAttribute) {
        if (HandleFlag(attackAttribute, AttackAttribute.Poison)) {
            // poison.
        }
        if (HandleFlag(attackAttribute, AttackAttribute.Burning)) {
            // burning.
        }
        if (HandleFlag(attackAttribute, AttackAttribute.Death)) {
            // death.
        }
    }

    private bool HandleFlag(AttackAttribute attackAttribute, AttackAttribute flag) {
        if (attackAttribute.HasFlag(flag)) {
            return true;
        }
        return false;
    }

    private void Attack2Fix(AttackAttribute attackAttribute) {
        if (HandleFlagFix(attackAttribute, AttackAttribute.Poison)) {
            // poison.
        }
        if (HandleFlagFix(attackAttribute, AttackAttribute.Burning)) {
            // burning.
        }
        if (HandleFlagFix(attackAttribute, AttackAttribute.Death)) {
            // death.
        }
    }

    private bool HandleFlagFix(AttackAttribute attackAttribute, AttackAttribute flag) {
        if ((attackAttribute & flag) != 0) {
            return true;
        }
        return false;
    }
}

