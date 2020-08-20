using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveFileJSON : MonoBehaviour
{
    static public void Save(object obj)
    {
        File.WriteAllText(SaveButtonScript.savePath, JsonUtility.ToJson(obj));
    }
}
