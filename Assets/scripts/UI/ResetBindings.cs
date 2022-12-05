using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private TMP_Text[] rebindTextArray;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GameObject.Find("player").GetComponent<PlayerInput>();
    }

    public void ResetKeyBindings()
	{
		// loop through every action available
		foreach (InputAction action in playerInput.actions)
		{
			// default every binding
			action.ApplyBindingOverride(default);


			// movement uses different indexing, as such an exception is made
			if (action.name != "Movement")
			{
				SetTextNormal(action);

				// delete playerprefs file
				for (int x = 1; x < 3; x++)
					PlayerPrefs.DeleteKey(action.name + x);
			}
			else
			{
				SetTextMovement(action);
				for (int x = 1; x < 6; x++)
					PlayerPrefs.DeleteKey(action.name + x);
			}
		}
	}

	// Sets the text in the options menu
	private void SetTextNormal(InputAction action)
	{
		foreach (TMP_Text textField in rebindTextArray)
		{
			if (textField.name.Split("_")[0] == action.name + "0")
				textField.text = action.bindings[0].ToDisplayString();
			else if (textField.name.Split("_")[0] == action.name + "1")
				textField.text = action.bindings[1].ToDisplayString();
		}
	}

	private void SetTextMovement(InputAction action)
	{
		foreach (TMP_Text textField in rebindTextArray)
		{
			if (textField.name.Split("_")[0] == action.name + "1")
				textField.text = action.bindings[1].ToDisplayString();
			else if (textField.name.Split("_")[0] == action.name + "2")
				textField.text = action.bindings[2].ToDisplayString();
			else if (textField.name.Split("_")[0] == action.name + "4")
				textField.text = action.bindings[4].ToDisplayString();
			else if (textField.name.Split("_")[0] == action.name + "5")
				textField.text = action.bindings[5].ToDisplayString();
		}
	}
}
