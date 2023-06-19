using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public static Prefabs Singleton;

    public GameObject player;
    public GameObject mob;
    public GameObject bullet;

    void Awake()
    {
        Singleton = this;
    }
}
