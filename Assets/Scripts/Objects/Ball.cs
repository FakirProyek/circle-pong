using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using BlitheFramework;
public class Ball : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public GameManager GameManager { get => gameManager; set => gameManager = value; }

    #endregion Public_field

    #region Pivate_field
    private Rigidbody2D rigidbody;
    private Vector2 velocity;
    private int owner;
    private GameManager gameManager;

    #endregion Pivate_field
    #endregion Initialize

    public override void Init()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = velocity;
        owner = 0;
    }

    void Start()
    {

    }
    #region factory
    #region EVENT_LISTENER_ADD
    #endregion EVENT_LISTENER_ADD
    #region EVENT_LISTENER_METHOD
    #endregion EVENT_LISTENER_METHOD
    #endregion factory
    #region private method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Debug.Log("hit border");
            switch (owner)
            {
                case 1:
                    gameManager.Goal(0);
                    break;
                case 2:
                    gameManager.Goal(1);
                    break;
                default:
                    Debug.Log("Owner 0 bounce back");
                    /*velocity.x = rigidbody.velocity.x;
                    velocity.y = (rigidbody.velocity.y / 2.0f) + (collision.attachedRigidbody.velocity.y / 3.0f);
                    rigidbody.velocity = -velocity;*/
                    Debug.Log(rigidbody.velocity);
                    rigidbody.velocity *= -1;
                    Debug.Log(rigidbody.velocity);
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PhotonView>().IsMine)
            {
                owner = 1;
            }
            else
            {
                owner = 2;
            }
            Debug.Log("Owner : " + owner);
        }
    }

    #endregion
    #region public method
    public void Remove()
    {
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
    #region update
    public void UpdateMethod()
    {
        
    }

    public void ResetPosition(Vector2 _velocity)
    {
        transform.position = Vector3.zero;
        rigidbody.velocity = _velocity;
    }
    #endregion
}