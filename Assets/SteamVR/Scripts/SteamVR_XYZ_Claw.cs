//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: For controlling in-game objects with tracked devices.
//
//=============================================================================

using UnityEngine;
using Valve.VR;

public class SteamVR_XYZ_Claw : MonoBehaviour
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
    public bool controlX = true;
    public bool controlY = true;
    public bool controlZ = true;
    public bool transformObject = true;
	public Transform thingToMove; // if not set, relative to parent

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

	
			/*	
			pose.pos.x *= origin.localScale.x;
			pose.pos.y *= origin.localScale.y;
			pose.pos.z *= origin.localScale.z;
			transform.position = pose.pos;
			transform.rotation = pose.rot;
			*/
	
			//Vector3 temppos = new Vector3(0,0,0);
			Vector3 temppos = transform.position;
			if(controlX == true) {
				thingToMove.position = new Vector3(pose.pos.x, thingToMove.position.y, thingToMove.position.z);
			}
			if(controlY == true) {
				thingToMove.position = new Vector3(thingToMove.position.x, pose.pos.y, thingToMove.position.z);
			}
			if(controlZ == true) {
				thingToMove.position = new Vector3(thingToMove.position.x, thingToMove.position.y, pose.pos.z);
			}
			print("-=-=-=-=-=");
			print("thingToMove.position");


			// move this object
			if(transformObject == true) {
				transform.localPosition = pose.pos;
				transform.localRotation = pose.rot;
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

