using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{

    public Quaternion InitialRotation;
    // Start is called before the first frame update
    void Awake()
    {
        InitialRotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = InitialRotation;
    }
}
