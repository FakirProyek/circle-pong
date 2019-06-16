using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlitheFramework;
public class Bat : BaseClass
{
    #region Initialize
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT

    #region Public_field
    public bool IsMine { get => isMine; set => isMine = value; }
    public float Angle { get => angle; set => angle = value; }
    public float Speed { get => speed; set => speed = value; }
    #endregion Public_field

    #region Pivate_field
    private bool isMine;
    private float angle;
    private float speed;

    private const float ENUM_CIRCLE_RADIUS = 2.5f;
    #endregion Pivate_field
    #endregion Initialize

    public override void Init()
    {

    }
    public void Init(bool _isMine, float _angle)
    {
        IsMine = _isMine;
        Angle = _angle;
        Speed = 0;
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
    private void UpdateSpeed()
    {
        angle += speed;
    }
    private void Move()
    {
        Vector3 diff = Vector3.zero - transform.position;
        diff.Normalize();
        float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);

        transform.position = new Vector3(Mathf.Cos(Angle * ENUM_CIRCLE_RADIUS) * ENUM_CIRCLE_RADIUS, Mathf.Sin(Angle * ENUM_CIRCLE_RADIUS) * ENUM_CIRCLE_RADIUS);
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
        UpdateSpeed();
        Move();
        /*transform.rotation = Quaternion.Euler(0, 0, Angle);
        Debug.Log(Angle + " ; " + transform.rotation.eulerAngles);
        transform.position = new Vector3(Mathf.Cos(Angle * ENUM_CIRCLE_RADIUS), Mathf.Sin(Angle * ENUM_CIRCLE_RADIUS));*/
    }
    #endregion
}