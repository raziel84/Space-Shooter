using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    //hazard = peligro;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    private Vector3 spawnPosition;
    private int hazardCount = 10;
    private float spawnWait = 0.5f;
    private float startWait = 1f;
    private float waveWait = 2f;

    private int score;
    public Text scoreText;

    public GameObject restartGameObject;
    private bool restart;
    public GameObject gameOverGameObject;
    private bool gameOver;

    // Use this for initialization
    void Start () {
        UpdateSpawnValues();
        score = 0;
        //Las corutinas no pueden llamarse como si llamaramos a un método
        StartCoroutine(SpawnWaves());
        restart = false;
        restartGameObject.SetActive(false);
        gameOver = false;
        gameOverGameObject.SetActive(false);
	}

    private void UpdateSpawnValues() {

        //Para más detalles ver PlayerController
        Vector2 half = Camera.main.GetDimensionsInWordUnits() / 2;
            //Utils.GetDimensionsInWordUnits() / 2;
        spawnValues = new Vector3(half.x - 0.7f, 0f, half.y + 6f);

    }

    void Update() {

        //Para debug utilizamos la tecla R para reiniciar eljuego cuando perdemos
        if (restart && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //IEnumerator transforma un método en una corutina
    IEnumerator SpawnWaves () {

        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                //6 y -6 en x son los límites para que el asteroide no salga de la pantaalla
                //Por el contrario, el 16 en z es para que se instancie fuera de pantalla
                //spawnPosition = new Vector3(Random.Range(6, -6), 0, 16);
                spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //Random.Range elije un número tomando el primer limite e ignorando el segundo
                //En este caso el rango es entre 0 y la cantidad de elementos del array (3)
                //Por lo tanto podrá tomar alguno de los siguientes valores: 0, 1 y 2
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, Quaternion.identity);
                //Las corutinas deben incluir una o más instrucciones yield
                //En esta instrucciones debemos indicar cto tiempo esperar antes de continuar
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            //Chequeamos si el jugador a muerto
            //Si es el caso mostramos el mensaje de restart y rompemos la corutina
            if (gameOver)
            {
                restartGameObject.gameObject.SetActive(true);
                restart = true;
                break;
            }
        }
        
	}

    public void AddScore(int value) {
        score += value;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public void GameOver() {
        gameOverGameObject.gameObject.SetActive(true);
        gameOver = true;
    }
}
