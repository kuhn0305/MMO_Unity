using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성을 보장함
    // 유일한 매니저를 가지고 온다.
    public static Managers Instance { get { return s_instance; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject obect = GameObject.Find("@Managers");

            if (obect == null)
            {
                obect = new GameObject { name = "@Managers" };
                obect.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obect);
            s_instance = obect.GetComponent<Managers>();
        }
    }
}
