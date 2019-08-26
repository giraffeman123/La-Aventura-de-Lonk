using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerInGameLevel2 : MonoBehaviour {

    public GameObject esqueleto;
    public GameObject arbolOjoLoco;
    public GameObject rayo;
    public Canvas canvas;
    public Text life;
    public static int currentNumberOfEsqueletos;
    private Vector3 newPos;
    private Animator arbolAnimator;
    private int[] rayoXPos = { 0, -6, 7, -3, 5 ,2};
    private int[] esqueletoXPos = {-6,-5,-3,-1,0,1,2,3,5,7};
    private int[] esqueletoYPos = { -5,-3,-1 };
    private int cantidadRayos = 1;
    private int vectorY = 0; // Mover posicion Y de los esqueletos
    private int NUMBER_OF_ESQUELETOS = 10;
    private bool hasMethodStarted = false;
    private bool isArbolOjoLocoActive = false;
    private bool isRayoLaserAnimationActive = false;
    private bool waitActive = false;
    private bool isPaused = false;

    void Start()
    {
        initiateAllEsqueletos();
    }

    // Update is called once per frame
    void Update()
    {
        lifeStatus();
        checkNumberOfEsqueletos();
        initiateArbolOjoLoco();
        if (PlayerManager.currentLife <= 0) { LoadScene("You Died"); }
        if (Input.GetKeyDown(KeyCode.P) && canvas.enabled == false) { pauseGame(); }
        if (!waitActive && isRayoLaserAnimationActive) { StartCoroutine(Wait()); }
        if (ArbolOjoLoco.currentLife == 0 && isArbolOjoLocoActive) { LoadScene("You Win"); }
    }

    public void LoadScene(string name)
    {
        Debug.Log("Loading Scene: " + name);
        SceneManager.LoadScene(name);
    }

    void initiateAllEsqueletos()
    {
        if (!hasMethodStarted)
        {
            if (!isArbolOjoLocoActive)
            {
                currentNumberOfEsqueletos = NUMBER_OF_ESQUELETOS;
                foreach (int x in esqueletoXPos)
                {
                    newPos = new Vector3(x, -1, 0);
                    Vector3 esqueletoPos = newPos;
                    GameObject skeleton = Instantiate(esqueleto, esqueletoPos, Quaternion.identity) as GameObject;
                }
            }
            else
            {
                foreach (int x in esqueletoXPos)
                {
                    newPos = new Vector3(x, esqueletoYPos[vectorY], 0);
                    Vector3 esqueletoPos = newPos;
                    GameObject skeleton = Instantiate(esqueleto, esqueletoPos, Quaternion.identity) as GameObject;
                    currentNumberOfEsqueletos++;
                }
                vectorY++;
                hasMethodStarted = true;
            }
        }
    }

    void initiateArbolOjoLoco()
    {
        if(currentNumberOfEsqueletos == 0 && !isArbolOjoLocoActive)
        {
            initiateAllEsqueletos();
            newPos = new Vector3(0, 2, 0);
            Vector3 arbolOjoLocoPos = newPos;
            GameObject arbol = Instantiate(arbolOjoLoco, arbolOjoLocoPos, Quaternion.identity) as GameObject;
            arbolAnimator = arbol.gameObject.GetComponent<Animator>();
            isArbolOjoLocoActive = true;
        }
    }

    IEnumerator Wait()
    {
        waitActive = true;
        yield return new WaitForSeconds(1.2f);
        // Crea rayos laser y espera 1 segundo 
        for (int x = 0; x < cantidadRayos; x++)
        {
            newPos = new Vector3(rayoXPos[x], -2, 0);
            Vector3 rayoPos = newPos;
            GameObject rasho = Instantiate(rayo, rayoPos, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(0.3f);
        // Destruye los rayos laser's creados y aumenta la cantidad de los proximos rayos laser's
        GameObject[] rashos = GameObject.FindGameObjectsWithTag("Rayo");
        for (int i = 0; i < rashos.Length; i++)
            Destroy(rashos[i]);
        cantidadRayos++;

        hasMethodStarted = false;
        isRayoLaserAnimationActive = false;
        waitActive = false;
    }

    void lifeStatus()
    {
        life.text = "Life: " + PlayerManager.currentLife;
    }

    void checkNumberOfEsqueletos()
    {
        if (isArbolOjoLocoActive)
        {
            GameObject[] esqueletos = GameObject.FindGameObjectsWithTag("Esqueleto");
            currentNumberOfEsqueletos = esqueletos.Length;
            //Debug.Log("currentNumberOfEsqueletos(arbolojoloco)" + currentNumberOfEsqueletos);

            if (arbolAnimator.GetCurrentAnimatorStateInfo(0).IsName("Arbol Ojo Loco_OjoCerrado") && !hasMethodStarted) {
                initiateAllEsqueletos();
            }
            else if (!ArbolOjoLoco.waitActive && arbolAnimator.GetCurrentAnimatorStateInfo(0).IsName("Arbol Ojo Loco_RashoLaser")) {
                isRayoLaserAnimationActive = true;
            }
        }
        else
        {
            GameObject[] esqueletos = GameObject.FindGameObjectsWithTag("Esqueleto");
            currentNumberOfEsqueletos = esqueletos.Length;
            //Debug.Log("currentNumberOfEsqueletos" + currentNumberOfEsqueletos);
        }
    }

    void pauseGame()
    {
        canvas.enabled = true;
        isPaused = true;
        Time.timeScale = 0; // SLOW-MOTION EFFECT
    }

    public void resumeGame()
    {
        if(canvas.enabled == true && isPaused == true) {
            canvas.enabled = false;
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void returnToMenu()
    {
        if (canvas.enabled == true && isPaused == true)
        {
            canvas.enabled = false;
            isPaused = false;
            Time.timeScale = 1;
            Debug.Log("Loading Scene: " + "Menu");
            SceneManager.LoadScene("Menu");
        }
    }
}
