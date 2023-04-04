using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;

    //Tiempo de espera antes de disparar
    public float delay;
    //Tiempo entre disparos
    public float fireRate;

    public AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        //Invoca un método repetidamente
        InvokeRepeating("Fire", delay, fireRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fire() {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();

    }
}
