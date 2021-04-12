using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // allow player to access all the buttons

public class QuestButton : MonoBehaviour
{
    public Button buttonComponent;
    public RawImage icon;
    public Text eventName;
    public Sprite currentImage;
    public Sprite waitingImage;
    public Sprite doneImage;
    public QuestEvent thisEvent;
    //each event will have three status which are CURRENT, WAITING and DONE

    QuestEvent.EventStatus status;

    public void Setup(QuestEvent e, GameObject scrollList)// initally, a button will be not interactable and set as WAITING statu. 
    {
        thisEvent = e;
        buttonComponent.transform.SetParent(scrollList.transform, false);
        eventName.text = "<b>" + thisEvent.name + "</b>\n" + thisEvent.description;
        status = thisEvent.status;
        icon.texture = waitingImage.texture;
        buttonComponent.interactable = false;
    }

    public void UpdateButton(QuestEvent.EventStatus s)
    {
        status = s;
        if(status == QuestEvent.EventStatus.DONE)// if the event is completed, player can not access to the button
        {
            icon.texture = doneImage.texture;
            buttonComponent.interactable = false;
        }
        else if (status == QuestEvent.EventStatus.WAITING)// if the event statu is WAITING, the quest is not yet started and can not be accessed by the player
        {
            icon.texture = waitingImage.texture;
            buttonComponent.interactable = false;
        }
        else if (status == QuestEvent.EventStatus.CURRENT)// The quest is on prograss, and therefore it can be accessed by the player.
        {
            icon.texture = currentImage.texture;
            buttonComponent.interactable = true;
        }
    }
}
