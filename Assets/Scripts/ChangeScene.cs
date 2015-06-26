using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void changeScene(string scene){
		Application.LoadLevel (scene);
	}
}
