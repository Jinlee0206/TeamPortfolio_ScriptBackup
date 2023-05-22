using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool isOnShake;
    public Transform myTarget;
    Vector3 Dir = Vector3.zero;
    float Dist = 0.0f;
    float TargetDist = 0.0f;

    public float RotSpeed = 5.0f;
    public float ZoomSpeed = 30.0f;
    public LayerMask crashMask;

    public bool isPlayer=true; //플레이어에 붙어있을 때 true, 아니면 false
    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    private void Awake()
    {
        transform.LookAt(myTarget);
        rotX = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        Dir = transform.position - myTarget.position;
        TargetDist = Dist = Dir.magnitude;
        Dir.Normalize();
        Dir = transform.InverseTransformDirection(Dir);
    }
    void Update()
    {

        if (isPlayer) //플레이어에 붙어있을 때만 실행되도록.
        {
            if (Input.GetMouseButton(1))
            {
                float x = Input.GetAxis("Mouse Y") * RotSpeed;
                float y = -Input.GetAxis("Mouse X") * RotSpeed;

                rotX *= Quaternion.Euler(x, 0, 0);
                rotY *= Quaternion.Euler(0, y, 0);

                float angle = rotX.eulerAngles.x;
                if (angle > 180.0f) angle -= 360;
                angle = Mathf.Clamp(angle, -60, 80);
                rotX = Quaternion.Euler(angle, 0, 0);
            }
            TargetDist -= Input.GetAxis("Mouse ScrollWheel");
            TargetDist = Mathf.Clamp(TargetDist, 1.0f, 10.0f);

            Dist = Mathf.Lerp(Dist, TargetDist, Time.deltaTime * 3.0f);

            Vector3 dir = rotY * rotX * Dir;
            float radius = 0.5f;
            if (Physics.Raycast(new Ray(myTarget.position, dir), out RaycastHit hit, Dist + radius, crashMask))
            {
                Dist = hit.distance - radius;

            }

            transform.position = myTarget.position + dir * Dist;

            transform.LookAt(myTarget);
        }
       
    }

    public  void ChangeViewPoint(Transform ViewPoint) //코루틴을 실행시키는 함수
    {
        StartCoroutine(ChangingViewPoint(ViewPoint));
    }

    IEnumerator ChangingViewPoint(Transform ViewPoint) //카메라의 시점을 전환시켜주는 코루틴, ViewPoint를 받아 ViewPoint로 옮겨서 이동함.
    {
        Vector3 v = ViewPoint.position - transform.position; 
        float Dist = v.magnitude;

        while (Dist>0) 
        {
            float delta = RotSpeed * Time.deltaTime;
            if (Dist < delta) delta = Dist;
            transform.position = Vector3.Lerp(transform.position, ViewPoint.position, delta); //위치, 회전 보간을 통해 움직임
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(ViewPoint.rotation.eulerAngles), 10.0f * Time.deltaTime);
            Dist-= delta;
            yield return null;
        }
    }

    public void Camera_PlayerToOther(Transform convertPoint) //플레이어에서 다른 시점으로 옮기는 함수, 플레이어와의 부모 관계를 끊는다.
    {
        transform.SetParent(null);
        ChangeViewPoint(convertPoint);
        isPlayer = false;   //다른 시점으로 옮겼기 때문에 isPlayer를 false로
    }

    public void Camera_OtherToPlayer(Transform Parent) //다른 시점에서 다시 플레이어로 돌아오게 하는 함수. 부모 관계를 다시 연결해준다.
    {
        transform.SetParent(Parent);    
        isPlayer = true;    
        ResetRotate(); //초기화
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void ResetRotate() //다른 시점에서 캐릭터로 돌아올 때, Dist, Dir이 전의 수를 가지고 있기 때문에 문제가 발생해서 강제로 초기화 시키는 함수
    {
        rotX = Quaternion.identity;
        rotY = Quaternion.identity;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        Dist = 0.0f;
        TargetDist = 0.0f;
        Dir = transform.position - myTarget.position;
    }
}
