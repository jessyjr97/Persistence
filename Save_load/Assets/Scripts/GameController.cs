using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameController : MonoBehaviour {
    public static GameController control;


    public int Attack;
    public int Defense;
    public int Health;


    private void Awake() {
        if (control == null) {
            DontDestroyOnLoad(gameObject);
            try
            {
                LoadGame();
            }
            catch
            {
                SetDefaultValue();
            }
            control = this;
        } else if (control != this) {
            Destroy(gameObject);
        }
    }

    private void SetDefaultValue()
    {
        Attack = 5;
        Defense = 2;
        Health = 10;
    }

    private void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 56;
        //print(Attack.ToString());
        GUI.Label(new Rect(10, 160, 100, 30), "Attack : " + Attack.ToString(), style);
        GUI.Label(new Rect(10, 110, 100, 30), "Defense : " + Defense.ToString(), style);
        GUI.Label(new Rect(10, 60, 100, 30), "Health : " + Health.ToString(), style);
       
    }

    public void IncreaseAttack() {
        Attack += 1;
    }

    public void IncreaseDefense() {
        Defense += 1;
    }

    public void IncreaseHealth() {
        Health += 10;
    }

    public void SaveGame()
    {
        FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Create);
        PlayerData data = new PlayerData();
        data.health = Health;
        data.attack = Attack;
        data.defense = Defense;
        data.scene = SceneController.sceneControl.GetSceneIndex();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            print("Failed");
            throw new Exception("Game file not found. You must recode the binary file manually via notepad.");
        }
        FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(file);
        Health = data.health;
        Attack = data.attack;
        Defense = data.defense;
        SceneController.sceneControl.LoadSelectedScene(data.scene);
        file.Close();
    }
}

[Serializable]
class PlayerData
{
    public int health;
    public int attack;
    public int defense;
    public int scene;
}