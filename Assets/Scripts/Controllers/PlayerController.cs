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

    //bool _moveToDest = false;
    private Vector3 _destPos;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle
    }

    private PlayerState _state = PlayerState.Idle;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard;
        //Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }


    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();

                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    private void UpdateIdle()
    {
        // 애니메이션
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

    private void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;

        if (dir.magnitude < 0.0001f)
            _state = PlayerState.Idle;
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Speed", _speed);
    }

    private void UpdateDie()
    {

    }

    private void OnMouseClicked(Define.MouseEvent mouseEvent)
    {
        if (mouseEvent != Define.MouseEvent.Click)
            return;

        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }
    }

    //private void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        // 1. transform.rotation =  Quaternion.LookRotation(Vector3.forward);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);

    //        // 1. transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        // 1. transform.rotation = Quaternion.LookRotation(Vector3.back);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);

    //        // 1. transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        // 1. transform.rotation = Quaternion.LookRotation(Vector3.left);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);

    //        // 1. transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        // 1. transform.rotation = Quaternion.LookRotation(Vector3.right);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);

    //        // 1. transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }

    //    _moveToDest = false;

    //    /*
    //     * 
    //     * Translate는 local 좌표 기준으로 움직임을 적용한다.
    //     * 
    //    if (Input.GetKey(KeyCode.W))
    //        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    //    if (Input.GetKey(KeyCode.S))
    //        transform.Translate(Vector3.back * Time.deltaTime * _speed);
    //    if (Input.GetKey(KeyCode.A))
    //        transform.Translate(Vector3.left * Time.deltaTime * _speed);
    //    if (Input.GetKey(KeyCode.D))
    //        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    //    */
    //}
}
