using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private string savePath; // string to hold the file's save path

    public TMP_Text buttonText; // reference to button's text
    public GameObject settingsPanel; // reference to panel settings 
    public Button continueButton; // reference to unity button to let it change from code
    
    private void Start() // runs at the start of the game
    {
        savePath = Application.persistentDataPath + "/save.json"; // file save path

        if (continueButton != null) // if the continueButton exists
        {
            continueButton.interactable = File.Exists(savePath); 
            // makes it interactible if there is a save file that exists on the machine

            if (continueButton.interactable) // if the button is interactible
            {
                buttonText.color = Color.white; // white button
            }
            else
            {
                buttonText.color = new Color(1f, 1f, 1f, 0.25f); // changes colour if button disabled
            }
        }
    }

    public void NewGame() // starts a new game
    {
        InventoryManager.ShouldLoadOnStart = false; // stops inventory manager from loading anything

        if (File.Exists(savePath)) File.Delete(savePath); // deletes old save to start a new game fully

        SceneManager.LoadScene(1); // loads the game scene
    }

    public void ContinueGame() // loads the saved progress
    {
        InventoryManager.ShouldLoadOnStart = true; // has the inventory manager load up the save on start
        SceneManager.LoadScene(1); // loads the game scene
    }

    public void QuitGame() // quits the game
    {
        Application.Quit(); // quits the game
        Debug.Log("Game exited"); // debug log to see if it works
    }

    public void ToggleSettings() // toggles settings panel
    {
        if (settingsPanel != null) // if there is a panel
        {
            bool isActive = settingsPanel.activeSelf; // if panel is active, isActive is true
            settingsPanel.SetActive(!isActive); // if isActive is true, its disabled, vice versa
        }
    }
}
