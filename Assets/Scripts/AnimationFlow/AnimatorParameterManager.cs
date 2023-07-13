using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
using static AnimatorParameterManager;
using UnityEditor.Events;

public class AnimatorParameterManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorControllerParameter> actions = new List<AnimatorControllerParameter>();

    public UnityEventBase ueb;

    [SerializeField]
    public Parameter[] parameters;
    private void OnValidate()
    {
        if(animator)
        {
            parameters = animator.parameters.Select((e,id) => new Parameter(e.name, id)).ToArray();
            //parameters[0].defaultBool = !parameters[0].defaultBool;
        }
    }

    public void SetParameterByName(int id)
    {
        //UnityEventTools
    }

    [Serializable]
    public class Parameter
    {
        public Parameter(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public string name;
        public int id;
        public void SetValue<T>(T value)
        {
        }
    }
}
