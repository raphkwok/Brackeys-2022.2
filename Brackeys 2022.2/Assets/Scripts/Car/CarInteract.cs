using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CarInteract : MonoBehaviour, Interactables
{
    public float MaxRange { get { return maxRange; } }
    [SerializeField] private float maxRange = 2f;
    [SerializeField] private float fadeTime = 0.01f;

    public GameObject car;
    [SerializeField] private GameObject carCam;
    [SerializeField] private GameObject player;
    private bool inCar;

    private GameObject textObject;
    private TMP_Text text;

    /**
    public void OnLeave(InputValue value)
    {
        if (inCar)
        {
            car.GetComponent<CarMovement>().enabled = false;
            carCam.SetActive(false);
            player.SetActive(true);
            player.transform.position = transform.position + Vector3.right * 3;
        }

    }
    **/
    public void Awake()
    {
        car.GetComponent<CarMovement>().enabled = false;
        carCam.SetActive(false);
        textObject = GameObject.Find("DoorText");
        text = textObject.GetComponent<TMP_Text>();
    }

    public void OnStartHover()
    {
        text.color = Color32.Lerp(new Color32(225, 225, 225, 225), new Color32(225, 225, 225, 0), Time.deltaTime * fadeTime);
    }

    public void OnInteract()
    {
        text.color = Color32.Lerp(new Color32(225, 225, 225, 0), new Color32(225, 225, 225, 225), Time.deltaTime * fadeTime);
        inCar = true;
        car.GetComponent<CarMovement>().enabled = true;
        carCam.SetActive(true);
        player.SetActive(false);
    }

    public void OnEndHover()
    {
        text.color = Color32.Lerp(new Color32(225, 225, 225, 0), new Color32(225, 225, 225, 225), Time.deltaTime * fadeTime);
    }

}
    
