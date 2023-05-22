using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 처리를 위한 네임스페이스 사용을 하겠다는 지시문 선언

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ConvertingNextScene());
    }

    void Update()
    {
        
    }

    IEnumerator ConvertingNextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(1);  // Unity는 one-source-multi-flatform tool
    }
}
