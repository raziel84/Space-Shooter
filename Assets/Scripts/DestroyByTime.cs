using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    //Este script destruye el gameobject donde está colocado al cabode cierto tiempo
    public float lifeTime;

	void Start () {
        Destroy(gameObject, lifeTime);
	}
	
}
