using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GameObject gameObject =  GetImage((int)Images.ItemIcon).gameObject;
        UI_EventHandler evt = gameObject.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += (PointerEventData data) => { evt.gameObject.transform.position = data.position; };
        }

    private void PointEventData(PointerEventData obj)
    {
        throw new NotImplementedException();
    }

    public void OnButtonClicked()
    {

    }
}
