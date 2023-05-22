using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BtnOnOff : MonoBehaviour
{
    Button myBtn;

    public Transform myTarget;
    // Start is called before the first frame update
    void Start()
    {
        myBtn = transform.GetComponent<Button>();
        myBtn.onClick.AddListener(() => ToggleBtn(myTarget));
    }

    public void ToggleBtn(Transform transform)
    {
        transform.gameObject.SetActive(!transform.gameObject.activeSelf);
    }


}
