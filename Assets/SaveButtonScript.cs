using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour
{
    public GameObject attentionPanel;
    public InputField inputField;
    public static string savePath;
    
    public void SaveGame()
    {
        if (CheckSaveName())
        {
            savePath = Application.dataPath + '\\' + inputField.text + ".json";
            DataPath.instance.Save();
        }
    }

    public bool CheckSaveName()
    {
        if (inputField.text == "NewGameSave" || inputField.text == " " || inputField.text == "")
        {
            attentionPanel.SetActive(true);
            return false;
        }
        else
            return true;
    }
}
