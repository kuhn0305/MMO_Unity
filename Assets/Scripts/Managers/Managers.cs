using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성을 보장함
    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
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
