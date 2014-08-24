using UnityEngine;
using System.Collections;

public class TetherCollider : MonoBehaviour
{
    virtual public void OnTetherCollision(Tether tether, int segment)
    {
        Debug.Log("Collided with segment: " + segment);
    }
}
