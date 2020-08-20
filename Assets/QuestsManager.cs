using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    #region Singleton
    public static QuestsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There than one instance of QuestsManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject questsParent;
    public List<Quest> quests;
    public GameObject questPanel;
    public GameObject[] questSlots;
    public GameObject questWindow;

    

    void Start()
    {
        
    }

    public void AddQuest(Quest newQuest)
    {
        quests.Add(newQuest);
        //quests = new Quest[questsParent.transform.childCount];
        //for (int i = 0; i < questsParent.transform.childCount; i++)
        //{
        //    quests[i] = questsParent.transform.GetChild(i).GetComponent<Quest>();
        //}
    }

    //public void AddQuest()
    //{
    //    questSlots = new GameObject[questPanel.transform.childCount];
    //    for (int i = 0; i < questPanel.transform.childCount; i++)
    //    {
    //        questPanel.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = quests[i].questName + "\n" + quests[i].questDescription;
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questWindow.SetActive(!questWindow.activeSelf);
        }
    }
}
