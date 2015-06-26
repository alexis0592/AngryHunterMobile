using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {


	private const string typeName = "abcd1234";
	private const string gameName = "DuckHunter";

	private bool isRefreshingHostList = false;
	private HostData[] hostList;

	private NetworkView nView;

	void Start(){
		nView = GetComponent<NetworkView> ();
	}

	public void changeScene(string scene){
		//JoinServer (hostList[0]);
		Debug.Log("Entr a change scene");
		RefreshHostList ();
		if (hostList != null) {
			JoinServer(hostList[0]);
			SendInfoToServer();
			//Application.LoadLevel (scene);
		}
	}

	private void RefreshHostList(){
		Debug.Log("Entr a change RefreshHostList");
		if (!isRefreshingHostList){
			isRefreshingHostList = true;
			MasterServer.RequestHostList(typeName);
		}
	}

	void OnMasterServerEvent(MasterServerEvent msEvent){
		Debug.Log("Entr a change OnMasterServerEvent");
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList ();
		}
	}

	private void JoinServer(HostData hostData){
		Debug.Log("Entr a change JoinServer");
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer(){
		Debug.Log("Entr a change OnConnectedToServer");
		Debug.Log("Server Joined");
	}

	[RPC] void SendInfoToServer(){
		string info = "Hola servidor";
		nView.RPC("ReceiveInfoFromClient", RPCMode.Server, info);
	}

	[RPC]
	void ReceiveInfoFromClient(string info){}
		
	
}
