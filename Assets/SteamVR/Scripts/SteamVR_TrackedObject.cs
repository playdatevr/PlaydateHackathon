//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
//
//=============================================================================

using UnityEngine;
using Valve.VR;

public class SteamVR_TrackedObject : MonoBehaviour
{
	public enum EIndex
	{
		None = -1,
		Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
		Device1,
		Device2,
		Device3,
		Device4,
		Device5,
		Device6,
		Device7,
		Device8,
		Device9,
		Device10,
		Device11,
		Device12,
		Device13,
		Device14,
		Device15
	}

	public EIndex index;
	public Transform origin; // if not set, relative to parent
    public bool isValid = false;
    public bool useRotation = true;
    public bool usePosition = true;

    private void OnNewPoses(params object[] args)
	{
		if (index == EIndex.None)
			return;

		var i = (int)index;

        isValid = false;
		var poses = (Valve.VR.TrackedDevicePose_t[])args[0];
		if (poses.Length <= i)
			return;

		if (!poses[i].bDeviceIsConnected)
			return;

		if (!poses[i].bPoseIsValid)
			return;

        isValid = true;

		var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

		if (origin != null)
		{
			pose = new SteamVR_Utils.RigidTransform(origin) * pose;
		
			if(usePosition) {
				pose.pos.x *= origin.localScale.x;
				pose.pos.y *= origin.localScale.y;
				pose.pos.z *= origin.localScale.z;
				transform.position = pose.pos;
			} else {
				pose.pos.x = 3;
				pose.pos.y = origin.position.y;
				pose.pos.z = 10;
				/*
				print("sphere pos:");
				print(origin.position.x);
				print(origin.position.y);
				print(origin.position.z);
				*/

				transform.position = origin.position;
			}
			if(useRotation) {
				transform.rotation = pose.rot;
			}
		}
		else
		{
			if(usePosition) {
				transform.localPosition = pose.pos;
			}	
			if(useRotation) {
				transform.localRotation = pose.rot;
			}
		}
	}

	void OnEnable()
	{
		var render = SteamVR_Render.instance;
		if (render == null)
		{
			enabled = false;
			return;
		}

		SteamVR_Utils.Event.Listen("new_poses", OnNewPoses);
	}

	void OnDisable()
	{
		SteamVR_Utils.Event.Remove("new_poses", OnNewPoses);
		isValid = false;
	}

	public void SetDeviceIndex(int index)
	{
		if (System.Enum.IsDefined(typeof(EIndex), index))
			this.index = (EIndex)index;
	}
}

