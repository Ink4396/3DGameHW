using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject disk_Prefab;              //飞碟预制

    private List<DiskData> used;                //正被使用的飞碟
    private List<DiskData> free;                //空闲的飞碟

    public void Start()
    {
        used = new List<DiskData>();
        free = new List<DiskData>();
        disk_Prefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk_Prefab"), Vector3.zero, Quaternion.identity);
        disk_Prefab.SetActive(false);
    }

    public GameObject GetDisk(int round)
    {
        GameObject disk;
        //如果有空闲的飞碟，则直接使用，否则生成一个新的
        if (free.Count > 0)
        {
            disk = free[0].gameObject;
            disk.transform.localEulerAngles = Vector3.zero;
            free.Remove(free[0]);
        }
        else
        {
            disk = GameObject.Instantiate<GameObject>(disk_Prefab, Vector3.zero, Quaternion.identity);
            disk.AddComponent<DiskData>();
        }

        //按照round来设置飞碟属性
        //飞碟的等级 = 0~3之间的随机数 * 轮次数
        //0~4:  铜色飞碟  
        //4~7:  银色飞碟  
        //7~10: 金色飞碟
        float level = UnityEngine.Random.Range(0, 3f) * (round + 1);
        if (level < 4)
        {
            disk.GetComponent<DiskData>().points = 1;
            disk.GetComponent<DiskData>().speed = 4.0f;
            disk.GetComponent<DiskData>().direction = new Vector3(GetRandomInt(-16,16), 1, 0);
            disk.GetComponent<Renderer>().material.color = Color.grey;
            disk.transform.localScale = new Vector3(6.5f, 0.8f, 6.5f);
        }
        else if (level > 7)
        {
            disk.GetComponent<DiskData>().points = 3;
            disk.GetComponent<DiskData>().speed = 8.0f;
            disk.GetComponent<DiskData>().direction = new Vector3(GetRandomInt(-16, 16), 1, 0);
            disk.GetComponent<Renderer>().material.color = Color.cyan;
            disk.transform.localScale = new Vector3(2.5f, 0.4f, 2.5f);
        }
        else
        {
            disk.GetComponent<DiskData>().points = 2;
            disk.GetComponent<DiskData>().speed = 6.0f;
            disk.GetComponent<DiskData>().direction = new Vector3(GetRandomInt(-16, 16), 1, 0);
            disk.GetComponent<Renderer>().material.color = Color.yellow;
            disk.transform.localScale = new Vector3(4.5f, 0.6f, 4.5f);
        }

        used.Add(disk.GetComponent<DiskData>());

        return disk;
    }

    public int GetRandomInt(int min,int max)
    {
        int tmp_min=min,tmp_max=max;
        if (UnityEngine.Random.Range(-1f, 1f) > 0)
        {
            tmp_max = max / 2;
            if (UnityEngine.Random.Range(-1f, 1f) > 0)
            {
                return (tmp_max + tmp_min) / 2;
            }
            else
            {
                if (UnityEngine.Random.Range(-1f, 1f) > 0)
                {
                    tmp_max = tmp_max / 2;
                    return (tmp_max + tmp_min) / 2;
                }
                else
                {
                    if (UnityEngine.Random.Range(-1f, 1f) > 0)
                        return tmp_max;
                    else
                        return max;
                }
            }
        }
        else
        {
            tmp_min = min / 2;
            if (UnityEngine.Random.Range(-1f, 1f) > 0)
            {
                return (tmp_min + tmp_max) / 2;
            }
            else
            {
                if (UnityEngine.Random.Range(-1f, 1f) > 0)
                {
                    tmp_min = tmp_min / 2;
                    return (tmp_max + tmp_min) / 2;
                }
                else
                {
                    if (UnityEngine.Random.Range(-1f, 1f) > 0)
                        return tmp_min;
                    else
                        return min;
                }     
            }
        }
    }
    public void FreeDisk(GameObject disk)
    {
        //找到使用中的飞碟，将其踢出并加入到空闲队列
        foreach (DiskData diskData in used)
        {
            if (diskData.gameObject.GetInstanceID() == disk.GetInstanceID())
            {
                disk.SetActive(false);
                free.Add(diskData);
                used.Remove(diskData);
                break;
            }
        }
    }
}
