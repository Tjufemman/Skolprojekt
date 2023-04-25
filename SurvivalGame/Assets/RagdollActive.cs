using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActive : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;

    // Start is called before the first frame update
    void Awake()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        DoRagdoll(true);
    }

    public void DoRagdoll(bool isRagDoll)
    {
        foreach(var collider in AllColliders)
            collider.enabled = isRagDoll;
        MainCollider.enabled = isRagDoll;
        GetComponent<Rigidbody>().useGravity = !isRagDoll;
        GetComponent<Animator>().enabled = !isRagDoll;
    }
}
