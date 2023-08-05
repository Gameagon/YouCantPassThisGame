using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Endlesscorridor : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    [SerializeField]private MeshRenderer mesh;
    public Material materialoffset;
    public Vector2 scroll;

    private void Start()
    {
        materialoffset.SetVector("Offset", scroll);
    }
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        scrollSpeed = scrollSpeed + scrollSpeed;

        scroll = new Vector2(0, scrollSpeed);
       // materialoffset.mainTextureOffset = scroll;
        float dot = Vector3.Dot(other.gameObject.transform.forward, Vector3.forward);
        if (other.gameObject.layer == 3)
        {
            if (dot > 0.9)
            {


            }

            //  mesh.material.of = new Vector2(0, Time.realtimeSinceStartup * scrollSpeed);

        }
        materialoffset.SetVector("Offset", scroll);
    }

    public void Update()
    {
        scrollSpeed = scrollSpeed + scrollSpeed;

        scroll = new Vector2(0, scrollSpeed);

        float offset = Time.time * scrollSpeed;
        Debug.Log(offset);
        materialoffset.SetVector("Offset", scroll);

        
    }







}
