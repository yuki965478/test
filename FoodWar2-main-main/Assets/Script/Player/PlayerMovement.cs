using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] Transform playerPos = null;
    [SerializeField] Transform cameraPos = null;
    float Speed = 5f;
    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        // �p�G�ڬO���L
        if (photonView.IsMine)
        {
            cameraPos = GameObject.Find("CameraPos").transform;
            photonView.RPC("SetSkin", RpcTarget.All, SaveManager.instance.nowData.characterID);
        }
        else // �p�G�ڬO�蹳
        {

        }
    }
    [PunRPC]
    public void SetSkin(int cid)
    {

    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Movement();
        }
        else
        {
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Movement();
        }
        else
        {
            // �蹳�n���ܨ쥻�L��m
            rb.MovePosition(Vector3.Lerp(rb.position, rpcPos, Time.deltaTime * �蹳�P�B�t�v));
        }
    }

    Vector3 lestPos = Vector3.zero;
    [SerializeField] float �h���n�P�B = 0.1f;
    void Movement()
    {
        float horizotalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizotalMove * Speed, Mathf.Clamp(rb.velocity.y, -20f, 20f), verticalMove * Speed);
        // �������P�B�y�� �p�G���L���ʦ�m�N�P�B�o�e���蹳
        float d = Vector3.Distance(rb.position, lestPos);
        if (d > �h���n�P�B)
        {
            photonView.RPC("GetPos", RpcTarget.Others, rb.position);
        }
    }
    Vector3 rpcPos = Vector3.zero;
    [PunRPC]
    public void GetPos(Vector3 pos)
    {
        // �u���蹳�|�����m�T��
        rpcPos = pos;
    }
    [SerializeField] float �蹳�P�B�t�v = 10f;
    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            if (cameraPos != null)
            {
                cameraPos.transform.position = playerPos.transform.position;
            }
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.name == "�l�l")
            {
                collision.gameObject.GetComponent<�l�l>().OpenOrClose();
            }
        }
        else
        {

        }
    }
}