using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager
{
    public static GameManager instance
    {
        get
        {
            // ���H�ݭn�ڪ��ɭ� �]���ڹ����Ƥ@�˨S������ �N�ۧڳгy�b��Ť�
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }
    static GameManager _instance = null;

    public bool �q�ж��h�� = false;

    
}

public enum FoodTeam
{
    /// <summary>0~4�O�n�������^���}��</summary>
    GOOD,
    /// <summary>5~9�O�����d�������^���}��</summary>
    BAD
}

