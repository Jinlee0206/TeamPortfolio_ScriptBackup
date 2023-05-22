using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public PlayableDirector myPD;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float time;
    

    void Start()
    {
        UIManager.instance.gameObject.SetActive(false);
        myPD = GetComponent<PlayableDirector>();
        myPD.initialTime = 0.0f;
        duration = (float)myPD.playableAsset.duration;
        time = (float)myPD.initialTime;

        SceneManager.activeSceneChanged += SetUpUIManager;

    }

    void Update()
    {
        time += Time.deltaTime;

        if(time > duration)
        {
            if(this.gameObject.scene.buildIndex != 3) SceneManager.LoadScene(2);
            else
            {
                GameManager.instance.OnUnLoadEndingScene();
            }
        }
    }

    void SetUpUIManager(Scene curScene, Scene nextScene)
    {
        GameManager.instance.myPlyaer = FindObjectOfType<PlayerController>();
        GameManager.instance.myEnemy = FindObjectOfType<Monster>();

        if (nextScene.buildIndex == 2)
        {
            UIManager.instance.gameObject.SetActive(true);
        }
        else
        {
            UIManager.instance.gameObject.SetActive(false);
        }
    }


}
