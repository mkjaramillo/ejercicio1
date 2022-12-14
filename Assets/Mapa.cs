using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Mapa : MonoBehaviour
{
    //representa el mapa con 1 que es el obtaculo
    private int[,]mapaChar={
        {1,1,0,0,0,0,0,0,1,0},
        {1,1,0,0,0,0,0,0,1,0},
        {0,0,0,0,0,0,0,0,0,0},
        {1,0,0,1,1,1,1,1,1,0},
        {0,0,0,0,0,1,0,1,0,0},
        {0,0,0,0,0,0,0,1,0,0},
        {0,0,1,0,0,0,0,1,0,0},
        {0,1,1,0,0,0,0,0,0,0},
        {0,0,1,0,0,0,0,1,1,1},
        {0,0,0,0,0,0,0,0,0,0}
    };
    private Nodo[,] mapaNodos;
    private int ancho;
    private int alto;
    private Nodo objetivo;
    private List<Nodo> abierta =new List<Nodo>();
    private List<Nodo> cerrada =new List<Nodo>();
    public List<Nodo> ruta =new List<Nodo>();

    public int inicioX=5;
    public int inicioY=4;
    [SerializeField] public GameObject jugador;
    [SerializeField] public GameObject personaje;
    [SerializeField] public GameObject obstaculo;


    // Start is called before the first frame update
    void Start()
    {
      ancho=mapaChar.GetLength(0);
      alto=mapaChar.GetLength(1);
      mapaNodos=new Nodo[ancho,alto];
      //creamos nodos del mapa
      int fila=0;
      int columna=0;
      int jugadorx=(int)jugador.transform.position.x;
      int jugadory=(int)jugador.transform.position.z;
      inicioX=(int)personaje.transform.position.x;
      inicioY=(int)personaje.transform.position.z;
      objetivo=new Nodo(jugadorx,jugadory,0,null);
      Debug.Log(objetivo+"     "+inicioX+", "+inicioY);
      //rellenar el mapa con nodos
      for(columna=0;columna<alto;columna++){
        for(fila=0;fila<ancho;fila++){
            mapaNodos[fila,columna]=new Nodo(fila,columna,mapaChar[fila,columna],objetivo);
        }
      }
      mapaNodos[jugadorx,jugadory]=objetivo;
      ///

      fila=0;
      columna=0;
      for(fila=0;fila<ancho;fila++){
        for(columna=0;columna<ancho;columna++){
            if(mapaNodos[fila,columna].Tipo==1){
                //creamos un obstaculo
                Instantiate(obstaculo,new Vector3(fila+0.5f,0.5f,columna+0.5f),Quaternion.identity); 
                    
                
            }
        }
      }
//invocamos el algoritmo
        AStar();
        //obtenemos la ruta
        MuestraRuta();


    }

    public void AStar(){
        Nodo actual =null;
        //adicionamos el nodo de inicio a la lista abierta
        abierta.Add(mapaNodos[inicioX,inicioY]);
        //mietras tengamos nodos en la lista abierta
        while (abierta.Count!=0)
        {
            //encontramos el que tiene menos puntaje 
            
          abierta.Sort();
          actual=abierta[0];
          //verificamos si estamos en el objetivo
          if(actual.X==objetivo.X&&actual.Y==objetivo.Y){
            Debug.Log("llegamos a objetivo");
            break;
          }else{
            //movemos el actual a la lista cerrada
            abierta.RemoveAt(0);
            cerrada.Add(actual);
            //verificamos los nodos adyacentes
            for(int fila=actual.X-1;fila<=actual.X+1;fila++)
                for(int columna=actual.Y-1;columna<=actual.Y+1;columna++)
                {
                    //verificamos si cumple las condiciones
                    if(abierta.Contains(mapaNodos[tFila(fila),tColumna(columna)])==false&&cerrada.Contains(mapaNodos[tFila(fila),tColumna(columna)])==false &&mapaNodos[tFila(fila),tColumna(columna)].Tipo!=1)
                    {
                        //calculamos valores
                        mapaNodos[tFila(fila),tColumna(columna)].Anterior=actual;
                        mapaNodos[tFila(fila),tColumna(columna)].CalculaCosto();
                        //adicionamos a la abierta
                        abierta.Add(mapaNodos[tFila(fila),tColumna(columna)]);

                    }
                }
          }  
        }
    }
    public void MuestraRuta(){
        Nodo trabajo=objetivo;
        Debug.Log("en muestra ruta");
        while(trabajo.Anterior!=null){
            Debug.Log(trabajo.X+", "+trabajo.Y);
            ruta.Add(trabajo);
            mapaNodos[trabajo.X,trabajo.Y].Tipo=2;
            trabajo=trabajo.Anterior;
        }
        ruta.Reverse();
    }
    public Nodo ObtenNodo(int pIndice){
        return ruta[pIndice];
    }
    public int tColumna(int pColumna){
        if(pColumna<0)
            return 0;
        if(pColumna>=ancho)
            return ancho-1;
        return pColumna;
    }
    public int tFila(int pFila){
        if(pFila<0)
            return 0;
        if(pFila>=alto)
            return alto-1;
        return pFila;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
