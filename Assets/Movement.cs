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

	private Data data;

	// Update is called once per frame
	void Update () 
	{
		
		if (isClient)
		{
			float _h = 0;
			if ((_h = Input.GetAxis("Horizontal")) > 0)
			{
				data = new Data {h = _h};
			}
		}
		
	}

	void FixedUpdate()
	{
		if (isClient && data != null)
		{
			toServer.Add(data);
		}

		if ((isServer) && serverData.Count > 0)
		{
			serverData.RemoveAt(0);
		}
		
		if (counter++ % 5 == 0 && toServer.Count > 0)
		{
			Cmd_SendData(new WraperData(){d = toServer});
		}
	}

	private float speed = 5;
	
	private void Move(Data data)
	{
		transform.Translate(new Vector3(data.h * Time.fixedDeltaTime * speed,0,0));
	}
	
	[Command]
	private void Cmd_SendData(WraperData datas)
	{
		serverData = datas.d;
		Rpc_SendData(datas);
	}
	
	[ClientRpc]
	private void Rpc_SendData(WraperData datas)
	{
		clientData = datas.d;
	}
}

[System.Serializable]
public class WraperData
{
	public List<Data> d = new List<Data>();
}
[System.Serializable]
public class Data
{
    public float h;
}
