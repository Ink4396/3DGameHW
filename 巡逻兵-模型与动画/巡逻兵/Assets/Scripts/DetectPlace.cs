using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlace : MonoBehaviour
{
    public delegate void SetArea(float x, float y);
    public static event SetArea setArea;                  //�����¼�����

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            setArea(transform.position.x, transform.position.z);
        }
    }

}
