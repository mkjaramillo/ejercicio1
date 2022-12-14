using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
//va a girar la camara para ver cde diferentes puntos de vista como llega a la solucion
{
    float a = 20; //velocidad de la camara
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //indica en punto sobre el cual quiero rotar-el eje en el cual estoy rotando-la cantidad que estoy rotando en ese momento
        transform.RotateAround( new Vector3(5,0,5),Vector3.up,a*Time.deltaTime);
    }
}
