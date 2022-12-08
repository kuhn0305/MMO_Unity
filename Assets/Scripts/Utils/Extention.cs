using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extention
{
    public static void AddUIEvent(this GameObject gameObject, Action<PointerEventData> action, Define.UIEvent type)
    {
        UI_Base.AddUIEvent(gameObject, action, type);
    }

}
