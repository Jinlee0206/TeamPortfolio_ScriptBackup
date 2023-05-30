using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    void Awake()
    {
        base.Initialize();
    }

    public int curLoading;
    // 맵 로딩 관련 -> Loading Scene을 거칠 때 사용
    public void ChangeMap(int destMap)
    {
        curLoading = destMap;
        UIManager.instance.gameObject.SetActive(false);
        Gamemanager.inst.gameObject.SetActive(false);
        StartCoroutine(MapLoading(destMap));
    }

    IEnumerator MapLoading(int destMap)
    {
        yield return SceneManager.LoadSceneAsync(1); // Loads the Scene asynchronously(비동기) in the background.
        AsyncOperation op = SceneManager.LoadSceneAsync(destMap); // 코루틴의 진행상태를 확인하는 방법
        op.allowSceneActivation = false; // Scene Loading 이 끝나면 바로 활성화 되게 하는 불 값 => false

        Slider slider = FindObjectOfType<Slider>();

        while (!op.isDone)
        {
            slider.value = op.progress / 0.9f;
            if (Mathf.Approximately(slider.value, 1.0f))
            {
                yield return new WaitForSeconds(5.0f); // Debuging용 딜레이 추가  
                UIManager.instance.gameObject.SetActive(true);
                Gamemanager.inst.gameObject.SetActive(true);
                op.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(2.0f); //   
        }
    }

    // Encounter CutScene 일 때
    public void SceneLoad(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        yield return SceneManager.LoadSceneAsync(sceneIndex);
    }

    // Play-In CutScene 관 련
    public void SceneLoadAdditive(int sceneIndex)
    {
        StartCoroutine(LoadingAdditive(sceneIndex));
    }

    IEnumerator LoadingAdditive(int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    public void SceneUnload(int sceneIndex)
    {
        StartCoroutine(Unloading(sceneIndex));
    }

    public IEnumerator Unloading(int sceneIndex)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneIndex);

        while (!unloadOperation.isDone)
        {
            
            yield return null;
        }
    }




}
