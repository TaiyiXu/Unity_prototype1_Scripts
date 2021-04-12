using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    public QuestManager qManager;
    public QuestEvent qEvent;
    public QuestButton qButton;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;// if the quest object does not hit player, return

        if (qEvent.status != QuestEvent.EventStatus.CURRENT) return;// if the quest is not the current quest that we are working on, return

        qEvent.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        qButton.UpdateButton(QuestEvent.EventStatus.DONE);
        qManager.UpdateQuestOnCompletion(qEvent);
    }

    public void Setup(QuestManager qm, QuestEvent qe, QuestButton qb)
    {
        qManager = qm;
        qEvent = qe;
        qButton = qb;

        qe.button = qButton;
    }
}
