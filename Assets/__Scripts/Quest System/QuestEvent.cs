using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvent
{
    public enum EventStatus {WAITING, CURRENT, DONE };
    //waiting- not yet finished but can't be worked on casue of a prerequisiite event
    //current- the quest taht player is working on
    //done- the quest that is finished

    public string name;// the name of the quest
    public string description;//the decription of the quest
    public string id;//unique identifier of the quest
    public int order = -1; // d emonstrate the order of the current Quest
    public EventStatus status;//keep track of the statu of the quest whether it is WAITING, CURRENT or DONE
    public QuestButton button; 

    public List<QuestPath> pathlist = new List<QuestPath>();  
    //the list keep track of what quest is coming next.
    //if the previous quest is DONE, tell player what is the next WAITING quest
    
    public QuestEvent(string n , string d)
    {
        id = Guid.NewGuid().ToString();// this code will generate a unique sequence of identity for the id of the quest(000111-1000-1113..)
        name = n;
        description = d;
        status = EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es)
    {
        status = es;
        button.UpdateButton(es); 
    }
    //this method will update the quest status.



    public String GetId()
    {
        return id;
    }
    //get the id of a quest
}
