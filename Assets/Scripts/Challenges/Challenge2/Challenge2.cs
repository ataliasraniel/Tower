using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Challenge2 : MonoBehaviour
{
    public Transform level;
    public GameObject switcher;
    public Transform switcherPos;

    public GameObject doors;
    public Light[] levelLights;

    public int diedEnemies;

    public GameObject goblins;
    public GameObject mage;
    public Transform[] localToSpawn;

    public Transform[] thirdPhaseLocalSpawn;

    private EnemyLifeSystem lifeSystem;
    private void Start()
    {
        EventManager.instance.OnSecondPhase += SecondPhaseStart;
    }
    public void UpdateEnemies()
    {
        diedEnemies++;
        if (diedEnemies >= 4 && doors!=null) 
        {
            StartCoroutine(UnlockDoors());
        }
        if(diedEnemies >= 8)
        {
            Instantiate(switcher, switcherPos.position, Quaternion.identity);
        }
    }
    public void LockDoors(bool isLeft)
    {
        doors.SetActive(true);
        foreach (var item in levelLights)
        {
            item.color = Color.red;
        }
        doors.transform.DOLocalMoveY(14, 2);

        for (int i = 0; i < localToSpawn.Length; i++)
        {
            if(isLeft && i <= 4)
            {
                var clone = Instantiate(goblins, localToSpawn[i].position, Quaternion.identity);
            }
            else if(!isLeft && i >= 4)
            {
                var clone = Instantiate(goblins, localToSpawn[i].position, Quaternion.identity);
            }
            
        }
    }
    private IEnumerator UnlockDoors()
    {
        doors.transform.DOLocalMoveY(0, 2f);
        yield return new WaitForSeconds(2);
        Destroy(doors);
    }
    public void SecondPhaseStart()
    {
        EventManager.instance.OnSecondPhase -= SecondPhaseStart;
        var switches = GameObject.FindGameObjectWithTag("Switcher");
        Destroy(switches);
        GameObject[] livingGoblins = GameObject.FindGameObjectsWithTag("Goblin");
        foreach (var item in livingGoblins)
        {
            Destroy(item);
        }
        for (int i = 0; i < thirdPhaseLocalSpawn.Length; i++)
        {
            Instantiate(goblins, thirdPhaseLocalSpawn[i].transform.position, Quaternion.identity);
            if(i <= 3)
            {
                Instantiate(mage, new Vector3(Random.Range(-20, 20), 10,
                    Random.Range(-20, 20)), Quaternion.identity);
            }
        }
        
        StartCoroutine(LevelAnim());

    }
    private IEnumerator LevelAnim()
    {
        level.DOLocalMoveY(-25, 2f);
        yield return new WaitForSeconds(4);
        Destroy(level.gameObject);
    }
}
