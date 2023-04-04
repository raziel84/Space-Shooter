using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    //Velocidad de evasión máxima
    public float dodge;
    //Valor máximo de variación entre frames
    public float smoothing;
    //x tiempo min e y tiempo max
    public Vector2 startWait;
    //x tiempo min e y tiempo max
    public Vector2 maneuverTime;
    //x tiempo min e y tiempo max
    public Vector2 maneuverWait;
    //Está clase está definida dentro del PlayerController
    public Boundary boundary;
    //tilt = inclinación
    public float tilt = 4f;

    private float targetManeuver;
    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start () {
        UpdateBoundary();
        StartCoroutine(Evade());

    }


    private void UpdateBoundary() {

        //El método devuelve un Vector2 en unidades del mundo que indican el ancho y alto
        //Total de la pantalla. Para nosotros definir los límites nos interesa solo la mitad
        Vector2 half = Camera.main.GetDimensionsInWordUnits() / 2;
        //Utils.GetDimensionsInWordUnits() / 2;

        //0.9 es la mitad del ancho de la nave enemiga. Si no lo contemplamos la nave se sale de pantalla.
        //Para calcular tomamos nota de los valores min y max manualmente
        //Luego observamos con un Debug.Log(half) los valores que devuelve el método UpdateBoundary
        //La diferencia entre estos y los valores colocados manualmente es el limite que hay que sumar

        //-6
        boundary.xMin = -half.x + 0.9f;
        //6
        boundary.xMax = half.x - 0.9f;
        //-4
        boundary.zMin = 0f;
        //8
        boundary.zMax = 0f;

    }
    IEnumerator Evade() {

        //Tiempo inicial de espera para iniciar la maniobra
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true) {            
            //Velocidad de evasión aleaoria
            //Mathf.Sign devuelve +1 o -1
            //En este caso negamos el resultado porque queremos ir hacia el lado contrario
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            //Tiempo de duración de la maniobra
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0f;
            //Tiempo de espera entre maniobras
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	void FixedUpdate () {

        //Esta linea es necesaria para no aplicar un cambio brusco de dirección
        //MoveTowards aumenta un valor continuamente hasta un valor objetivo
        //Teniendo en cuenta un valor máximo de aumento por cada frame
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0f, rb.velocity.z);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0f, rb.position.z);
        //Rotamos al enemigo cuando se mueve
        rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
    }
}
