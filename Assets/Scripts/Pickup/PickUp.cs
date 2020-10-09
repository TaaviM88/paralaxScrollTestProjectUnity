using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PickUp : MonoBehaviour
{
    public enum PlayerCarryingState
    {
        None,
        PickingUp,
        Carry,
        Lowering
    }

    basic2DMovement move;
    PlayerCarryingState state = PlayerCarryingState.None;
    public Transform carryingPoint;
    public Transform interactPoint;

    public float range = 1f;
    public float pickupSpeed = 1f;

    public LayerMask pickupLayer;
    GameObject carryingObj;
    private Vector3 orginalInteractivePosition, orginalCarryingPosition;

    // Start is called before the first frame update
    void Awake()
    {
        move = GetComponent<basic2DMovement>();

        orginalInteractivePosition = interactPoint.localPosition;
        orginalCarryingPosition = carryingPoint.localPosition;
    }


    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            PickUpObject();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(move.side == 1 && interactPoint.localPosition != orginalInteractivePosition)
        {
            interactPoint.localPosition = orginalInteractivePosition;
            carryingPoint.localPosition = orginalCarryingPosition;
        }

        if (move.side == -1 && interactPoint.localPosition == orginalInteractivePosition)
        {
            interactPoint.localPosition = new Vector2(interactPoint.localPosition.x * -1, interactPoint.localPosition.y);
            carryingPoint.localPosition = new Vector2(carryingPoint.localPosition.x * -1, carryingPoint.localPosition.y);
        }
    }

    public void PickUpObject()
    {
        //jatketaan
        switch (state)
        {
            case PlayerCarryingState.None:

                Collider2D pickupObject;
                pickupObject = Physics2D.OverlapCircle(interactPoint.position, range, pickupLayer);
                    
       
                if (pickupObject?.GetComponent<PickableObj>() && pickupObject.transform.parent == null && pickupObject.GetComponent<PickableObj>().CanLifted())
                {
                    move.FreezeMovement();
                    carryingObj = pickupObject.gameObject;
                    carryingObj.GetComponent<PickableObj>().SetObjectState(PickableObj.PickableState.lifted);
                    //Disable asiat mitä pelaaja ei voi tehdä kun kantaa laatikkoa

                    carryingObj.transform.SetParent(carryingPoint);
                    carryingObj.transform.DOMove(carryingPoint.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => SetPlayerCarrying());
                }
                
                //Collider2D[] pickupObjects;
                //pickupObjects = Physics2D.OverlapCircleAll(interactPoint.position, range, pickupLayer);

                //for (int i = 0; i < pickupObjects.Length; i++)
                //{
                //    if (pickupObjects[i].GetComponent<PickableObj>() && pickupObjects[i].transform.parent == null && pickupObjects[i].GetComponent<PickableObj>().CanLifted())
                //    {
                //        move.FreezeMovement();
                //        carryingObj = pickupObjects[i].gameObject;
                //        carryingObj.GetComponent<PickableObj>().SetObjectState(PickableObj.PickableState.lifted);
                //        //Disable asiat mitä pelaaja ei voi tehdä kun kantaa laatikkoa

                //        carryingObj.transform.SetParent(carryingPoint);
                //        carryingObj.transform.DOMove(carryingPoint.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => SetPlayerCarrying());
                //    }
                //}
                break;
            case PlayerCarryingState.PickingUp:
                break;
            case PlayerCarryingState.Carry:
                move.FreezeMovement();
                carryingObj?.transform.DOMove(interactPoint.position, pickupSpeed).SetEase(Ease.InFlash).OnComplete(() => LowerObject());
                break;
            case PlayerCarryingState.Lowering:
                break;
            default:
                break;
        }
    }

    private void SetPlayerCarrying()
    {
        carryingObj.transform.localPosition = Vector2.zero;
        state = PlayerCarryingState.Carry;
        move.StartPlayerMovement();
    }

    public void LowerObject()
    {
        carryingObj.GetComponent<PickableObj>().SetObjectState(PickableObj.PickableState.lowered);
        carryingObj.transform.parent = null;
        carryingObj = null;
        state = PlayerCarryingState.None;
        move.StartPlayerMovement();
        //Muista enabloida kaikki mitä disabloid
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactPoint.position, range);
    }
}
