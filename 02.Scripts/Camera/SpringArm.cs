using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct ZoomData
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float curDist;
    public float desireDist;
    public ZoomData(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1, 2);
        ZoomLerpSpeed = 3.0f;
        curDist = 0.0f;
        desireDist = 0.0f;
    }
}

public class SpringArm : MonoBehaviour
{
    public Transform cameraPoint;
    public LayerMask crashMask;
    [SerializeField] float Offset = 0.5f;
    [SerializeField] float RotSpeed = 180.0f;
    Transform myCam = null;
    public ZoomData myZoomData = new ZoomData(3.0f);
    Vector3 curRot = Vector3.zero;
    public Vector3 CurRot 
    {
        get => curRot; 
        set => curRot = value;
    }
    [SerializeField] Vector2 LookUpRange = new Vector2(-60, 90);

    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        cameraPoint = myCam.transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.localPosition.magnitude;
    }

    [field: SerializeField] public bool toggleCameraRotation { get; set; }
    void Update()
    {
        if (!CameraChange)
        {
            CameraUpdate();
        }
       
    }
    
    void CameraUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
            curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(curRot.x, 0, 0);
        }

        if (!toggleCameraRotation)
        {
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, 0, 0), Time.deltaTime * 10.0f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }

        myZoomData.desireDist = Mathf.Clamp(myZoomData.desireDist + Input.GetAxis("Mouse ScrollWheel") * myZoomData.ZoomSpeed, myZoomData.ZoomRange.x, myZoomData.ZoomRange.y);

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, myZoomData.curDist + Offset, crashMask))
        {
            myZoomData.curDist = hit.distance - Offset;
        }
        else
        {
            myZoomData.curDist = Mathf.Lerp(myZoomData.curDist, myZoomData.desireDist, Time.deltaTime * myZoomData.ZoomLerpSpeed);
        }

        myCam.localPosition = Vector3.back * myZoomData.curDist;
    }

    public bool CameraChange = false;
    public void ViewPointTransformation(Transform ViewPoint, UnityAction e = null)
    {
        myCam.transform.SetParent(null);
        ChangeViewPoint(ViewPoint,e);
        CameraChange = true;
    }

    public void ViewPointReset(Transform Parent)
    {
        myCam.transform.SetParent(Parent);
        ResetSetting();
    }

    public void ResetSetting()
    {
        StopAllCoroutines();
        StartCoroutine(Resetting(myZoomData.curDist));
    }

    IEnumerator Resetting(float dist)
    {           
        myCam.localRotation=Quaternion.identity;
        while (Mathf.Abs(myCam.localPosition.x) > 0.01f || Mathf.Abs(myCam.localPosition.y) > 0.01f)
        {            
            myCam.localPosition = Vector3.Lerp(myCam.localPosition, new Vector3(0,0,-dist), Time.deltaTime * myZoomData.ZoomLerpSpeed);            
            yield return null;
        }
        myZoomData.desireDist = myZoomData.curDist;
        CameraChange = false;
    }


    public void ChangeViewPoint(Transform ViewPoint, UnityAction e = null) //코루틴을 실행시키는 함수
    {
        StartCoroutine(ChangingViewPoint(ViewPoint,e));
    }

    IEnumerator ChangingViewPoint(Transform ViewPoint, UnityAction e =null) //카메라의 시점을 전환시켜주는 코루틴, ViewPoint를 받아 ViewPoint로 옮겨서 이동함.
    { 
        while (!Mathf.Approximately(myCam.localPosition.x, ViewPoint.position.x) && (!Mathf.Approximately(myCam.localPosition.y, ViewPoint.position.y) && !Mathf.Approximately(myCam.localPosition.z, ViewPoint.position.z)))
        {
            float delta = myZoomData.ZoomLerpSpeed * Time.deltaTime;
            myCam.transform.position = Vector3.Lerp(myCam.transform.position, ViewPoint.position, Time.deltaTime * 7.0f); //위치, 회전 보간을 통해 움직임
            myCam.transform.rotation = Quaternion.Slerp(myCam.transform.rotation, Quaternion.Euler(ViewPoint.rotation.eulerAngles), 7.0f * Time.deltaTime);
            yield return null;
        }
        e?.Invoke();
    }
}
