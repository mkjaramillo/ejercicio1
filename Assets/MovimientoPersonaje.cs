using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    public Mapa mapa;
    private int nodoActual=0;
    private bool enObjetivo=false;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(enObjetivo==false){
            //verificamos si hemos llegado al nodo
            Vector3 nodoObjetivo=new Vector3(mapa.ruta[nodoActual].X+0.5f,0.5f,mapa.ruta[nodoActual].Y+0.5f);
            if(Vector3.Magnitude(transform.position-nodoObjetivo)<0.1){
                nodoActual++;
                if(nodoActual==mapa.ruta.Count)
                    enObjetivo=true;
            }
            //vemos al objetivo
            transform.LookAt(nodoObjetivo);
            transform.Translate(Vector3.forward*Time.deltaTime);
        }
    }
}
