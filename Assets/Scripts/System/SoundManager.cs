using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static GameObject _soundManagerInstanse;

	void Awake ()
    {
        DontDestroyOnLoad(gameObject);

        if(_soundManagerInstanse == null)
        {
            _soundManagerInstanse = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
	}
	

}
