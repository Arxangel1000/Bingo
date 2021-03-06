﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Book : MonoBehaviour {

    [SerializeField] private string loadBook = "MainBook"; // имя текстового файла
    [SerializeField] private string booksPath = "Books"; // имя папки с текстовым файлов в Resources
    [SerializeField] private Text leftPage; // левая страница
    [SerializeField] private Text rightPage; // правая страница
    [SerializeField] private Text LPN; // номер левой страницы
    [SerializeField] private Text RPN; // номер правой страницы
    [SerializeField] private Button nextButton; // листаем вперед
    [SerializeField] private Button prevButton; // листаем назад
    private List<string> pagesList = new List<string>();
    [SerializeField] private int pageCount;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton = GameObject.Find("closeButton").GetComponent<Button>();
        closeButton.interactable = false;
    }

    void Start()
    {
        nextButton.onClick.AddListener(() => { Next(); });
        prevButton.onClick.AddListener(() => { Prev(); });
        leftPage.rectTransform.sizeDelta = rightPage.rectTransform.sizeDelta;
       
        Calculate(loadBook);
        pageCount += 2;
        SetPages();
    }

    void SetPages()
    {
        leftPage.text = pagesList[pageCount];
        rightPage.text = (pageCount + 1 > pagesList.Count - 1) ? string.Empty : pagesList[pageCount + 1];
        LPN.text = (pageCount + 1).ToString();
        RPN.text = (pageCount + 2).ToString();
    }
    // Листаем вперед
    void Next()
    {
        pageCount += 2;
        SetPages();
        prevButton.interactable = true;
        if (pageCount + 1 >= pagesList.Count - 1)
        {
            nextButton.interactable = false;
            closeButton.interactable = true;
        }
       
    }
    // Листаем назад
    void Prev()
    {
        pageCount -= 2;
        if (pageCount == 0)
        {
            prevButton.interactable = false;
            closeButton.interactable = false;
        }
        if (pageCount < 0)
        {
            leftPage.text = string.Empty;
            rightPage.text = string.Empty;
            LPN.text = string.Empty;
            RPN.text = string.Empty;
            prevButton.interactable = false;
            return;
        }

        SetPages();
        nextButton.interactable = true;
    }
    // Метод закрытия книги
     public void CloseBook() 
    {
        
            Destroy(this.gameObject);
    }

    void Calculate(string fileName)
    {
        TextAsset binary = Resources.Load<TextAsset>(booksPath + "/" + fileName);

        if (binary != null && !string.IsNullOrEmpty(binary.text))
        {
            pagesList.Clear();
            pagesList = Pages(binary.text, leftPage);
            leftPage.text = string.Empty;
            rightPage.text = string.Empty;
            LPN.text = string.Empty;
            RPN.text = string.Empty;
            nextButton.interactable = true;
            prevButton.interactable = false;
            pageCount = -2;
        }
    }

    List<string> Pages(string text, Text page) // определяем на сколько страниц нужно разбить текст
    {
        if (string.IsNullOrEmpty(text) || page == null) return new List<string>();

        TextGenerationSettings settings = page.GetGenerationSettings(page.rectTransform.rect.size);
        TextGenerator textGenerator = new TextGenerator();

        string current = text;
        int index = 0;
        List<string> pages = new List<string>();

        while (current.Length != 0)
        {
            textGenerator.Populate(current, settings);
            index = textGenerator.characterCountVisible;
            pages.Add(current.Substring(0, index));
            current = current.Substring(index).Trim();
        }

        return pages;
    }
}
