using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderResizer
{
    public static Collider SetSize<Collider>(this Collider comp, Vector3 size)
    {
        if (comp is BoxCollider)
        {
            (comp as BoxCollider).size = size;
        }
        else if (comp is CapsuleCollider)
        {
            (comp as CapsuleCollider).height = size.y;
            (comp as CapsuleCollider).radius = size.x / 2;
        }
        else if(comp is SphereCollider)
        {
            (comp as SphereCollider).radius = size.x / 2;
        }
        else if (comp is MeshCollider)
        {
            (comp as MeshCollider).transform.localScale = SkullAGMaths.divide((comp as MeshCollider).bounds.size, size);
        }

        return comp;
    }

    public static Collider Scale<Collider>(this Collider comp, Vector3 scale)
    {
        if (comp is BoxCollider)
        {
            (comp as BoxCollider).size = Vector3.Scale((comp as BoxCollider).size, scale);
        }
        else if (comp is CapsuleCollider)
        {
            (comp as CapsuleCollider).height *= scale.y;
            (comp as CapsuleCollider).radius *= scale.x;
        }
        else if (comp is SphereCollider)
        {
            (comp as SphereCollider).radius *= scale.x;
        }
        else if (comp is MeshCollider)
        {
            (comp as MeshCollider).transform.localScale = scale;
        }

        return comp;
    }

    public static Collider SetCenter<Collider>(this Collider comp, Vector3 vec)
    {
        if (comp is BoxCollider)
        {
            (comp as BoxCollider).center = vec;
        }
        else if (comp is CapsuleCollider)
        {
            (comp as CapsuleCollider).center = vec;
        }
        else if (comp is SphereCollider)
        {
            (comp as SphereCollider).center = vec;
        }

        return comp;
    }

    public static Collider MoveCenter<Collider>(this Collider comp, Vector3 vec)
    {
        if (comp is BoxCollider)
        {
            (comp as BoxCollider).center += vec;
        }
        else if (comp is CapsuleCollider)
        {
            (comp as CapsuleCollider).center += vec;
        }
        else if (comp is SphereCollider)
        {
            (comp as SphereCollider).center += vec;
        }

        return comp;
    }
}
