using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 1, columnCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        UpdateQuestUI();
        Quest.questAdded += UpdateQuestUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateQuestUI()
    {
        if(QuestsManager.instance.quests.Count != 0)
            itemCount = QuestsManager.instance.quests.Count;

        Debug.Log("UpdateQuestUI()");
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item

        float width = containerRectTransform.rect.width / columnCount;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        int j = 0;
        for (int i = 0; i < itemCount; i++)
        {
            if (i % columnCount == 0)
                j++;

            //create a new item, name it, and set the parent

            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = gameObject.name + " item at (" + i + "," + j + ")";

            if (QuestsManager.instance.quests.Count > 0)
            {
                newItem.transform.GetChild(0).GetComponentInChildren<Text>().text = QuestsManager.instance.quests[i].questName;
                newItem.transform.GetChild(1).GetComponentInChildren<Text>().text = QuestsManager.instance.quests[i].questDescription;
            }

            //newItem.transform.parent = gameObject.transform;
            newItem.transform.SetParent(gameObject.transform);

            //move and size the new item

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2 - height * j;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }
}