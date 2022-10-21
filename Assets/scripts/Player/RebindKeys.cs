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
        actionToRebind = rebindWindow.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        //rebindWindow = GameObject.Find("RebindWindow");
        //actionToRebind = rebindWindow.transform.Find("ActionToRebind").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        //Debug.Log(playerInput.actions.FindAction("Movement").bindings);
        //Debug.Log(playerInput.actions.FindAction(actionToBind).bindings[0].isComposite);

        // button name is used to find the action. Esc to cancel rebind
        playerInput.currentActionMap.Disable();
        inputAction.PerformInteractiveRebinding(index).WithCancelingThrough("<Keyboard>/escape").OnCancel(callback => {
            callback.Dispose();
            playerInput.currentActionMap.Enable();
            rebindWindow.SetActive(false);
        }).OnComplete(callback =>
        {
            callback.Dispose();
            boundKey.text = inputAction.bindings[index].ToDisplayString();
            playerInput.currentActionMap.Enable();
            rebindWindow.SetActive(false);
        }).Start();
    }
}
