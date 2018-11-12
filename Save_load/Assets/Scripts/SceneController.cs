using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static SceneController sceneControl;

    public float health;
    public float experience;

    private void Awake() {
        if(sceneControl == null) {
            DontDestroyOnLoad(gameObject);
            sceneControl = this;
        } else if(sceneControl != this) {
            Destroy(gameObject);
        }
    }

    public void NextScene() {
        if ((SceneManager.sceneCountInBuildSettings-1) > SceneManager.GetActiveScene().buildIndex) {
            print("loading " + (SceneManager.GetActiveScene().buildIndex + 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            print("This is the last scene");
        }        
    }

    public void PreviousScene() {
        if (SceneManager.GetActiveScene().buildIndex != 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            print("loading " + (SceneManager.GetActiveScene().buildIndex -1 ));
        } else {
            print("This is the first scene");
        }        
    }

    public void LoadSelectedScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        print("loading " + (sceneIndex));
    }

    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 56;
        GUI.Label(new Rect(10, 10, 180, 80), "Active scene index : "  + SceneManager.GetActiveScene().buildIndex, style);
    }  

}