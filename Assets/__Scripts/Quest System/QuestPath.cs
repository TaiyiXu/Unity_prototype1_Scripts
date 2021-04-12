using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath 
{
    public QuestEvent startEvent;
    public QuestEvent endEvent;

    public QuestPath(QuestEvent from, QuestEvent to)
    {
        startEvent = from;
        endEvent = to;
    }
    //keeps the quest as nodes
    //there will be starting points and end points
    //a series of quest starting from the beginning of the game 
    //and end at the end of the game
}
