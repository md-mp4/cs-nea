using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // pause menu game object reference
    public InventoryManager inventory; // inventory manager reference to let us save and load
    private bool isPaused = false; // boolean to check if the game is paused or not

    private void Update() // runs every frame to check for key presses
    {
        var keyboard = Keyboard.current; // getting a reference to the keyboard
    
        if (keyboard == null) return; // if no keyboard is plugged in doesn't run this

        if (Keyboard.current.escapeKey.wasPressedThisFrame) // if escape was pressed this frame
        {
            if (isPaused) // if the game is paused
            {
                Resume(); // unpauses
            }
            else // if the game is unpaused
            {
                Pause(); // pauses
            }
        }
    }

    public void Pause() // pauses game
    {
        pausePanel.SetActive(true); // activates pause panel
        Time.timeScale = 0f; // freezes game
        isPaused = true; // enables isPaused so the game knows what to do next
    }

    public void Resume() // unpauses game
    {
        pausePanel.SetActive(false); // disables pause panel
        Time.timeScale = 1f; // lets time flow
        isPaused = false; // disabled isPaused so the game knows what to do next
    }

    public void SaveAndExit() // saves the game then exits to main menu
    {
        if (inventory != null) // if there is an existing inventory
        {
            inventory.SaveGame(); // saves the game
        }

        Time.timeScale = 1f; // sets the time to flow normally
        SceneManager.LoadScene(0); // sends back to main menu
    }

    public void QuitNoSave() // quits the game without saving anything
    {
        Time.timeScale = 1f; // sets time to flow normally
        SceneManager.LoadScene(0); // sends back to main menu
    }
}
