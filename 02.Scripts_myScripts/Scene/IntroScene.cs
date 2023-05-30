using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관련 처리를 위한 네임스페이스 사용을 하겠다는 지시문 선언

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.activeSceneChanged += SetUI;
        StartCoroutine(ConvertingToLoadingScene());
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }

    IEnumerator ConvertingToLoadingScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(7);  // Unity는 one-source-multi-flatform tool
                                    // Loading Scene 은 1번 인덱스
    }

    void SetUI(Scene preScene, Scene postScene)
    {
        if(postScene.isLoaded)
        {
            UIManager.instance.gameObject.SetActive(true);
            UIManager.instance.Refresh();
            
        }
        Destroy(this.gameObject);
    }

}
