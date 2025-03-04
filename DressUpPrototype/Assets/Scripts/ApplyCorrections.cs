using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyCorrections : MonoBehaviour
{
    [SerializeField] private Transform Transform;
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Transform.gameObject.GetComponent<Rigidbody2D>().velocity;
    }
}
