using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Xml.Serialization;
using UnityEngine.Events;

public class Main : MonoBehaviour
{
    //关卡列表
    private List<Level> m_levels;

    public Button CancleButton;
    private UnityAction OnBackButton = null;
    void Start()
    {
        /*
         * 
         */
        OnBackButton = () => { Application.LoadLevel(0);};
        CancleButton.onClick.AddListener(OnBackButton);
        //获取关卡
        m_levels = LevelSystem.LoadLevels();
        //动态生成关卡
        int i = 0;
        foreach (Level l in m_levels)
        {
            GameObject prefab = (GameObject)Instantiate((Resources.Load("Level") as GameObject));
            //数据绑定
            DataBind(prefab, l);
            //设置父物体
            prefab.transform.SetParent(GameObject.Find("Canvas/Background/LevelPanel").transform);
//            prefab.transform.localPosition = new Vector3(m_levels.Count/3*100+140, m_levels.Count%3*100-140, 0);
            prefab.GetComponent<RectTransform>().localPosition = new Vector3(i%3*100-100,-i/3*100+100, 0);
            i++;
            prefab.transform.localScale = new Vector3(1, 1, 1);
            //将关卡信息传给关卡
            prefab.GetComponent<LevelEvent>().level = l;
            prefab.name = l.Name;
        }
    }

    /// <summary>
    /// 数据绑定
    /// </summary>
    void DataBind(GameObject go, Level level)
    {
        //为关卡绑定关卡名称
        go.transform.Find("LevelName").GetComponent<Text>().text = level.Name;
        //为关卡绑定关卡图片
        Texture2D tex2D;
        if (level.UnLock)
        {
            tex2D = Resources.Load("nolocked") as Texture2D;
        }
        else
        {
            tex2D = Resources.Load("locked") as Texture2D;
        }
        Sprite sprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0.5F, 0.5F));
        go.transform.GetComponent<Image>().sprite = sprite;
    }
}
