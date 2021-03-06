﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    [Range(1, 2)]
    public int PlayerId = 1;

    [Header("Direction: Only 1 or -1")]
    [Range(-1, 1)]
    public int Direction = 1;

    [Header("States")]
    public bool IsGrounded = false;
    public bool IsWallSliding = false;
    public bool IsCrouching = false;
    //public bool FootInFrontTriggered = false;
    //public bool FootInBackTriggered = false;
    public bool FrontTriggered = false;
    public bool BackTriggered = false;
    public bool HighHitTriggered = false;
    public bool MidHitTriggered = false;
    public bool LowHitTriggered = false;

    public bool InWall = false;

    [Header("Trigger: Place here")]
    public GameObject GroundChecker;
    //public GameObject FootInBack;
    public GameObject WallTriggerBack;
    public GameObject WallTriggerFront;
    public GameObject HighHit;
    public GameObject MidHit;
    public GameObject LowHit;

    [Header("Animator: No Touching")]
    public Animator Animator;

    //private LayerTrigger _footBackTrigger;
    private LayerTrigger _groundCheckerTrigger;
    private LayerTrigger _backTrigger;
    private LayerTrigger _frontTrigger;
    private LayerTrigger _hightHitTrigger;
    private LayerTrigger _midHitTrigger;
    private LayerTrigger _lowHitTrigger;

    private bool _blockInput = false;


	// Use this for initialization
	void Start ()
	{
	    InitializeTrigger();
        Animator = GetComponent<Animator>();
	}
	// Update is called once per frame
	void  Update () {
        UpdateTrigger();
	    //IsGrounded = FootInFrontTriggered && FootInBackTriggered;
        UpdateAnimator();
	}


    private void UpdateTrigger()
    {
        //FootInBackTriggered = _footBackTrigger.isTriggered;
        IsGrounded = _groundCheckerTrigger.isTriggered;
        BackTriggered = !InWall && _backTrigger.isTriggered;
        FrontTriggered = !InWall && _frontTrigger.isTriggered;
        HighHitTriggered = !InWall && _hightHitTrigger.isTriggered;
        MidHitTriggered = !InWall && _midHitTrigger.isTriggered;
        LowHitTriggered = !InWall && _lowHitTrigger.isTriggered;
    }

    public void Flip()
    {   
        // Flip the gameObject based on localScale
        Direction *= -1;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;

        //Flip Trigger
        bool temp = BackTriggered;
        BackTriggered = FrontTriggered;
        FrontTriggered = temp;

       /* temp = FootInBackTriggered;
        FootInBackTriggered = FootInFrontTriggered;
        FootInFrontTriggered = temp;*/
    }

    public Vector2 DirectionVector()
    {
        return Vector2.right * Direction;
    }

    private void UpdateAnimator()
    {
        var velX = rigidbody2D.velocity.x;
        Animator.SetFloat("movementSpeed", Mathf.Abs(velX));
        Animator.SetBool("isWallSliding", IsWallSliding);
        Animator.SetBool("isCrouching", IsCrouching);
        Animator.SetBool("isGrounded", IsGrounded);
    }

    private void InitializeTrigger()
    {
       // _footBackTrigger = FootInBack.GetComponent<LayerTrigger>();
        _groundCheckerTrigger = GroundChecker.GetComponent<LayerTrigger>();
        _backTrigger = WallTriggerBack.GetComponent<LayerTrigger>();
        _frontTrigger = WallTriggerFront.GetComponent<LayerTrigger>();
        _hightHitTrigger = HighHit.GetComponent<LayerTrigger>();
        _midHitTrigger = MidHit.GetComponent<LayerTrigger>();
        _lowHitTrigger = LowHit.GetComponent<LayerTrigger>();
    }

    public bool IsInputBlocked ()
    {
        return _blockInput;
    }

    public void BlockInput(bool block)
    {
        _blockInput = block;
    }

 }
