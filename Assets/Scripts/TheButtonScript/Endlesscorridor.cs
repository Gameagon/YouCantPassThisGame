using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlesscorridor : MonoBehaviour
{
    public float scrollSpeed = 1000;
    [SerializeField]private Texture2D mesh;
    // Start is called before the first frame update
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (transform.position.x <= -20)
            {
                collision.gameObject.transform.position = new Vector3(60, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            }
            Debug.Log(mesh.width);
          //  mesh.material.of = new Vector2(0, Time.realtimeSinceStartup * scrollSpeed);
            
        }
    }




}
