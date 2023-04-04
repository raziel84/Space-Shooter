using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

//Unity muestra en el inspector los tipos de datos serializables. 
//Para poder ver las variables de la clase Boundary (en español Limites) se debe indicar que es serializable
[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    //Se pueden utilizar cabeceras para agrupar variables en el inspector
    //Se deben colocar siempre antes de una variable pública.
    [Header("Movement")]    
    public float speed = 10f;
    public float tilt = 4f; //tilt = inclinación
    public Boundary boundary;
    private Rigidbody rb;

    [Header("Shooting")]
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.25f; //Equivale a cuatro disparos por segundo
    private float nextFire;

    private AudioSource audioSource;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        UpdateBoundary();
    }

    private void UpdateBoundary() {

        //El método devuelve un Vector2 en unidades del mundo que indican el ancho y alto
        //Total de la pantalla. Para nosotros definir los límites nos interesa solo la mitad
        Vector2 half = Camera.main.GetDimensionsInWordUnits() / 2;
            //Utils.GetDimensionsInWordUnits() / 2;

        //0.7 es la mitad del ancho de la nave. Si no lo contemplamos la nave se sale de pantalla.
        //Para calcular tomamos nota de los valores min y max manualmente
        //Luego observamos con un Debug.Log(half) los valores que devuelve el método UpdateBoundary
        //La diferencia entre estos y los valores colocados manualmente es el limite que hay que sumar

        //-6
        boundary.xMin = -half.x + 0.7f;
        //6
        boundary.xMax = half.x - 0.7f;
        //-4
        boundary.zMin = -half.y + 6f;
        //8
        boundary.zMax = half.y - 2f;

    }

    void Update() {
        //Controlamos si se pulso el boton de disparo
        //Tambien controlamos si el tiempo actual es mayor al tiempo del próximo disparo
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
        {
            //Actualizamos el tiempo para el proximo disparo sumando al tiempo actual
            //el tiempo entre disparos
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity);
            audioSource.Play();

        }
    }

    void FixedUpdate () {
        //Reemplazamos Input por CrossPlatformInputManager para utilizar el joysticky botones virtuales
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        //La nave se mueve en dos ejes. X y Z
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        //Le damos como velocidad al rigidbody la cantidad de movimiento en cada eje
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);

    }
}
