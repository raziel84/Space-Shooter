using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    private GameController gameController;
    public int scoreValue;

    void Start() {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Boundary") || collider.CompareTag("Enemy")) return;

        //Está comprobación es necesaria porque no queremos una explosion cuando el disparo del jugador
        //Entre en contacto con el disparo enemigo. Simplemente lo eliminara
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if (collider.CompareTag("Player"))
        {
            Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation);
            //Mostramos GameOver
            gameController.GameOver();
        }

        //Actualizamos puntuacion
        gameController.AddScore(scoreValue);

        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}
