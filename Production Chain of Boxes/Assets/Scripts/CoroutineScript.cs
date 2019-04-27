using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineScript : MonoBehaviour
{
    public Transform[] markers;
    public GameObject box1, box2,box3,box4;
    public int whichBoxIsOn = 1;
    public int count = 0;
    public float media;
    public float desviacion;
    public GameObject panel;
    public Bitacora bitacora;
    // Start is called before the first frame update
    void Start()
    {
        box1.gameObject.SetActive(true);
        box2.gameObject.SetActive(false);
        box3.gameObject.SetActive(false);
        box4.gameObject.SetActive(false);
        StartCoroutine(FollowMarkers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void CargarMarkets(Transform[] markers)
    {
        this.markers = markers;
    }
    IEnumerator Move(Vector3 toPosition, float speed){
        while(toPosition != transform.position){
            transform.position = Vector3.MoveTowards(transform.position, toPosition, speed * Time.deltaTime);
            yield return null;
        }
        //Destroy(gameObject);
    }

    IEnumerator FollowMarkers(){
        bitacora.horasEstacion.Add(DateTime.Now);
        int count = 0;
        foreach (var marker in markers)
        {
            float velocidad =4/ (float)bitacora.listaVelocidades[count];
            yield return StartCoroutine(Move(marker.position,velocidad));
            bitacora.horasEstacion.Add(DateTime.Now);
            SwitchBoxes();
            count++;
        }
        box4.SetActive(false);
    }
    public void SwitchBoxes()
    {
        switch (whichBoxIsOn)
        {
            case 1:
                whichBoxIsOn = 2;
                box1.gameObject.SetActive(false);
                box2.gameObject.SetActive(true);
                box3.gameObject.SetActive(false);
                box4.gameObject.SetActive(false);
                break;
            case 2:
                whichBoxIsOn = 3;
                box1.gameObject.SetActive(false);
                box2.gameObject.SetActive(false);
                box3.gameObject.SetActive(true);
                box4.gameObject.SetActive(false);
                break;
        case 3:
            whichBoxIsOn = 4;
            box1.gameObject.SetActive(false);
            box2.gameObject.SetActive(false);
            box3.gameObject.SetActive(false);
            box4.gameObject.SetActive(true);
            break;

        }
    }

}
