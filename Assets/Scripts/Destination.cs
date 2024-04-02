using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject panelUI;      // Object组件

    ////--------------------------------------------------------Event Function 事件触发函数
    ///【到达目的地】
    private void OnTriggerEnter2D(Collider2D col)       // 当box和Trigger相撞
    {
        if (col.CompareTag("Player"))       // 若是Palyer遇到
        {
            panelUI.SetActive(true);     // 开启panelUI
        }
    }

    ///【离开目的地】
    private void OnTriggerExit2D(Collider2D other)      // 当box和Trigger从相撞到不相撞
    {
        if (other.CompareTag("Player"))       // 若是Palyer遇到
        {
            panelUI.SetActive(false);     // 关闭panelUI
        }
    }
}
