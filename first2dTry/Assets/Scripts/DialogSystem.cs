using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    bool textFinish;//是否完成打字
    bool cancelTyping;//取消打字
    List<string> textList = new List<string>();

    
    void Awake()
    {
        GetTextFromFile(textFile);
    }
    private void OnEnable()
    {
        index = 0;
        textFinish=true;
        //textLabel.text = textList[0];
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (index >= textList.Count )
            {
                gameObject.SetActive(false);
                index = 0;
                return;
            }

            //textLabel.text = textList[++index];
            if (textFinish && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinish && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
       var lineDate= file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }

    }
    public void Touch()
    {
        if (index >= textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        //textLabel.text = textList[++index];
        if (textFinish && !cancelTyping)
        {
            StartCoroutine(SetTextUI());
        }
        else if (!textFinish && !cancelTyping)
        {
            cancelTyping = true;
        }

    }
    IEnumerator SetTextUI()
    {
        textFinish = false;
        textLabel.text = "";
        //for(int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];
        //    yield return new WaitForSeconds(textSpeed);
        //}
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinish = true;
        index++;
    }
}
