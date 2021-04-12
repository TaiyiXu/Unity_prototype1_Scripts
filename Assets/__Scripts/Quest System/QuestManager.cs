using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest quest = new Quest();
    public GameObject questPrintBox;
    public GameObject buttonPrefab;

    public GameObject foodA;
    void Start()
    {
        QuestEvent a = quest.AddQuestEvent(" ", " ");
        QuestEvent b = quest.AddQuestEvent(" ", " ");
        QuestEvent c = quest.AddQuestEvent(" ", " ");
        QuestEvent d = quest.AddQuestEvent(" ", " ");
        QuestEvent e = quest.AddQuestEvent(" ", " ");

        quest.AddPath(a.GetId(), b.GetId()); 
        quest.AddPath(b.GetId(), c.GetId());
        quest.AddPath(c.GetId(), d.GetId());
        quest.AddPath(d.GetId(), e.GetId());

        quest.BFS(a.GetId());

        QuestButton button = CreateButton(a).GetComponent<QuestButton>();
        foodA.GetComponent<QuestLocation>().Setup(this, a, button);
        button = CreateButton(b).GetComponent<QuestButton>();
        button = CreateButton(c).GetComponent<QuestButton>();
        button = CreateButton(d).GetComponent<QuestButton>();
        button = CreateButton(e).GetComponent<QuestButton>();

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
            }
        }
    }
}
