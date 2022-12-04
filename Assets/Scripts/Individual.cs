using UnityEngine;

public class Individual : MonoBehaviour
{
    // A reference to the GameManager in the scene
    public GameManager gameManager;
    
    public Texture individualIcon;
    public Texture individualIcon2;

    // Just the basic name of the individual
    public string individualNameString;
    
    //// INDIVIDUAL ATTRIBUTES ////
    // Ex: "5' 7""
    public string individualNameHeight;
    
    // Ex: "Brown hair, darkish brown eyes"
    public string individualDescription;
    
    // Ex: "Annoyed/Irritated"
    public string individualFacialExpression;
    
    // What is the correct answer for whether or not this individual should have been let in or not?
    public bool shouldBeLetIn = false;
    
    //// INDIVIDUAL BIOGRAPHY ////
    // What were the previous occupations of this individual?
    public string individualPreviousOccupations;
    
    public string individualHistory;

    public string individualImmigrationReason;

    public string additionalNotes;

    public bool entranceAccepted;
    public string acceptedResult;
    public string declinedResult;
    public string characterResult { get { return entranceAccepted? acceptedResult : declinedResult; } }
}