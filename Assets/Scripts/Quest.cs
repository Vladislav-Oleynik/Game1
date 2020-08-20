using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : Interactable
{
    public string questName;
    public string questDescription;
    public bool isDone = false;

    public delegate void QuestAdded();
    public static event QuestAdded questAdded;


    public override void Interact()
    {
        base.Interact();
        AddQuest();
        gameObject.SetActive(false);
    }

    void AddQuest()
    {
        QuestsManager.instance.AddQuest(this);
        questAdded();
        Debug.Log("AddQuest");
    }

    public void QuestIsDone()
    {
        isDone = true;
    }
}
