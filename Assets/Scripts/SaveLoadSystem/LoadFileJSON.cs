using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadFileJSON : MonoBehaviour
{
   static public T Load<T>()
    {
        if (File.Exists(SaveLoader.path))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(SaveLoader.path));
        }
        else return default(T);
    }
}
