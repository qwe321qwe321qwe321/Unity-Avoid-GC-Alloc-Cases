using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

/// <summary>
/// CaseUnityAPIs
/// </summary>
public class CaseUnityAPIs : ICase {
    public GameObject sampleObject;
    public Renderer sampleRenderer;

    public void Process() {
        CompareTags();
        Physics2DCastAlls();
        Physics2DOverlapAlls();
        // Collision2D is hard to be populated.
        //Collision2DGetContact(collision);
        RendererMaterials(sampleRenderer);
    }

    private void CompareTags() {
        Profiler.BeginSample("CompareTags");

        // GC.Alloc by obj.tag getter.
        if (sampleObject.tag == "Player") {
            // do something
        }

        Profiler.EndSample();
        Profiler.BeginSample("CompareTags (Fix)");

        // No GC.Alloc
        if (sampleObject.CompareTag("Player")) {
            // do something
        }

        Profiler.EndSample();
    }

    // This array is used for Physics2D NonAlloc Cast methods.
    private static RaycastHit2D[] s_RaycastHitCaches = new RaycastHit2D[100];
    private void Physics2DCastAlls() {
        Vector2 origin = Vector2.zero;
        Vector2 direction = Vector2.right;
        float distance = 1f;

        Profiler.BeginSample("Physics2D.CastAlls");

        // GC.Alloc from array allocation.
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);
        for (int i = 0; i < hits.Length; i++) {
            // hits[i]...
        }

        // Same with:
        //  Physics2D.CircleCastAll, Physics2D.BoxCastAll,
        //  Physics2D.CapsuleCastAll, Physics2D.LinecastAll,
        //  Physics2D.GetRayIntersectionAll.

        Profiler.EndSample();

        Profiler.BeginSample("Physics2D.CastAlls (Fix)");

        // No GC.Alloc
        int hitCount = Physics2D.RaycastNonAlloc(origin, direction, s_RaycastHitCaches, distance);
        for (int i = 0; i < hitCount; i++) {
            // s_RaycastHitCaches[i]...
        }

        // Btw, single cast method won't GC.Alloc since it doesn't need to allocate an array.
        RaycastHit2D firstHit = Physics2D.Raycast(origin, direction, distance);

        Profiler.EndSample();
    }

    // This array is used for Physics2D NonAlloc Overlap methods.
    private static Collider2D[] s_OverlapCaches = new Collider2D[100];
    private void Physics2DOverlapAlls() {
        Vector2 origin = Vector2.zero;
        float radius = 1f;

        Profiler.BeginSample("Physics2D.OverlapAlls");

        // GC.Alloc from array allocation.
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius);
        for (int i = 0; i < hits.Length; i++) {
            // hits[i]...
        }

        // Same with:
        //  Physics2D.OverlapCapsuleAll, Physics2D.OverlapBoxAll,
        //  Physics2D.OverlapAreaAll, Physics2D.OverlapPointAll

        Profiler.EndSample();

        Profiler.BeginSample("Physics2D.OverlapAlls (Fix)");

        // No GC.Alloc
        int hitCount = Physics2D.OverlapCircleNonAlloc(origin, radius, s_OverlapCaches);
        for (int i = 0; i < hitCount; i++) {
            // s_OverlapCaches[i]...
        }

        // Btw, single overlap method won't GC.Alloc since it doesn't need to allocate an array.
        Collider2D firstHit = Physics2D.OverlapCircle(origin, radius);

        Profiler.EndSample();
    }

    private static ContactPoint2D[] s_ContactPointCaches = new ContactPoint2D[100];
    private void Collision2DGetContact(Collision2D collision2D) {
        Profiler.BeginSample("Collision2D.contacts");
        ContactPoint2D[] contacts = collision2D.contacts;
        for (int i = 0; i < contacts.Length; i++) {
            Vector2 point = contacts[i].point;
            // do something.
        }
        Profiler.EndSample();

        Profiler.BeginSample("Collision2D.GetContacts");
        int contactCount = collision2D.GetContacts(s_ContactPointCaches);
        for (int i = 0; i < contactCount; i++) {
            Vector2 point = s_ContactPointCaches[i].point;
            // do something.
        }
        Profiler.EndSample();

        Profiler.BeginSample("Collision2D.GetContact(i)");
        for (int i = 0; i < contactCount; i++) {
            ContactPoint2D contact = collision2D.GetContact(i);
            Vector2 point = contact.point;
            // do something.
        }
        Profiler.EndSample();
    }

    private static List<Material> s_MaterialCacheList = new List<Material>(32);
    private void RendererMaterials(Renderer renderer) {
        Profiler.BeginSample("Renderer.sharedMaterials");

        Material[] materials = renderer.sharedMaterials;

        Profiler.EndSample();

        Profiler.BeginSample("Renderer.GetSharedMaterials");

        s_MaterialCacheList.Clear();
        renderer.GetSharedMaterials(s_MaterialCacheList);

        Profiler.EndSample();
    }
}

