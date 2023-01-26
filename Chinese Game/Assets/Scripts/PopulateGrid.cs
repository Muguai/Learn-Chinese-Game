using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class PopulateGrid : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector
	private List<string> stringChecker = new List<string>();
	private List<string> AllCharactersAndstring = new List<string>();

	private string fileName = "ChineseChar.txt";
	private string result = "FAILED-FAILED-FAILED-FAILED-FAILED";


	void Awake()
    {
		if (this.gameObject.name == "Content1")
		{
			//StartCoroutine(loadStreamingAsset(fileName));
		}
	}
	void Start()
	{
		if(this.gameObject.name == "Content1")
        {
			//	string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Data");
			//    filePath = System.IO.Path.Combine(filePath, fileName);
			var lines1 = result.Split("\n"[0]);

			if (lines1.Length < 2)
			{
				TextAsset txt = (TextAsset)Resources.Load("ChineseChar");
				Debug.Log(txt.text);
				result = txt.text;
			}
			
			lines1 = result.Split("\n"[0]);


			if (lines1.Length < 2)
			{
				StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/" + fileName);
				string fileContents = sr.ReadToEnd();
				sr.Close();
				result = fileContents;
			}



			var lines = result.Split("\n"[0]);
			foreach (string l in lines)
			{
				AllCharactersAndstring.Add(l);
			}
		}
		Populate();
	}

	IEnumerator loadStreamingAsset(string fileName)
	{
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

		if (filePath.Contains("://") || filePath.Contains(":///"))
		{
			WWW www = new WWW(filePath);
			yield return www;
			result = www.text;
		}
        else
        {
			result = System.IO.File.ReadAllText(filePath);
		}
	}


	public void ExternalPopulate(GameObject newObj)
    {
		GameObject differentObj;
		Debug.Log("add " + newObj);

		if(stringChecker.Contains(newObj.name) == false)
        {
			differentObj = (GameObject)Instantiate(newObj, transform) as GameObject;
			differentObj.name = newObj.name;
			differentObj.GetComponent<AddCharacter>().DestroyWhenClick();
			stringChecker.Add(newObj.name);
		}
	}

	public void DePopulate(GameObject newObj)
	{
		Debug.Log("Remove " + newObj);
		stringChecker.Remove(newObj.name);
	}

	void Populate()
	{
		GameObject newObj;

		for (int i = 0; i < AllCharactersAndstring.Count; i++)
		{
			string[] words = AllCharactersAndstring[i].Split('-');
			if(words.Length > 1)
            {
				string ChineseSymbol = words[2];

				newObj = (GameObject)Instantiate(prefab, transform);
				newObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ChineseSymbol;
				newObj.name = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3] + "-" + words[4];
				newObj.transform.SetParent(this.transform);
			}
			
		}

	}
	public List<string> GetPrefabList()
    {
		return stringChecker;		
	}
}