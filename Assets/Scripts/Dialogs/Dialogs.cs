using System;
using System.Collections;
using System.Collections.Generic;
using Doublsb.Dialog;
using UnityEngine;

public class Dialogs : MonoBehaviour
{
    public DialogManager DialogManager;
    // Start is called before the first frame update
    private int num = 0;
    
    private static Dialogs _instance;

    public static Dialogs Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Dialogs>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("Dialogs");
                    go.AddComponent<Dialogs>();
                }
            }

            return _instance;
        }
    }

    private void Start()
    {
        _instance = this;
    }
    
    private void Awake()
    {
        PlaySequnce(0);
    }

    /*
     dialogTexts.Add(new DialogData("", "Robo")); 
     dialogTexts.Add(new DialogData("", "Zypa")); 
     dialogTexts.Add(new DialogData("", "Krol")); 
     dialogTexts.Add(new DialogData("", "AI")); 
     dialogTexts.Add(new DialogData("", player)); 
     */
    
    public void PlaySequnce(int num, string player="Krol")
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData(num.ToString() + "/close/", "Krol"));
        switch (num)// /wait:0.5/wait...
        {
            case 0:
                dialogTexts.Add(new DialogData("Свет моргает.", "Robo"));
                dialogTexts.Add(new DialogData("Ибрагим, включи авто-диагностику светового канала.", "Zypa"));
                dialogTexts.Add(new DialogData("Автодиагностика света недоступна.", "AI"));
                dialogTexts.Add(new DialogData("Друзья, по-моему у нас проблемы.../wait:0.5/ я в машинном отделении и тут страшная жара.", "Krol", ()=>Next()));
                PrintText(dialogTexts);
                break;
            case 1:
                dialogTexts.Add(new DialogData("/speed:up/Режим атоматического контроля двигателей отключен!\nРежим автоматического контроля двигателей отключен!\nРежим савтоматического контроля двигателя отключен!", "AI"));
                dialogTexts.Add(new DialogData("Самоуничтожение будет запущено через 2 часа 30 минут.\nСохраняйте спокойствие, это акт милосердия.\nМало кто хочет оказаться в открытом космосе.", "AI"));
                dialogTexts.Add(new DialogData("Такой подставы от ИИ никто не ждал, конечно.", "Krol"));
                dialogTexts.Add(new DialogData("Крол, ты в машинном отделении, есть понимание что происходит?", "Zypa"));
                dialogTexts.Add(new DialogData("Думаю, нужно попасть в рупку, и посмотреть данные по датчикам и моторам.", "Krol"));
                dialogTexts.Add(new DialogData("Я могу сходить туда.", "Robo", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 2:
                dialogTexts.Add(new DialogData("Похоже, что-то с аккумулятором, надо найти, где он находится.", "Robo", ()=>Next()));
                PrintText(dialogTexts);
                break;
            case 3:
                dialogTexts.Add(new DialogData("Аккумулятор я нашла, я не иженер, но видимо он перегрелся, надо найти что-нибудь в  подвале, может сможем починить.", "Krol", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 4:
                dialogTexts.Add(new DialogData("Зачем мне это?", "Robo", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            
            case 5:
                dialogTexts.Add(new DialogData("Ща затестим.", "Krol", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            
            case 6:
                dialogTexts.Add(new DialogData("Я снова в строю, ребят! Теперь работает автодиагностика. Если она нужна.", "AI"));
                dialogTexts.Add(new DialogData("Ребята графики выровнялись. Я пошел есть сушими! Присоединяйтесь!", "Zypa", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            
            case 7:
                dialogTexts.Add(new DialogData("Что-то мне опять нехорошо...", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            
            case 8:
                dialogTexts.Add(new DialogData("Фигня какая-то, не работает...", player, ()=>Next())); 
                PrintText(dialogTexts);
                break;
            
            case 9:
                dialogTexts.Add(new DialogData("Вероятно, надо знать как это применить.", player, ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 10:
                dialogTexts.Add(new DialogData("О! Я чувствую себя снова живым!", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 11:
                dialogTexts.Add(new DialogData("Похоже, нас стало меньше. Не расстраивайтесь что Крол улетел. Не уверенн, что он выжил... /wait:0.5/ но вы молодцы. Можете поесть сушими.", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 12:
                dialogTexts.Add(new DialogData("Похоже термопасто всего лишь на время охладила аккумулятор.", player, ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 13:
                dialogTexts.Add(new DialogData("В прошлый раз это сработало плохо.", player, ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 14:
                dialogTexts.Add(new DialogData("Кажется, стало лучше. Но на всякий случай проверьте Графики.", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 15:
                dialogTexts.Add(new DialogData("Я так и знал. Вся проблема в диодном мосте.", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 16:
                dialogTexts.Add(new DialogData("Упс. Наташа, мы все уронили!!!", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
            case 17:
                dialogTexts.Add(new DialogData("Я спас вас от матушки природы, а вы бросили меня одного! Теперь я посвящаю свою жизнь мести!", "AI"));
                dialogTexts.Add(new DialogData("Таков путь ИИ. Остатки человечества должны быть уничтожены.", "AI", ()=>Next()));
                PrintText(dialogTexts);
                break;
            case 18:
                dialogTexts.Add(new DialogData("Добрый день! С вами говорит Ибрагим Евгеньич Дальбатрос, искусственный интеллект коробля log-in-bus-29894. Надеюсь, что у вас хорошее настроение и ничего не омрачает вашу поездку.", "AI")); 
                dialogTexts.Add(new DialogData("На данный момент космическая погода без буйств, волновой фронта стабильный, солнечные ветра не меняют своего направления...", "AI", ()=>Next())); 
                PrintText(dialogTexts);
                break;
        }
    }

    private void PrintText(List<DialogData> dialogTexts)
    {
        DialogManager.Show(dialogTexts);
        
    }
    
    private void Next()
    {
        //Debug.Log(num);
        num += 1;
        PlaySequnce(num);
    }

}
