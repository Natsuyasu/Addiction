using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject panelUI;      // Object���

    ////--------------------------------------------------------Event Function �¼���������
    ///������Ŀ�ĵء�
    private void OnTriggerEnter2D(Collider2D col)       // ��box��Trigger��ײ
    {
        if (col.CompareTag("Player"))       // ����Palyer����
        {
            panelUI.SetActive(true);     // ����panelUI
        }
    }

    ///���뿪Ŀ�ĵء�
    private void OnTriggerExit2D(Collider2D other)      // ��box��Trigger����ײ������ײ
    {
        if (other.CompareTag("Player"))       // ����Palyer����
        {
            panelUI.SetActive(false);     // �ر�panelUI
        }
    }
}
