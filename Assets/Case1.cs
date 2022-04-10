using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// Case1: 能用struct就不要用class
/// </summary>
public class Case1 : ICase {
    public Transform source;
    public Transform[] destinations;

    public void Process() {
        Profiler.BeginSample("Case1.CopyTransformTo");
        CopyTransformTo(source, destinations);
        Profiler.EndSample();
    }

    public class Transform2DValue {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform2DValue(Transform transform) {
            this.position = transform.position;
            this.rotation = transform.eulerAngles.z;
            this.scale = transform.localScale;
        }

        public void Apply(Transform destination) {
            destination.position = this.position;
            destination.eulerAngles = new Vector3(0, 0, this.rotation);
            destination.localScale = this.scale;
        }
    }

    public void CopyTransformTo(Transform source, Transform[] destinations) {
        Transform2DValue transformValue = new Transform2DValue(source); // GC.Alloc!
        foreach (var item in destinations) {
            transformValue.Apply(item);
        }
    }
}

public class CaseFix1 : ICase {
    public Transform source;
    public Transform[] destinations;

    public void Process() {
        Profiler.BeginSample("CaseFix1.CopyTransformTo");
        CopyTransformTo(source, destinations);
        Profiler.EndSample();
    }

    public struct Transform2DValue {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform2DValue(Transform transform) {
            this.position = transform.position;
            this.rotation = transform.eulerAngles.z;
            this.scale = transform.localScale;
        }

        public void Apply(Transform destination) {
            destination.position = this.position;
            destination.eulerAngles = new Vector3(0, 0, this.rotation);
            destination.localScale = this.scale;
        }
    }

    public void CopyTransformTo(Transform source, Transform[] destinations) {
        Transform2DValue transformValue = new Transform2DValue(source); // GC.Alloc!
        foreach (var item in destinations) {
            transformValue.Apply(item);
        }
    }
}
