using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//cambiar icomparable-> para encontrar el nodo mas bajo
public class Nodo : IComparable
{
    private int distancia = 0;
    private int heuristica = 0;
    private int puntaje = 0;
    private Nodo anterior = null;
    //corrdenadas de los nodos
    private int x;
    private int y;
    private int tipo;//0 espacio vacio 1 obstaculo 2 camino
    
    public int Distancia{ get {return distancia;}set{distancia=value;}}
    public int Heuristica{get {return heuristica;}set{heuristica=value;}}
    public int Puntaje{get{return x;}set{x=value;}}
    public int X{get{return x;}set {x=value;}}
    public int Y{get{return y;}set {y=value;}}
    internal Nodo Anterior{get{return anterior;}set{anterior=value;}}
    public int Tipo{get{return tipo;}set{tipo=value;}}

    //nodo objetivo para calcular la distancia manhatan y ver la distancia
    public Nodo(int pX,int pY,int pTipo,Nodo pObjetivo){
       x=pX;
       y=pY;
       tipo=pTipo;
       //calcula la heuristica con distancia Manhattan
       if(pObjetivo!=null)
            heuristica=Math.Abs(x-pObjetivo.X)+Math.Abs(y-pObjetivo.Y);

    }
    public void CalculaCosto(){
        if(anterior==null)
            distancia=0;
        else
        {
            distancia=anterior.Distancia+1;
        }
        puntaje=distancia+heuristica;
    }
    //comparacion
    int IComparable.CompareTo(object o){
        int r=0;
        Nodo temp=(Nodo)o;
        if(puntaje >temp.Puntaje)
            r=1;
        if(puntaje<temp.Puntaje)
            r=-1;
        return r;
    }
    public override string ToString(){
        //cordenadas y el tipo
        return string.Format("({0},{1}),{2}",x,y,tipo);
    }

}
