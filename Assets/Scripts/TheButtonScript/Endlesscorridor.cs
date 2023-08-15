using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Endlesscorridor : MonoBehaviour
{
    public float scrollSpeed = 1f;
    float scrollwall = 0;
    [SerializeField]private MeshRenderer mesh;
    [SerializeField] private GameObject bone;
    public Material materialoffset;
    public Vector2 scroll;
    private bool on = false;

    private void Start()
    {
        scrollwall = scrollSpeed / 4;
        //materialoffset.SetVector("Offset", scroll);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            on = true;
            //  mesh.material.of = new Vector2(0, Time.realtimeSinceStartup * scrollSpeed);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            on = false;

            //  mesh.material.of = new Vector2(0, Time.realtimeSinceStartup * scrollSpeed);

        }
    }
    // Start is called before the first frame update


    public void Update()
    {

            float dot = Vector3.Dot(Controller.current.gameObject.transform.forward, Vector3.forward);
          // Debug.Log(dot);
            if (dot > 0.10 && Controller.current.movement.y >= 0.1  && on || dot < -0.7 && Controller.current.movement.y < -0.50 && on)
            {
               
                scrolltexture(scrollSpeed);

            }
            else if (dot >= -0.3 && dot < 0.9 && Controller.current.movement.x < 0 && Controller.current.gameObject.transform.forward.x > 0.01 && on || dot >= -0.9 && dot < 0.9 && Controller.current.movement.x > 0.70 && Controller.current.gameObject.transform.forward.x < 0.01 && on)
            {
                 scrolltexture(scrollwall);
             }
            else
            {
                Controller.current.canmove = true;
               // scroll = new Vector2(1.25f, 1.75f);

              //  Debug.Log(materialoffset.GetVector("_Offset"));
            }
           // materialoffset.SetVector("_Offset", scroll);
        
      



    }
    public void scrolltexture(float scrollmult)
    {
       bone.transform.position += new Vector3(0, 0.1f, 0); 
       Debug.Log(Controller.current.movement);
    }







}
