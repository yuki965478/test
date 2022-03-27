using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class Lobby : MonoBehaviourPunCallbacks
{
    #region �s�u
    public static Lobby ins = null;
    private void Awake()
    {
        ins = this;
        

    }

    void Start()
    {
       
        // �۰ʱN�Ҧ��H�Ԩ�ХD������
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���J���a���
        SaveManager.instance.LoadPlayerData_0();

        // �]�w�ʺ�
        PhotonNetwork.NickName = SaveManager.instance.nowData.playerName;

        // �p�G�w�g�s�L�F�N�O�s
        if (PhotonNetwork.IsConnected)
        {
            OnConnectedToMaster();
            return;
        }
        
        �UŪ.ins.info = "�}�l�s�u����A��...";
        �UŪ.ins.isOpen = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        �UŪ.ins.info = "�s�W���A��...";
        �UŪ.ins.isOpen = false;
        �s�W�F = true;
        
        OpenMenu();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        �UŪ.ins.isOpen = false;
        �j�M�ж�.ins.Close();
        �ж��C��.ins.Close();
        �Ыةж�.ins.Close();
        �s�W�F = false;
        if (cause == DisconnectCause.MaxCcuReached)
        {
            �F��.ins.���F��("�C���z���F�еy��b�աC");
            Invoke("Start", 5f);
        }
        else
        {
            �F��.ins.���F��("�_�u�F�C");
            Invoke("Start", 5f);
        }
    }
    public bool �s�W�F = false;
    // ----------------------------------------------
    #endregion
    public void OpenMenu()
    {
        if (�s�W�F)
        {
            StartGameLobby.ins.Open();
        }
    }
    #region �Ыةж�
    public void �}�D���()
    {
        if (�s�W�F)
        {
            �D���.ins.Open();
            BlockUI.ins.Open();
        }
        else
        {
            �F��.ins.���F��("�ثe�S���s�W���СA�еy��A�աC");
        }
           
    }
    public void �إߩж�(string v, int number)
    {
        �UŪ.ins.info = "�Ыةж���...";
        �UŪ.ins.isOpen = true;

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = (byte)number;
        ro.PublishUserId = true;
        PhotonNetwork.CreateRoom(v, ro);
    }
    public override void OnJoinedRoom()
    {
        // ���󱡪p�U�i�J�ж����|�Ө즹
        �UŪ.ins.isOpen = false;
        // �ХD�M�w����ӳ��� �Ыȷ|�۰ʸ��H
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene(1);
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        �UŪ.ins.isOpen = false;
        �F��.ins.���F��("�ж��W�ٽĬ�F�C");
    }
    #endregion

    public void �i�J�ж�(string �ЦW)
    {
        �UŪ.ins.isOpen = true;
        �UŪ.ins.info = "���b�[�J�ж�...";
        PhotonNetwork.JoinRoom(�ЦW);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        �UŪ.ins.isOpen = false;
        �F��.ins.���F��("�ж��H�Ƥw���Ωж����s�b�C");
    }
}