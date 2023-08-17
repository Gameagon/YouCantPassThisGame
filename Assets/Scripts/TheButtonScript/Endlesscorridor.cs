using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Endlesscorridor : MonoBehaviour
{

    [SerializeField] private Transform Target;
    public Vector3 offset;
    public Vector3 maxPosition;
    public Vector3 minPosition;
    public bool x, y, z;
    public Vector3 axis { get { return new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0); } }

    // Start is called before the first frame update


    public void Update()
    {

        transform.position = Vector3.Scale(Vector3.Max(Vector3.Min(Target.position + offset, maxPosition), minPosition), axis) + Vector3.Scale(transform.position, (Vector3.one - axis));
    }








}
