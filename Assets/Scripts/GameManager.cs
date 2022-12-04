using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Add to this array all of the cases or individuals that the player should have to deal with
    public Individual[] allIndividuals;

    // Once the player makes a decision, we throw the individual that has been decided on into one of these two arrays.
    public Individual[] allAcceptedIndividuals;
    public Individual[] allRejectedIndividuals;

    // The current individual that the player must decide on
    public Individual currentIndividual;
    
    // How many individuals should be evaluated before we update the date UI?
    public int howManyIndividualsPerDay = 1;

    //// UI REFERENCES ////
    public Text currentIndividualDescrptionWidget;
    public Text currentIndividualDescrptionWidget2;
    public RawImage currentIndividualImageWidget;
    public RawImage currentIndividualImageWidget2;

    public GameObject InGameDisplay;
    public GameObject PostGameDisplay;

    public Text currentGameDayTextReference;
    
    //// PRIVATE HELPER VARIABLES ////
    private int _numberOfIndividualsForToday;
    private int _currentDay = 1;
    
    // We need to know which individual we are currently on in the array
    private int _currentIndividualIndex;

    public Text CharacterResults;

    public bool gameIsOver;

    private int postGameCharacterIndex;

    // Start is called before the first frame update
    void Start()
    {
        allAcceptedIndividuals = new Individual[allIndividuals.Length];
        allRejectedIndividuals = new Individual[allIndividuals.Length];

        // Assign the reference of the GameManager to every single individual we have
        foreach (var thisIndividual in allIndividuals)
        {
            thisIndividual.gameManager = this;
        }

        // Pick our first individual
        PickNewIndividual();
        
        // Default the currentGameDayText to Day: 1
        currentGameDayTextReference.text = String.Concat("Day: ", _currentDay);

        // Update the remaining individual UI elements
        UpdateIndividualUIElements();
        gameIsOver = false;
        postGameCharacterIndex = 0;
    }

    void Update()
    {
        if(!gameIsOver || postGameCharacterIndex >= allIndividuals.Length)
            return;
        
        if(Input.GetMouseButtonDown(0))
            nextCharacterResults();
    }

    // The code that should run upon an individual being accepted
    public void OnButton_AcceptIndividual()
    {
        allAcceptedIndividuals[GetNextEmptyIndex(allAcceptedIndividuals)] = currentIndividual;
        _numberOfIndividualsForToday++;
        currentIndividual.entranceAccepted = true;
        
        // Check to see if we should switch to the next day
        CheckToSwitchDay();
        
        PickNewIndividual();
    }

    // The code that should run upon an individual being rejected
    public void OnButton_RejectIndividual()
    {
        allRejectedIndividuals[GetNextEmptyIndex(allRejectedIndividuals)] = currentIndividual;
        _numberOfIndividualsForToday++;
        currentIndividual.entranceAccepted = false;
        
        // Check to see if we should switch to the next day
        CheckToSwitchDay();
        
        PickNewIndividual();
    }

    private void PickNewIndividual()
    {
        if (_currentIndividualIndex >= allIndividuals.Length)
        {
            HideAllInGameUI();
            
            // We should also now transition to the end game summary UI
            ShowEndGameSummaryUI();
            
            return;
        }
        
        currentIndividual = allIndividuals[_currentIndividualIndex];
        _currentIndividualIndex++;
        
        UpdateIndividualUIElements();
    }

    private void CheckToSwitchDay()
    {
        if (_numberOfIndividualsForToday >= howManyIndividualsPerDay)
        {
            _numberOfIndividualsForToday = 0;
            currentGameDayTextReference.text = String.Concat("Day: ", ++_currentDay);
        }
    }

    // Finds the next empty index in our array, a basic private helper method
    private int GetNextEmptyIndex(Individual[] arrayToCheck)
    {
        for (int i = 0; i < arrayToCheck.Length; i++)
        {
            if (arrayToCheck[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    private void UpdateIndividualUIElements()
    {
        currentIndividualDescrptionWidget.text = currentIndividual.individualNameString + "\n" + currentIndividual.individualDescription;
        currentIndividualDescrptionWidget2.text = "- " + currentIndividual.individualPreviousOccupations + "\n"
                                                        + "- " + currentIndividual.individualHistory + "\n"
                                                        + "- " + currentIndividual.individualImmigrationReason + "\n"
                                                        + "- " + currentIndividual.additionalNotes;
        currentIndividualImageWidget.texture = currentIndividual.individualIcon;
        currentIndividualImageWidget2.texture = currentIndividual.individualIcon2;

        // We should update the rest of the variables in the Individual class here
    }

    private void HideAllInGameUI()
    {
        gameIsOver = true;
        InGameDisplay.SetActive(false);
        PostGameDisplay.SetActive(true);
    }

    private void ShowEndGameSummaryUI()
    {
        changePostText();
    }

    public void nextCharacterResults()
    {
        if(postGameCharacterIndex >= (allIndividuals.Length - 1))
        {
            CharacterResults.text = "That's all folks!";
            CharacterResults.color = Color.white;
            return;
        }

        postGameCharacterIndex++;
        changePostText();
    }
    
    public void changePostText()
    {
        CharacterResults.text = allIndividuals[postGameCharacterIndex].characterResult;

        if(allIndividuals[postGameCharacterIndex].entranceAccepted)
            CharacterResults.color = Color.green;
        else
            CharacterResults.color = Color.red;
    }
}
