using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LoadScene(string name)
    {
        Debug.Log("Loading Scene: "+name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("I wanna quit");
        Application.Quit(); // <---------- No esta recomendado terminar la aplicacion
        // con la clase Application y el metodo quit. Usualmente se le debe reserva esa 
        // opcion al sistema operativo para hacerlo. 
        // En android no se dejara subir a la appstore una app que use el metodo.
    }
}
