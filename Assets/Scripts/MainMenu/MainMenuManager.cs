using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    private string savePath; // string to hold the file's save path

    public GameObject settingsPanel; // reference to panel settings 
    public Button continueButton; // reference to unity button to let it change from code
    
    private void Start() // runs at the start of the game
    {
        savePath = Application.persistentDataPath + "/save.json"; // file save path

        if (continueButton != null) // if the continueButton exists
        {
            continueButton.interactable = File.Exists(savePath); 
            // makes it interactible if there is a save file that exists on the machine
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
    }

    public void ToggleSettings() // toggles settings panel
    {
        if (settingsPanel != null) // if there is a panel
        {
            bool isActive = settingsPanel.activeSelf; // if its active then 
            settingsPanel.SetActive(!isActive);
        }
    }
}
