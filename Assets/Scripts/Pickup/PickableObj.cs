using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableObj : MonoBehaviour
{
    public enum PickableState
    {
        none,
        lifted,
        lowered
    }

    public PickableState state = PickableState.none;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private bool canBeLifted = true;
    private int spawnID;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

    }

    public void SetObjectState(PickableState newState)
    {
        state = newState;
        DoAction();
    }

    //Vaihda Update-funtioon jos tuntuu, että ei toimi
    public void DoAction()
    {
        switch (state)
        {
            case PickableState.none:
                break;
            case PickableState.lifted:
                rb.isKinematic = true;
                boxCollider2D.isTrigger = true;
                break;
            case PickableState.lowered:
                rb.isKinematic = false;
                boxCollider2D.isTrigger = false;
                break;
            default:
                break;
        }
    }

    public bool CanLifted()
    {
        return canBeLifted;
    }
    public void SetSpawnerID(int newID)
    {
        spawnID = newID;
    }

    protected void DestroyGameobj()
    {
        //GameEvents.current.SpawnObject(spawnID);
        Destroy(gameObject);
    }
}
