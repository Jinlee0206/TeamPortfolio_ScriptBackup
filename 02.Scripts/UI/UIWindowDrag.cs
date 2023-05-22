using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIWindowDrag : MonoBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform orgParent
    {
        get; private set;
    }
    Vector2 mousePos = Vector2.zero;
    Vector2 distParent = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        mousePos = (Vector2)transform.position - eventData.position;
        orgParent = transform.parent.parent;
        distParent = orgParent.position - transform.position;
        //transform.SetParent(transform.parent.parent);
        //transform.SetAsLastSibling();
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.parent.parent.position = eventData.position + mousePos + distParent;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.SetParent(orgParent);
        //transform.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }
}
