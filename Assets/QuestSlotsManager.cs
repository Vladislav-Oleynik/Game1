using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlotsManager : MonoBehaviour
{
    [SerializeField] GameObject myPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = (GameObject)Instantiate(myPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
