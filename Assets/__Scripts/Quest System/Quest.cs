using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest //it has the information of all previous and future quests.
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();//list of current Quest
    List<QuestEvent> pathList = new List<QuestEvent>();// list of the upcoming Quest

    public Quest() { }

    public QuestEvent AddQuestEvent(string n, string d,GameObject l)
    {
        QuestEvent questEvent = new QuestEvent(n, d,l);
        questEvents.Add(questEvent);
        return questEvent;
    }//this method will first create a quest event with the given name,decriptio and id.
     //Then store the newly created quest into the questEvents list   
     //Finally, return the newly created quest to the manager


    public void AddPath(string fromQuestEvent, string toQuestEvent)
    {
        QuestEvent from = FindQuestEvent(fromQuestEvent);
        QuestEvent to = FindQuestEvent(toQuestEvent);

        if (from != null && to != null)
        {
            QuestPath p = new QuestPath(from, to);//creatinga new path for the current event.
            from.pathlist.Add(p); // store the upcoming path for the current Quest
        }
    }

    QuestEvent FindQuestEvent(string id)//finding appointed quest event with specific id
    {
        foreach (QuestEvent n in questEvents)
        {
            if (n.GetId() == id)
                return n;
        }
        return null;
    }

    public void BFS(string id, int orderNumber = 1)// breadth first search(search all reachable nodes first, then explore the furthur nodes)
    {
        QuestEvent thisEvent = FindQuestEvent(id);//find the certain Quest
        thisEvent.order = orderNumber;//then set the order of this quest as assigned orderNumber

        foreach (QuestPath e in thisEvent.pathlist)
        {
            if (e.endEvent.order == -1)// when the order of next event is -1, that means it is the first event
                BFS(e.endEvent.GetId(), orderNumber + 1);
        }
    }

    public void PrintPath()
    {
        foreach (QuestEvent n in questEvents)
        {
            Debug.Log(n.name+ " "+ n.order);
        }
    }
}

