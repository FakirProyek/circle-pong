using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;
public class GameManager : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    #endregion Public_field

    #region Pivate_field
    private bool isHost;

    private System.Random random;

    private const float ENUM_SPEED = (2 * Mathf.PI) / 360;
    private const int ENUM_MAX_BALL_SPEED = 2;

    #endregion Pivate_field
    #endregion Initialize

    public override void Init()
    {
        random = new System.Random();
        CreateFactoryBall();
        CreateFactoryBat();
        InitPlayers();
        InitBall();
    }

    void Start()
    {
        Init();
    }
    #region factory
    [SerializeField] private GameObject prefabBat;
    FactoryBat factoryBat;
    private void CreateFactoryBat()
    {
        var go = new GameObject();
        go.name = "FactoryBat";
        factoryBat = new FactoryBat();
        factoryBat = go.AddComponent<FactoryBat>() as FactoryBat;
        #region EVENT_LISTENER_ADD_FactoryBat
        factoryBat.EVENT_REMOVE += OnRemoveFactoryBat;
        #endregion EVENT_LISTENER_ADD_FactoryBat    
}

    [SerializeField] private GameObject prefabBall;
    FactoryBall factoryBall;
    private void CreateFactoryBall()
    {
        var go = new GameObject();
        go.name = "FactoryBall";
        factoryBall = new FactoryBall();
        factoryBall = go.AddComponent<FactoryBall>() as FactoryBall;
        #region EVENT_LISTENER_ADD_FactoryBall
        factoryBall.EVENT_REMOVE += OnRemoveFactoryBall;
        #endregion EVENT_LISTENER_ADD_FactoryBall    
}

    #region EVENT_LISTENER_ADD
    #endregion EVENT_LISTENER_ADD
    #region EVENT_LISTENER_METHOD
    private void OnRemoveFactoryBat(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        #region EVENT_LISTENER_REMOVE_FactoryBat
        sender.GetComponent<FactoryBat>().EVENT_REMOVE -= OnRemoveFactoryBat;
        #endregion EVENT_LISTENER_REMOVE_FactoryBat
        Destroy(sender);
    }

    private void OnRemoveFactoryBall(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        #region EVENT_LISTENER_REMOVE_FactoryBall
        sender.GetComponent<FactoryBall>().EVENT_REMOVE -= OnRemoveFactoryBall;
        #endregion EVENT_LISTENER_REMOVE_FactoryBall
        Destroy(sender);
    }

    #endregion EVENT_LISTENER_METHOD
    #endregion factory
    #region private method
    private void InitBall()
    {
        factoryBall.Add(prefabBall, Vector3.zero, Quaternion.identity, new Vector2(
                random.Next(-ENUM_MAX_BALL_SPEED, ENUM_MAX_BALL_SPEED), 
                random.Next(-ENUM_MAX_BALL_SPEED, ENUM_MAX_BALL_SPEED)));
    }
    private void InitPlayers()
    {
        factoryBat.Add(prefabBat, new Vector3(2.5f, 0), Quaternion.Euler(0, 0, 0), true, 0);
        factoryBat.Add(prefabBat, new Vector3(-2.5f, 0), Quaternion.Euler(0, 0, 180), false, 0);
    }

    private void UpdatePlayer()
    {
        for (int i = 0; i < factoryBat.GetNumberOfObjectFactories(); i++)
        {
            if (factoryBat.Get(i).IsHost == isHost)
            {
                factoryBat.Get(i).UpdateMethod();
            }
        }
    }

    private void ChangeMyPlayerSpeed(float _speed)
    {
        for (int i = 0; i < factoryBat.GetNumberOfObjectFactories(); i++)
        {
            if (factoryBat.Get(i).IsHost == isHost)
            {
                factoryBat.Get(i).Speed = _speed;
            }
        }
    }


    #endregion
    #region public method
    public void OnPlusButtonDown()
    {
        ChangeMyPlayerSpeed(ENUM_SPEED);
    }
    public void OnPlusButtonUp()
    {
        ChangeMyPlayerSpeed(0);
    }
    public void OnMinusButtonDown()
    {
        ChangeMyPlayerSpeed(ENUM_SPEED * -1);
    }
    public void OnMinusButtonUp()
    {
        ChangeMyPlayerSpeed(0);
    }
    public void Remove()
    {
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
    #region update
    public void FixedUpdate()
    {
        UpdatePlayer();
    }
    #endregion
}