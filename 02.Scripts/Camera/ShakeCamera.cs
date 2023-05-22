using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static ShakeCamera instance;                // ShakeCamera의 인스턴스 정적 변수로 생성
    public static ShakeCamera Instance => instance;     // Instance 프로퍼티에 람다식으로 instance 정적 변수 할당

    private float shakeTime;                            // 흔들리는 시간 설정
    private float shakeIntensity;                       // 흔들리는 강도 설정

    private SpringArm cameraController;          // CameraController 인스턴스 생성

    public ShakeCamera()
    {
        instance = this;                                // 인스턴스에 자기자신 할당
    }

    private void Awake()
    {
        cameraController = GetComponent<SpringArm>();    // CameraController 컴포넌트 참조
    }

    public void OnShakeCamera(float shakeTime = 0.1f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;

        StopCoroutine("ShakeByRotation");       // 기존에 실행된 ShakeByRotation 코루틴 중지 
        StartCoroutine("ShakeByRotation");      // ShakeByRotation 코루틴 함수 실행
    }

    // ShakeByRotation이 조금 더 화려해 보여서 채용
    private IEnumerator ShakeByRotation()
    {
        //cameraController.isOnShake = true;      // 카메라 컨트롤러의 isOnShake 부울 값 true 변경

        Vector3 startRotation = transform.eulerAngles;  // 초기 회전값 오일러각을 Vector3 형태로 startRotation에 저장

        float power = 5f;

        while (shakeTime > 0.0f)
        {
            float x = Random.Range(-1f, 1f); // 랜덤 x, y, z 값 생성
            float y = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);   // 오일러각 쿼터니언으로 변환 후 transform.rotation에 대입

            shakeTime -= Time.deltaTime;

            yield return null;  // shakeTime while문 빠져나가기 전까지 다음 Update()가 끝나면 yield return null 다음 구문 실행(while문 반복)
        }
        transform.rotation = Quaternion.Euler(startRotation);   // 회전값 원위치

        //cameraController.isOnShake = false;     // isOnShake false로 변경

    }

    /*private IEnumerator ShakeByPosition()
    {
     Vector3 startPosition = transform.position;
     while (shakeTime > 0.0f)
         {
         transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;
         shakeTime -= Time.deltaTime;
         yield return null;
         }
     transform.position = startPosition;
    }*/


}
