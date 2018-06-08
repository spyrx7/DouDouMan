using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
                                    // 在x轴的距离内，玩家可以在摄像机前移动。
    public float xMargin = 1f;      // 在x轴的距离内，玩家可以在摄像机前移动。
    public float yMargin = 1f;      // 在y轴上，玩家可以在摄像机前移动。
    public float xSmooth = 8f;      // 相机在x轴的目标运动中如何平滑地捕捉到它?.
    public float ySmooth = 8f;      // 相机在y轴上的目标运动是如何顺利地捕捉到的。
    public Vector2 maxXAndY;        // 相机的最大x和y坐标。
    public Vector2 minXAndY;        // 相机的最小x和y坐标。


    private Transform player;       // Reference to the player's transform.  参考玩家的转换。


    void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

        // If the player has moved beyond the y margin...
        //如果player已经超出了y的界限…
        if (CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

        // 目标x和y坐标不应该大于或小于最小值。
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
