using UnityEngine;
using UnityEngine.UI;



    public class ImageCreater : MonoBehaviour
    {
        public static ImageCreater _instance;
        //存储裁剪好图片的数组.
        //public Sprite[] sprites;
        //格子的预设体.
        //public GameObject cellPrefab;

        public GameObject xinZang;
        void Start()
        {
            _instance = this;
            //CreateImages();
        }
        
        //public  void CreateImages()
        //{
        //    //将图片数组随机排列.
        //    GameManager.RandomArray(sprites);
        //    //生产图片.
        //    for (int i = 0; i < sprites.Length; i++)
        //    {
        //        //通过预设体生成图片.
        //        GameObject cell = (GameObject)Instantiate(cellPrefab);
        //        //设置cell的名字方便检测是否完成拼图.
        //        cell.name = i.ToString();
        //        //获取cell的子物体.
        //        Transform image = cell.transform.GetChild(0);
        //        //设置显示的图片.
        //        image.GetComponent<Image>().sprite = sprites[i];
        //        //设置子物体的名称，方便检测是否完成拼图.
        //        int tempIndex = sprites[i].name.LastIndexOf('_');
        //        image.name = sprites[i].name.Substring(tempIndex + 1);
        //        //将Cell设置为Panel的子物体.
        //        cell.transform.SetParent(this.transform);
        //        //初始化大小.
        //        cell.transform.localScale = Vector3.one;

        //        //设置旋转
        //        //cell.transform.GetChild(0).rotation = Quaternion.Euler(0,0,90 * Random.Range(0, 3));
        //    }
        //}
    }
