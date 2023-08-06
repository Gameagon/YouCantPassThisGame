using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Endlesscorridor : MonoBehaviour
{
    public float scrollSpeed = 1f;
    [SerializeField]private MeshRenderer mesh;
    public Material materialoffset;
    public Vector2 scroll;
    private bool on = false;
    private void Start()
    {
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
        //Debug.Log(dot);
            if (dot > 0.9 && Controller.current.movement.y == 1  && on || dot > 0.9 && Controller.current.movement == new Vector2(0.71f,0.71f) && on || dot > -0.9 && dot < 0.9 && Controller.current.movement.x == 1 && on || dot > -0.9 && dot < 0.9 && Controller.current.movement == new Vector2(1,1) && on || dot > -0.9 && dot < 0.9 && Controller.current.movement == new Vector2(-1, -1) && on || dot > -1 && Controller.current.movement.y == -1 && on || dot > -1 && Controller.current.movement == new Vector2(-1, -1) && on)
            {
                scrolltexture();

            }
            else
            {
                Controller.current.canmove = true;
                scroll = new Vector2(1.25f, 1.75f);

              //  Debug.Log(materialoffset.GetVector("_Offset"));
            }
            materialoffset.SetVector("_Offset", scroll);
        
      



    }
    public void scrolltexture()
    {
        Controller.current.canmove = false;
        scroll = new Vector2(1.25f, Time.realtimeSinceStartup * scrollSpeed);
        //  Debug.Log(Controller.current.movement);
    }







}
