using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;


#region 벡터 구현
// 1. 위치 벡터
// 2. 방향 벡터
struct MyVector
{
    public float x;
    public float y;
    public float z;

    //                 +
    //        +       +
    //  +-----------+

    // 방향의 전체 크기
    public float Magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
    // 크기가 1 짜리인 '단위 벡터'
    public MyVector Normalized { get { return new MyVector(x / Magnitude, y / Magnitude, z / Magnitude); } }

    public MyVector(float x, float y, float z)
    { this.x = x; this.y = y; this.z = z; }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static MyVector operator *(MyVector a, float b)
    {
        return new MyVector(a.x * b, a.y * b, a.z * b);
    }
}
#endregion


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }


    //float _yAngle = 0.0f;
    void Update()
    {
        //_yAngle += Time.deltaTime * 100.0f;
        // 절대 회전값
        //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        // +- delta
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // Local -> World :: TransformDirection
        // World -> Local :: InverseTransformDirection


        if (_moveToDest)
        {
            if (_moveToDest)
            {
                Vector3 dir = _destPos - transform.position;

                if (dir.magnitude < 0.0001f)
                    _moveToDest = false;
                else
                {
                    float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

                    transform.position += dir.normalized * moveDist;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
                }
            }
        }
    }



    private void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // 1. transform.rotation =  Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);

            // 1. transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // 1. transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);

            // 1. transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // 1. transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);

            // 1. transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            // 1. transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);

            // 1. transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;

        /*
         * 
         * Translate는 local 좌표 기준으로 움직임을 적용한다.
         * 
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        */
    }

    private void OnMouseClicked(Define.MouseEvent mouseEvent)
    {
        if (mouseEvent != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            _destPos = hit.point;
            _moveToDest = true;

            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }

    }
}
