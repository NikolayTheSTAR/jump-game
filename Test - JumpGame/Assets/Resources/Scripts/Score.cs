using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static int s;
    private static Text text;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            s = PlayerPrefs.GetInt("Score");
        }
        else s = 0;

        text = GetComponent<Text>();

        UpdateText();
    }

    // добавление очка
    public static void AddScore()
    {
        s++;
        UpdateText();

        PlayerPrefs.SetInt("Score", s);
    }

    // обновление текста
    private static void UpdateText()
    {
        text.text = Convert.ToString(s);
    }
}