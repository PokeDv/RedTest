using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
	public List<Data> serverData = new List<Data>();
	public List<Data> clientData = new List<Data>();
	public List<Data> toServer   = new List<Data>();
	public int counter;
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (isClient)
		{
			float _h = 0;
			if ((_h = Input.GetAxis("Horizontal")) > 0)
			{
				toServer.Add(new Data{h = _h});
			}
		}

		if (counter % 5 == 0)
		{
			Cmd_SendData(toServer);
		}
		counter++;
	}

	[Command]
	private void Cmd_SendData(List<Data> datas)
	{
		serverData = datas;
	}
	
	[ClientRpc]
	private void Rpc_SendData(List<Data> datas)
	{
		serverData = datas;
	}
}

[System.Serializable]
public class Data
{
    public float h;
}
