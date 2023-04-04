using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;
    private Vector3 startPosition;
    //OJO en este caso coincide porque el Quad inicial tenía una escala de 1.
    //Lo normal es que esto no suceda y se deba calcular de otra manera (?)
    private float tileSize;

    void Start () {
        startPosition = transform.position;
        tileSize = transform.localScale.y;
	}
	
	void Update () {
        //En este caso en el primer parametro tomamos el tiempo en segundos y lo multiplicamos por la velocidad
        //Si no lo hicieramos se movería a razon de 1 s
        //A medida que pasa el tiempo el resultado de esa multiplicación se va incrementando
        //Cuando llega al valor limite (tileSize) devuelve cero y empieza de nuevo
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSize);
        //Se aplica el vector forward porque el juego se mueve arriba en Z
        transform.position = startPosition + Vector3.forward * newPosition;
	}
}
