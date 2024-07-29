using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Operations : MonoBehaviour
{
    // Buttons for user interactions
    [SerializeField] private Button bRock;
    [SerializeField] private Button bPaper;
    [SerializeField] private Button bScissors;
    [SerializeField] private Button bShoot;
    [SerializeField] private Button bReplay;

    // Text fields to display choices and results
    [SerializeField] private TMP_Text UserChoice;
    [SerializeField] private TMP_Text CompChoice;
    [SerializeField] private TMP_Text Verdict;

    // Pages to display different stages of the game
    [SerializeField] private GameObject OpeningPage;
    [SerializeField] private GameObject ChoicesPage;
    [SerializeField] private GameObject WinnersPage;

    // Variables to store choices
    private Choice compChoice;
    private Choice userChoice;

    // Enumeration of possible choices
    private enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    // Called when the script is initialized
    private void Start()
    {
        // Show the opening page
        OnOpening();

        // Assign functions to button clicks
        bRock.onClick.AddListener(() => ButtonListener(Choice.Rock));
        bPaper.onClick.AddListener(() => ButtonListener(Choice.Paper));
        bScissors.onClick.AddListener(() => ButtonListener(Choice.Scissors));

        bShoot.onClick.AddListener(() => ShootPressed());
        bReplay.onClick.AddListener(() => ReplayPressed());
    }

    // Function called when a choice button is clicked
    private void ButtonListener(Choice choice)
    {
        userChoice = choice;
        Debug.Log("User choice: " + userChoice);
    }

    // Function to generate a random choice for the computer
    public void GenerateCompChoice()
    {
        Choice[] choices = new Choice[] { Choice.Rock, Choice.Paper, Choice.Scissors };
        compChoice = choices[Random.Range(0, choices.Length)];
        Debug.Log("Computer choice: " + compChoice);
    }

    // Function called when the shoot button is pressed
    public void ShootPressed()
    {
        // Show the choices page
        OnChoices();

        // Generate and display choices
        GenerateCompChoice();
        UserChoice.text = userChoice.ToString();
        CompChoice.text = compChoice.ToString();

        // Determine and display the result
        WhoWins();

        // Switch to winners page after a delay
        StartCoroutine(LoadSceneAfterDelay(3f));
    }

    // Function to determine the winner
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

    // Function called when the replay button is pressed
    public void ReplayPressed()
    {
        userChoice = Choice.Rock; // Reset the user's choice
        OnOpening(); // Show the opening page
    }

    // Function to show the opening page
    public void OnOpening()
    {
        OpeningPage.SetActive(true);
        ChoicesPage.SetActive(false);
        WinnersPage.SetActive(false);
    }

    // Function to show the choices page
    public void OnChoices()
    {
        OpeningPage.SetActive(false);
        ChoicesPage.SetActive(true);
        WinnersPage.SetActive(false);
    }

    // Function to show the winners page
    public void OnWinners()
    {
        OpeningPage.SetActive(false);
        ChoicesPage.SetActive(false);
        WinnersPage.SetActive(true);
    }

    // Coroutine to delay switching to the winners page
    IEnumerator LoadSceneAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnWinners();
    }
}
