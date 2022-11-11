using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class RebindKeys : MonoBehaviour
{
    private PlayerInput playerInput;
    private GameObject rebindWindow;
    private TMP_Text actionToRebind;
    private TMP_Text boundKey;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("player").GetComponent<PlayerInput>();
        boundKey = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        rebindWindow = GameObject.Find("Rebind").transform.GetChild(0).gameObject;
        actionToRebind = rebindWindow.transform.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void RebindKey()
	{
        // clicked button's name
        string[] splitName = gameObject.name.Split('_');
        string actionToBind = splitName[0];
        int index = int.Parse(splitName[1]);
        InputAction inputAction = playerInput.actions.FindAction(actionToBind);

        // activate overlay
        rebindWindow.SetActive(true);
        actionToRebind.text = "Rebinding\n" + actionToBind;

        // button name is used to find the action. Esc to cancel rebind
        playerInput.currentActionMap.Disable();
        inputAction.PerformInteractiveRebinding(index).WithCancelingThrough("<Keyboard>/escape").OnCancel(callback => {
            callback.Dispose();
            playerInput.currentActionMap.Enable();
        }).OnComplete(callback =>
        {
            callback.Dispose();
            boundKey.text = inputAction.bindings[index].ToDisplayString();
            playerInput.currentActionMap.Enable();
            rebindWindow.SetActive(false);

            // save the changed keybind in playerprefs (saved somewhere externally on the pc)
            inputAction.bindings[index].ToDisplayString(out string deviceLayout, out string controlPath);
            PlayerPrefs.SetString(actionToBind + index, "<" + deviceLayout + ">/" + controlPath);
            PlayerPrefs.Save();
        }).Start();
    }
}
