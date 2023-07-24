using UnityEngine;
using System.Collections;

public class main : MonoBehaviour
{
	public GameObject planeImage;

	public GameObject _plane;
	public GameObject _planeParent;
	public GameObject _background;
	//public Texture2D[] _texAll;
	public Vector3[] _RandomPos;
	public int raw = 3;
	public int volumn = 3;
	public float factor = 0.25f;

	GameObject[] _tempPlaneAll;

	float sideLength = 0;

	int finishCount = 0;
	//int _index = 0;

	Vector2 originPoint;
	Vector2 space;

	void Start()
	{
		sideLength = _background.transform.localScale.x;
		space.x = sideLength / volumn;
		space.y = sideLength / raw;
		originPoint.x = -((volumn - 1) * space.x) / 2;
		originPoint.y = ((raw - 1) * space.y) / 2;
		Vector2 range;
		range.x = space.x * factor * 10f;
		range.y = space.y * factor * 10f;

		_tempPlaneAll = new GameObject[raw * volumn];
		int k = 0;
		for (int i = 0; i != raw; ++i)
		{
			for (int j = 0; j != volumn; ++j)
			{
				GameObject tempObj = (GameObject)Instantiate(_plane);
				tempObj.name = "Item" + k;
				tempObj.transform.parent = _planeParent.transform;
				tempObj.transform.localPosition = new Vector3((originPoint.x + space.x * j) * 10f, (originPoint.y - space.y * i) * 10f, 0);
				tempObj.transform.localScale = new Vector3(space.x, 1f, space.y);
				Vector2 tempPos = new Vector2(originPoint.x + space.x * j, originPoint.y - space.y * i);

				float offset_x = (tempPos.x <= 0 + Mathf.Epsilon) ? (0.5f - Mathf.Abs((tempPos.x - space.x / 2) / sideLength)) : (0.5f + (tempPos.x - space.x / 2) / sideLength);
				float offset_y = (tempPos.y <= 0 + Mathf.Epsilon) ? (0.5f - Mathf.Abs((tempPos.y - space.y / 2) / sideLength)) : (0.5f + (tempPos.y - space.y / 2) / sideLength);

				float scale_x = Mathf.Abs(space.x / sideLength);
				float scale_y = Mathf.Abs(space.y / sideLength);

				tempObj.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset_x, offset_y);
				tempObj.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scale_x, scale_y);
				tempObj.SendMessage("SetRange", range);

				_tempPlaneAll[k] = tempObj;
				++k;
			}
		}
	}

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 30), "Play"))
    //        StartGame();
    //    if (GUI.Button(new Rect(10, 80, 100, 30), "Next Textrue"))
    //        ChangeTex();
    //}

    public  void StartGame()
	{
		for (int i = 0; i != _tempPlaneAll.Length; ++i)
		{
			int tempRank = Random.Range(0, _RandomPos.Length);
			_tempPlaneAll[i].transform.localPosition = new Vector3(_RandomPos[tempRank].x, _RandomPos[tempRank].y, 0f);
		}
		gameObject.BroadcastMessage("Play");
		planeImage.SetActive(false);

	}

	public  void SetIsMoveFale()
	{
		gameObject.BroadcastMessage("IsMoveFalse");
	}

	public  void IsFinish()
	{
		++finishCount;
		if (finishCount == raw * volumn)
        {
			Debug.Log("Finish!");
			planeImage.SetActive(true);
		}
	}

	//public  void ChangeTex()
	//{
	//	_background.GetComponent<Renderer>().material.mainTexture = _texAll[_index];
	//	gameObject.BroadcastMessage("SetTexture", _texAll[_index++]);
	//	if (_index > _texAll.Length - 1)
	//		_index = 0;
	//}

}
