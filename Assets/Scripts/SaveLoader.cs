using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoader : MonoBehaviour
{
   
    public Dropdown dropdown;
    private string[] paths;
    public static string path;
    private string[] saveNames;
    

    void Awake()
    {
        Debug.Log("SaveLoader Awake");
        path = Directory.GetFiles(Application.dataPath, "NewGameSave.json")[0];
    }

    void Start()
    {
        Debug.Log("SaveLoader Start");
        paths = Directory.GetFiles(Application.dataPath, "*.json");
        saveNames = new string[paths.Length];
        for(int i = 0; i < saveNames.Length; i++)
        {
            saveNames[i] = paths[i].Substring(paths[i].IndexOf('\\') + 1);
            saveNames[i] = saveNames[i].Substring(0, saveNames[i].LastIndexOf('.'));
        }
        
        Debug.Log(saveNames[0] + saveNames[1] + saveNames[2]);
        Debug.Log(Directory.GetFiles(Application.dataPath, "*.json")[0]);
        foreach (string option in saveNames)
        {
            dropdown.options.Add(new Dropdown.OptionData(option));
        }
        dropdown.value = 1;
        dropdown.value = 0;
    }

    public void LoadPath()
    {
        path = paths[dropdown.value];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateLoadPath()
    {
        path = paths[dropdown.value];
    }
}
