using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class UIPlayer : MonoBehaviourPunCallbacks
{
    public string playerName = "";
    int characterID = -1;
    private void Start()
    {
        // �p�G�ڬO���L�N�]�w���a�W��
        if (photonView.IsMine)
        {
            // �M�w�ν�
            this.gameObject.name = photonView.Owner.UserId;
            int characterID = CharacterSelection();
            SaveManager.instance.nowData.characterID = characterID;
            photonView.RPC("SetPlayerName", RpcTarget.AllBuffered, SaveManager.instance.nowData.playerName, characterID);
        }
    }

    [PunRPC]
    public void SetPlayerName(string v, int characterID)
    {
        playerName = v;
        this.characterID = characterID;
        // ���W�l����n�b�ж����s�W�@�Ӧۤv������
        �ж���.ins.�s�W�Y��(photonView.Owner.UserId, v, characterID);
    }
    private void OnDestroy()
    {
        if (�ж���.ins != null)
        �ж���.ins.�����Y��(photonView.Owner.UserId, characterID);
    }

    // 1.�T�{�������H����h
    // 2.�q�H����֪��@�Ӷ����ID
    // 2-2. �p�G����H�Ƥ@�˴N�H����Ӷ����
    // 3. �P�B�åͦ�UI

    /// <summary>���w���o����H���^��</summary>
    public int GetOkCharacterID(FoodTeam team)
    {
        int temp = 0;
        if (team == FoodTeam.GOOD)
            temp = Random.Range(0, 5);
        else if (team == FoodTeam.BAD)
            temp = Random.Range(6, 10);
        // �p�G�o��ID��Ф�������H����
        for (int i = 0; i < �ж���.ins.characterIDList.Count; i++)
        {
            if (�ж���.ins.characterIDList[i] == temp)
            {
                return GetOkCharacterID(team);
            }
        }
        return temp;
    }
    /// <summary>�^�Ǧ��X�Ӫ��a</summary>
    public int GetTeamCount(FoodTeam team)
    {
        int toteal = 0;
        for (int i = 0; i < �ж���.ins.characterIDList.Count; i++)
        {
            if (team == FoodTeam.GOOD && �ж���.ins.characterIDList[i] <= 4)
            {
                toteal++;
            }
            if (team == FoodTeam.BAD && �ж���.ins.characterIDList[i] >= 5)
            {
                toteal++;
            }
        }
        return toteal;
    }

    public int CharacterSelection()
    {
        int �n�������ƶq = GetTeamCount(FoodTeam.GOOD);
        int �a�������ƶq = GetTeamCount(FoodTeam.BAD);

        

        if (�n�������ƶq == �a�������ƶq)
        {
            if (Random.Range(0f, 100f) > 50f)
                return GetOkCharacterID(FoodTeam.GOOD);
            else
                return GetOkCharacterID(FoodTeam.BAD);
        }
        else if (�n�������ƶq > �a�������ƶq)
        {
            return GetOkCharacterID(FoodTeam.BAD);
        }
        else
        {
            return GetOkCharacterID(FoodTeam.GOOD);
        }
    }
}
