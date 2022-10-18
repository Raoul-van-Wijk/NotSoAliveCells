using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownRes : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    public class Resolution
	{
        public int width;
        public int height;
        public int refreshRate;

        public Resolution(int width, int height, int refreshRate)
		{
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }
    }

    // list which will be filled at with all possible resolutions for better indexing (than checking the index of the dropdown since those are saved as strings)
    private List<Resolution> resolutionList = new();

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        // clear all options just in case
        dropdown.ClearOptions();

        // adds all available resolutions to the dropdown list
        foreach (var res in Screen.resolutions)
        {
            dropdown.AddOptions(new List<string>() { res.width + "x" + res.height + " " + res.refreshRate + "Hz"});
            resolutionList.Add(new Resolution(res.width, res.height, res.refreshRate));
        }

        // selects the current resolution
        dropdown.value = dropdown.options.FindIndex(option => option.text == Screen.currentResolution.width + "x" + Screen.currentResolution.height + " " + Screen.currentResolution.refreshRate + "Hz");

        dropdown.onValueChanged.AddListener((v) =>
        {
            Screen.SetResolution(resolutionList[v].width, resolutionList[v].height, Screen.fullScreen, resolutionList[v].refreshRate);
        });
    }
}
