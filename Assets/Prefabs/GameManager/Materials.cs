using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    public static Materials Singleton;

    public Material black;
    public Material green;
    public Material orange;
    public Material purple;
    public Material red;
    public Material white;

    void Awake()
    {
        Singleton = this;
    }
}
