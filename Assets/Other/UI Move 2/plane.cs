using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour
{
	Transform mTransform;

	Vector3 offsetPos;
	Vector3 finishPos = Vector3.zero;

	Vector2 range;

	float z = 0;

	bool isPlay = false;
	bool isMove = false;

	void Start()
	{
		mTransform = transform;
		finishPos = mTransform.localPosition;
	}

	void Update()
	{
		if (!isPlay)
			return;

		Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (Input.GetMouseButtonDown(0) && tempMousePos.x > GetComponent<Collider>().bounds.min.x && tempMousePos.x < GetComponent<Collider>().bounds.max.x
			&& tempMousePos.y > GetComponent<Collider>().bounds.min.y && tempMousePos.y < GetComponent<Collider>().bounds.max.y)
		{
			mTransform.parent.SendMessage("SetIsMoveFale");
			offsetPos = mTransform.position - tempMousePos;
			z = mTransform.position.z;
			isMove = true;
		}

		if (isMove && Input.GetMouseButton(0))
		{
			tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mTransform.position = new Vector3(tempMousePos.x + offsetPos.x, tempMousePos.y + offsetPos.y, z - 0.1f);
		}

		if (Input.GetMouseButtonUp(0))
		{
			mTransform.position = new Vector3(mTransform.position.x, mTransform.position.y, z);
			isMove = false;
		}

		IsFinish();
	}

	void IsFinish()
	{
		if (mTransform.localPosition.x > finishPos.x - range.x && mTransform.localPosition.x < finishPos.x + range.x
			&& mTransform.localPosition.y > finishPos.y - range.y && mTransform.localPosition.y < finishPos.y + range.y)
		{
			isPlay = false;
			mTransform.localPosition = finishPos;
			mTransform.parent.SendMessage("IsFinish");
		}
	}

	void Play()
	{
		isPlay = true;
	}

	void IsMoveFalse()
	{
		isMove = false;
	}

	void SetRange(Vector2 _range)
	{
		range = _range;
	}

	void SetTexture(Texture2D _tex)
	{
		mTransform.GetComponent<Renderer>().material.mainTexture = _tex;
	}
}
