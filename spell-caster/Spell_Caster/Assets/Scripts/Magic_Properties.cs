using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Properties : MonoBehaviour {


    public void MagicLaunched(GameObject MagicObject, float MagicSpeed)
    {
        Rigidbody rb;
        rb = MagicObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * MagicSpeed,ForceMode.Impulse);


    }
}
