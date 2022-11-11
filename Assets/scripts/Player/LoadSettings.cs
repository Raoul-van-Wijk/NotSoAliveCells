using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class LoadSettings : MonoBehaviour
{
    private PlayerInput playerInput;
	[SerializeField] private TMP_Text[] rebindTextArray;

	// Option menu ui elements
	[SerializeField] private Image buttonImageSFX;
	[SerializeField] private Image buttonImageBGM;
	[SerializeField] private Slider sliderSFX;
	[SerializeField] private Slider sliderBGM;
	[SerializeField] private TMP_InputField sliderTextSFX;
	[SerializeField] private TMP_InputField sliderTextBGM;
	private MuteSFX muteSFX;
	private MuteBGM muteBGM;

	// Start is called before the first frame update
	void Start()
    {
        playerInput = GetComponent<PlayerInput>();
		muteSFX = buttonImageSFX.GetComponent<MuteSFX>();
		muteBGM = buttonImageBGM.GetComponent<MuteBGM>();

		// volume stuffs
		int volumeSFX = PlayerPrefs.GetInt("VolumeSFX");
		int volumeBGM = PlayerPrefs.GetInt("VolumeBGM");
		int mutedSFX = PlayerPrefs.GetInt("MutedSFX");
		int mutedBGM = PlayerPrefs.GetInt("MutedBGM");
		if (PlayerPrefs.HasKey("VolumeSFX"))
		{
			sliderTextSFX.text = volumeSFX + "";
			sliderSFX.value = volumeSFX;
			AudioManager.Instance.volumeSFX = volumeSFX / 100f;
		}
		if (PlayerPrefs.HasKey("VolumeBGM"))
		{
			sliderTextBGM.text = volumeBGM + "";
			sliderBGM.value = volumeBGM;
			AudioManager.Instance.volumeBGM = volumeBGM / 100f;
			AudioManager.Instance.ChangeBGMVolume();
		}
		if (PlayerPrefs.HasKey("MutedSFX"))
		{
			if (mutedSFX == 1)
			{
				muteSFX.ToggleMute();
				AudioManager.Instance.muteSFX = true;
			}
		}
		if (PlayerPrefs.HasKey("MutedBGM"))
		{
			if (mutedBGM == 1)
			{
				muteBGM.ToggleMute();
				AudioManager.Instance.muteBGM = true;
			}
		}

		// loop through every action available
		foreach (InputAction action in playerInput.actions)
		{
			// movement uses different indexing, as such an exception is made
			if (action.name != "Movement")
			{
				// each action has 2 binds, both binds are retrieved from playerprefs (previously stored when key was rebound in RebindKeys.cs
				string binding1 = PlayerPrefs.GetString(action.name + "0");
				string binding2 = PlayerPrefs.GetString(action.name + "1");
				
				// binding exists, binding does not exist if player has never changed their binds
				if (binding1.Length > 0)
				{
					action.ApplyBindingOverride(0, binding1);
					SetText(0, action);
				}
				if (binding2.Length > 0)
				{
					action.ApplyBindingOverride(1, binding2);
					SetText(1, action);
				}
			} else
			{
				string binding1 = PlayerPrefs.GetString(action.name + "1");
				string binding2 = PlayerPrefs.GetString(action.name + "2");
				string binding3 = PlayerPrefs.GetString(action.name + "4");
				string binding4 = PlayerPrefs.GetString(action.name + "5");
				if (binding1.Length > 0)
				{
					action.ApplyBindingOverride(1, binding1);
					SetText(1, action);
				}
				if (binding2.Length > 0)
				{
					action.ApplyBindingOverride(2, binding2);
					SetText(2, action);
				}
				if (binding3.Length > 0)
				{
					action.ApplyBindingOverride(4, binding3);
					SetText(4, action);
				}
				if (binding4.Length > 0)
				{
					action.ApplyBindingOverride(5, binding4);
					SetText(5, action);
				}
			}
		}
	}

	// Sets the text in the options menu
	private void SetText(int index, InputAction action)
	{
		foreach (TMP_Text textField in rebindTextArray)
		{
			if (textField.name.Split("_")[0] == action.name + index)
			{
				textField.text = action.bindings[index].ToDisplayString();
			}
		}
	}
}
