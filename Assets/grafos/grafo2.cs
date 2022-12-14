using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NodoGrafo
{
    public float x;
    public float z;
    public NodoGrafo(float pX,float pZ) {
        {
            x=pX;
            z=pZ;
        }
    }
}

public class grafo2 : MonoBehaviour
{
    
    public int inicio=0;
    public int final=6;
    public List<int> ruta;
    public GameObject personaje;
    private int[,] mAdyacencia;
    private int[] indegree;
    private int cantNodos=7;
    public NodoGrafo[] nodosCoords;
    private bool inicializado=false;
    void Start()
    {
        //iniciamos matriz de adyacencia
        mAdyacencia=new int[cantNodos,cantNodos];
        //arreglo indegree
        indegree=new int[cantNodos];
        //coordenadas de nodos
        nodosCoords= new NodoGrafo[cantNodos];

        ruta= new List<int>();
        AdicionaArista(0,1);
        AdicionaArista(0,3);
        AdicionaArista(1,4);
        AdicionaArista(2,5);

        AdicionaArista(3,2);
        AdicionaArista(3,4);
        AdicionaArista(3,5);

        AdicionaArista(4,6);
        AdicionaArista(6,5);

        AdicionaCoords(0,17,50);
        AdicionaCoords(1,35,55);
        AdicionaCoords(2,5,25);
        AdicionaCoords(3,25,30);
        AdicionaCoords(4,45,27);
        AdicionaCoords(5,15,5);
        AdicionaCoords(6,33,7);

        inicializado=true;
        personaje.transform.position= new Vector3(nodosCoords[inicio].x,0.5f,nodosCoords[inicio].z);
        CalcularRuta();
    
    }
    //para que no dibuje cosas que no estan inicializadas
    private void InDisable(){
        inicializado=false;
    }
    public void AdicionaArista(int pNodoInicio, int pNodoFinal){
        mAdyacencia[pNodoInicio,pNodoFinal]=1;
        //si la arista se mueve en ambas direcciones
        mAdyacencia[pNodoFinal,pNodoInicio]=1;
    }
    public void AdicionaCoords(int pNodo, float pX,float pZ){
        nodosCoords[pNodo]= new NodoGrafo(pX,pZ);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //
    private void OnDrawGizmos() {
        if(inicializado){
            foreach (NodoGrafo miNodo in nodosCoords)
            {
                Gizmos.color=Color.green;
                Gizmos.DrawSphere(new Vector3(miNodo.x,0,miNodo.z),0.5f);
            }
            int n=0;
            int m=0;
            for(n=0; n< cantNodos;n++)
                for(m=0;m<cantNodos;m++)
                {
                    if(mAdyacencia[n,m]!=0)
                    {
                        Gizmos.color=Color.yellow;
                        Gizmos.DrawLine(new Vector3(nodosCoords[n].x,0,nodosCoords[n].z),new Vector3(nodosCoords[m].x,0,nodosCoords[m].z));
                    }
                }
        }
    }
    public int ObtenAdyacencia(int pFila,int pColumna){
        return mAdyacencia[pFila,pColumna];
    }
    public void CalculatorIndegree(){
        int n=0;
        int m=0;
         for(n=0; n< cantNodos;n++)
                for(m=0;m<cantNodos;m++)
                {
                    if(mAdyacencia[m,n]==1)
                    {
                        indegree[n]++;
                    }
                }
    }
    public int EncuentraI0(){
        bool encontrado=false;
        int n=0;
        for(n=0; n< cantNodos;n++){
            if(indegree[n]==0){
             encontrado=true;
             break;
            }
            
        }
        if(encontrado)
            return n;
        else
            return -1;//no se ha encontado
    }
    public void DecrementaIndigree(int pNodo){
        indegree[pNodo]=-1;
        int n=0;
         for(n=0; n< cantNodos;n++){
            if(mAdyacencia[pNodo,n]==1)
                indegree[n]--;
         }
    }
    public void CalcularRuta(){
        //creamos la tabla
        //0 visitado 1 distancia 2 previo
        int[,] tabla=new int[cantNodos,3];
        int n=0;
        int distancia=0;
        int m=0;
        //inicializamos la tabla
        for(n=0; n< cantNodos;n++){
            tabla[n,0]=0;
            tabla[n,1]=int.MaxValue;
            tabla[n,2]=0;
        }
      tabla[inicio,1]=0;  
      for(distancia=0;distancia<cantNodos;distancia++){
         for(n=0; n< cantNodos;n++){
            if((tabla[n,0]==0)&&(tabla[n,1]==distancia)){
                tabla[n,0]=1;
                for(m=0;m<cantNodos;m++){
                    //verificamos que el nodo sea adyacente
                    if(ObtenAdyacencia(n,m)==1){
                        if(tabla[m,1]==int.MaxValue){
                            tabla[m,1]=distancia+1;
                            tabla[m,2]=n;
                        }
                    }
                }
            }
         }
      }
      ruta.Clear();
      int nodo=final;
      while(nodo!=inicio){
        ruta.Add(nodo);
        nodo= tabla[nodo,2];
      }
      ruta.Add(inicio);
      ruta.Reverse();
    }
}
