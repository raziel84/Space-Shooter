using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HelpURL("http://www.google.es/search?q=hagamos+videojuegos")]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class VariablesInspector : MonoBehaviour {

    [Header("Salud")]
    [Range(1, 5)]
    [Tooltip("Vidas del jugador.\nEntre 1 y 5.")]
    public int vidas = 3;
    [Range(1, 100)]
    public int energia = 80;
    [HideInInspector]
    public int maxEnergia = 100;

    [Header("Otros")]
    [SerializeField]
    private bool tieneEspada = false;
    [Space(10)]
    [Multiline(3)]
    //[TextArea(3,6)]
    [Tooltip("Mensaje de saludo del personaje")]
    public string saludo = @"¡Hola Jugador!
    Una larga aventura nos espera.
    Espero salir con vida de ella.";

    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;

    void Awake () {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
	
}
