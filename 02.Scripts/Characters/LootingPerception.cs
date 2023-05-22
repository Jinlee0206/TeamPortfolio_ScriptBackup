using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingPerception : MonoBehaviour
{
    //public LayerMask enemyMask;
    IinterPlay myPlayer;
    Transform myTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] list = Physics.OverlapSphere(transform.parent.position,7.0f); 

        foreach (Collider col in list) 
        {
            if(col.GetComponent<IinterPlay>() != null)
            {
                myPlayer = col.GetComponent<IinterPlay>();
                myPlayer.SetisObjectNear(true);
                myPlayer.OpenUi.RemoveAllListeners();
                myPlayer.CloseUi.RemoveAllListeners();
                LootingManager.Inst.ListIsEnterUpdate(col.GetComponent<Monster>(), true);
                //myPlayer.OpenLoot += LootingManager.Inst.RefreshLootTarget?.Invoke(transform.parent.GetComponent<Monster>());
                myPlayer.OpenUi.AddListener(() => LootingManager.Inst.RefreshLootTarget(transform.parent.GetComponent<Monster>()));
                myPlayer.CloseUi.AddListener(() => LootingManager.Inst.RemoveWindows(transform.parent.GetComponent<Monster>()));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReSearchPlayer()
    {
        if (LootingManager.Inst.SearchLootList())
        {
            myPlayer.SetisObjectNear(true);
            int index = LootingManager.Inst.SearchIndexLootList();
            if (index != -1)
            {
                myPlayer.OpenUi.AddListener(() => LootingManager.Inst.RefreshLootTarget(LootingManager.Inst.myLootingMonster[index].myMonster));
                myPlayer.CloseUi.AddListener(() => LootingManager.Inst.RemoveWindows(LootingManager.Inst.myLootingMonster[index].myMonster));
            }
        }
        else
        {
            myPlayer.SetisObjectNear(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IinterPlay>() != null)
        {
            myPlayer = other.GetComponent<IinterPlay>();
            myPlayer.SetisObjectNear(true);
            myPlayer.OpenUi.RemoveAllListeners();
            myPlayer.CloseUi.RemoveAllListeners();
            LootingManager.Inst.ListIsEnterUpdate(other.GetComponent<Monster>(), true);
            //myPlayer.OpenLoot += LootingManager.Inst.RefreshLootTarget?.Invoke(transform.parent.GetComponent<Monster>());
            myPlayer.OpenUi.AddListener(() => LootingManager.Inst.RefreshLootTarget(transform.parent.GetComponent<Monster>()));
            myPlayer.CloseUi.AddListener(() => LootingManager.Inst.RemoveWindows(transform.parent.GetComponent<Monster>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent <IinterPlay>() != null)
        {
            myPlayer.OpenUi.RemoveAllListeners();
            myPlayer.CloseUi.RemoveAllListeners();
            LootingManager.Inst.ListIsEnterUpdate(other.GetComponent<Monster>(),false);
            if (LootingManager.Inst.SearchLootList())
            {
                myPlayer.SetisObjectNear(true);
                int index = LootingManager.Inst.SearchIndexLootList();
                if( index != -1 )
                {
                    myPlayer.OpenUi.AddListener(() => LootingManager.Inst.RefreshLootTarget(LootingManager.Inst.myLootingMonster[index].myMonster));
                    myPlayer.CloseUi.AddListener(() => LootingManager.Inst.RemoveWindows(LootingManager.Inst.myLootingMonster[index].myMonster));
                }
            }
            else
            {
                myPlayer.SetisObjectNear(false);
            }
        }
    }
}
