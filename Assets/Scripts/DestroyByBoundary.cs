using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    //NOTA. Es mejor usar un pool a está opción

    void OnTriggerExit(Collider collision) {
        Destroy(collision.gameObject);
    }
}
