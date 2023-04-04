using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    //Utilizamos este script para mover el rayo
    //En realidad estamos moviendo un gameobject en una direccion a una determinada velocidad
    //Es decir, que podemos usarlo para mover otras cosas, por ejemplo el asteroide
    private Rigidbody rb;
    public float speedBolt = 20f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start () {
        rb.velocity = transform.forward * speedBolt;
	}
		
}
