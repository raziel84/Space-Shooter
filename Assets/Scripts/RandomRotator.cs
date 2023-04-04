using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble = 5f;
    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start () {
        //Modificamos la velocidad de giro creando un Vector3 normalizado

        //Este código obtiene un punto aleatorio dentro de un cubo con lados de longitud 2 y luego lo normaliza
        //Vector3 angularVelocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;

        //Este otro código obtiene un punto aleatorio dentro de una esfera de radio 1 no normalizado
        //Es decir, no obtendrá siempre la misma velocidad de giro
        //Vector3 angularVelocity = Random.insideUnitSphere;

        rb.angularVelocity = Random.insideUnitSphere * tumble;
	}	
	
}
