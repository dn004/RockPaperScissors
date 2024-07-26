using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Operations : MonoBehaviour
{
    [SerializeField] private Button bRock;
    [SerializeField] private Button bPaper;
    [SerializeField] private Button bScissors;
    [SerializeField] private Button bShoot;
    [SerializeField] private Button bReplay;

    [SerializeField] private TMP_Text UserChoice;
    [SerializeField] private TMP_Text CompChoice;
    [SerializeField] private TMP_Text Verdict;


    [SerializeField] private GameObject OpeningPage;
    [SerializeField] private GameObject ChoicesPage;
    [SerializeField] private GameObject WinnersPage;

    private Choice compChoice;
    private Choice userChoice;

    private enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    private void Start()
    {
        OnOpening();

        bRock.onClick.AddListener(() => ButtonListener(Choice.Rock));
        bPaper.onClick.AddListener(() => ButtonListener(Choice.Paper));
        bScissors.onClick.AddListener(() => ButtonListener(Choice.Scissors));

        bShoot.onClick.AddListener(() => ShootPressed());
        bReplay.onClick.AddListener(() => ReplayPressed());
    }


    // Player Choice
    private void ButtonListener(Choice choice)
    {
        userChoice = choice;
        
        Debug.Log("User choice: " + userChoice);
    }


    // Computer Choice
    public void GenerateCompChoice()
    {
        Choice[] choices = new Choice[] { Choice.Rock, Choice.Paper, Choice.Scissors };
        compChoice = choices[Random.Range(0, choices.Length)];
        Debug.Log("Computer choice: " + compChoice);
    }

    public void ShootPressed()
    {
        OnChoices();

        GenerateCompChoice();
        
        UserChoice.text = userChoice.ToString();
        CompChoice.text = compChoice.ToString();

        WhoWins();

        StartCoroutine(LoadSceneAfterDelay(3f));
    }

    public void WhoWins()
    {
        if (compChoice == userChoice)
        {
            Verdict.text = "IT'S A TIE";
        }
        else if ((compChoice == Choice.Rock && userChoice == Choice.Scissors) ||
                 (compChoice == Choice.Paper && userChoice == Choice.Rock) ||
                 (compChoice == Choice.Scissors && userChoice == Choice.Paper))
        {
            Verdict.text = "YOU LOSE!";
        }
        else
        {
            Verdict.text = "YOU WIN!";
        }
        
        Debug.Log(Verdict.text);
    }

    public void ReplayPressed()
    {
        userChoice = Choice.Rock;
        OnOpening();

    }


    public void OnOpening()
    {
        OpeningPage.SetActive(true);
        ChoicesPage.SetActive(false);
        WinnersPage.SetActive(false);

    }

    public void OnChoices()
    {
        OpeningPage.SetActive(false);
        ChoicesPage.SetActive(true);
        WinnersPage.SetActive(false);

    }

    public void OnWinners()
    {
        OpeningPage.SetActive(false);
        ChoicesPage.SetActive(false);
        WinnersPage.SetActive(true);

    }



    IEnumerator LoadSceneAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        OnWinners();
    }
}
