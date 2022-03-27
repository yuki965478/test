using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public static SaveManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
                _instance.Load();
            }
            return _instance;
        }
        set { _instance = value; }
    }
    static SaveManager _instance;

    /// <summary>�s�ɸ��</summary>
    public AllSaveData allSaveData = new AllSaveData();
    /// <summary>��e���</summary>
    public PlayerData nowData = new PlayerData();

    /// <summary>���J�ɮ�</summary>
    void Load()
    {
        // �ԥX��l��(��r)
        string jsonData = PlayerPrefs.GetString("AllSaveData");
        if (jsonData == "")
        {
            // �L���� �ݭn��ʪ�l��
            // �̪Ű��@�Ӫ��a���
            PlayerData tempPlayerData = new PlayerData();
            tempPlayerData.playerName = "���R�W";
            tempPlayerData.level = 1;
            tempPlayerData.exp = 0;
            tempPlayerData.currentResolutionIndex = -1;
            // <<�K�[���>>
            // �̪Ű��@�ӥ�����
            AllSaveData tempAllSaveData = new AllSaveData();
            tempAllSaveData.playerDataList = new List<PlayerData>();
            tempAllSaveData.playerDataList.Add(tempPlayerData);

            allSaveData = tempAllSaveData;
        }
        else
        {
            // ������ Ū����ӧa
            allSaveData = JsonUtility.FromJson<AllSaveData>(jsonData);
        }
    }
    /// <summary>�s��</summary>
    /// <param name="number">�ĴX�����</param>
    public void SaveGame(int number = 0)
    {
        // ��o���s�ɸ�Ʃ�i��ƪ�̭�
        SaveManager.instance.allSaveData.playerDataList[number] = SaveManager.instance.nowData;
        // �I�sSaveManager��Ҧ���ƶ�i�w�и�
        Save();
    }
    /// <summary>�x�s�ɮ�</summary>
    void Save()
    {
        // �����ഫ����r
        string jsonData = JsonUtility.ToJson(allSaveData);
        // ���r�s�_��
        PlayerPrefs.SetString("AllSaveData", jsonData);
    }

    /// <summary>�إߤ@���s�����a���</summary>
    public void CreateNewPlayerData()
    {
        // �̪Ű��@�Ӫ��a���
        PlayerData tempPlayerData = new PlayerData();
        tempPlayerData.playerName = "���R�W";
        tempPlayerData.level = 1;
        tempPlayerData.exp = 0;
        tempPlayerData.currentResolutionIndex = -1;

        // ����гy���s��Ʒ��ثe�����
        nowData = tempPlayerData;
    }
    /// <summary>���J0���s��</summary>
    public void LoadPlayerData_0()
    {
        // �q�D��Ƹ̭��ԥX0����ƥX�ӥ�
        nowData = allSaveData.playerDataList[0];
    }

    public void Log()
    {
        // ���ձN��Ƶ��c�ഫ��json�r��
        Debug.Log(JsonUtility.ToJson(allSaveData, true));
    }
}

// ----------���a�����ɪ���Ƶ��c----------

/// <summary>�Ҧ��n�x�s���F��</summary>
[System.Serializable]
public struct AllSaveData
{
    [SerializeField]
    public List<PlayerData> playerDataList;
}

/// <summary>�浧���a���</summary>
[System.Serializable]
public struct PlayerData
{
    [SerializeField]
    public string playerName;
    public int level;
    public int exp;
    public int characterID;
    public int currentResolutionIndex;
    public int currentQualityIndex;
    public bool isFullScreen;
    public string loadedScene;
}
