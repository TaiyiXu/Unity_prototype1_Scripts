using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox;
    public GameObject buttonPrefab;

    public GameObject quest1;
    public GameObject quest2;

    void Start()
    {
        QuestEvent a = quest.AddQuestEvent("quest 1", "Find goods", quest1);
        QuestEvent b = quest.AddQuestEvent("quest 2", "Escape", quest2);

        quest.AddPath(a.GetId(), b.GetId()); 


        quest.BFS(a.GetId());

        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        quest1.GetComponent<QuestLocation>().Setup(this, a, button);
        button = CreateButton(b).GetComponent<QuestButton>();
        quest2.GetComponent<QuestLocation>().Setup(this, b, button);
 
        quest.PrintPath();
    }
    
    GameObject CreateButton(QuestEvent e)
    {
        GameObject b = Instantiate(buttonPrefab);
        b.GetComponent<QuestButton>().Setup(e, questPrintBox);
        if (e.order == 1)
        {
            b.GetComponent<QuestButton>().UpdateButton(QuestEvent.EventStatus.CURRENT);
            e.status = QuestEvent.EventStatus.CURRENT;
        }
        return b; 
    }

    public void UpdateQuestOnCompletion(QuestEvent e)
    {
        foreach(QuestEvent n in quest.questEvents)
        {
            if (n.order == (e.order + 1))// if the event order is the next quest for the current quest
            {
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);//set the next quest statu to CURRENT in order for player to prograss.
                quest2.SetActive(gameObject);
            }
        }
    }
}
