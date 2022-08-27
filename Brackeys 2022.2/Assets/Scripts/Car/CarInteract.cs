using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInteract : MonoBehaviour, Interactables
{
    public float MaxRange { get { return maxRange; } }
    [SerializeField] private float maxRange = 2f;

    public void OnStartHover()
    {
        print("enter");
    }

    public void OnInteract()
    {

    }

    public void OnEndHover()
    {
        print("end");
    }

}
    
