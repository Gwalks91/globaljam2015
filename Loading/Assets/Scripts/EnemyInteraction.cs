using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyInteraction : MonoBehaviour 
{
	private int mnumChoices;
	private List<string> mtexts;
	private Dictionary<string, int> mexpIds;
	private Dictionary<string, int> mtextIds;
	private Dictionary<string, int> mchoiceCost;
    private Dictionary<string, string> mchoiceImage;
    private Animator cameraAnimator;
	private int numIds;
	private string mexplanation;

    public AudioClip trainBGM;
    public AudioClip screamOnApprroach;
    public AudioClip trainLoop;
    public AudioClip lever;
    public AudioClip crash;

    private AudioSource BGM;
    private AudioSource trainSE;
    private AudioSource screenSE;

	// Use this for initialization
	void Start ()
    {
        cameraAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();

        AudioSource[] ass = GetComponents<AudioSource>();
        BGM = ass[0];
        BGM.clip = trainBGM;
        BGM.loop = true;
        BGM.playOnAwake = false;

        trainSE = ass[1];
        trainSE.clip = screamOnApprroach;
        trainSE.loop = false;
        trainSE.playOnAwake = false;

        screenSE = ass[2];
        screenSE.clip = trainLoop;
        screenSE.loop = false;
        screenSE.playOnAwake = false;
	}

	public void init()
	{
		mtexts = new List<string>();
		mexpIds = new Dictionary<string, int>();
		mtextIds = new Dictionary<string, int>();
		mchoiceCost = new Dictionary<string, int>();
		mchoiceImage = new Dictionary<string, string>();
		mexplanation = "";

        //GameObject.FindGameObjectWithTag("ExplanationPanel").GetComponent<Image>().enabled = false;
	}

	public void setNumChoice(int numChoices)
	{
		mnumChoices = numChoices;
	}

	public void setExplanation(string explanation, int id)
	{
		mexpIds.Add(explanation,id);
	}

	public void setnumIds(int ids)
	{
		numIds = ids;
	}

	public void addString(int id, string text, int value, string image)
	{
		mtexts.Add(text);
		mtextIds.Add(text, id);
		mchoiceCost.Add(text, value);
		mchoiceImage.Add(text, image);
	}

	// Update is called once per frame
	void OnTriggerEnter (Collider obj) 
	{
		if(obj.gameObject.tag == "Player")
		{
            System.Random r = new System.Random((int)System.DateTime.Now.Ticks);
            int chosenId = r.Next(0, numIds);
            //int chosenId = Random.Range(0, numIds-1);

			Debug.Log("Player hit the AI: " + numIds);
			int left = mnumChoices;
			PlayerMovement.Instance.SendMessage("togglePause");

	        GameObject[] gos = GameObject.FindGameObjectsWithTag("UserChoice");
			foreach(GameObject o in gos)
			{
	            Button b = o.GetComponent<Button>();
	            if (b != null)
	            {
	                if (left > 0)
	                {
	                    b.enabled = true;
	                    b.image.enabled = true;
	                    b.GetComponentInChildren<Text>().enabled = true;
						foreach(string str in mtextIds.Keys)
						{
							if(mtextIds[str] == chosenId)
							{
								b.GetComponentInChildren<Text>().text = str;
								Debug.Log(str);
								string text = str;
								b.onClick.AddListener(() => onButtonClick(text));
								mtextIds.Remove(str);
								left--;
								break;
							}
						}

		                cameraAnimator.SetBool("ZoomIn", true);
					}
	                else
	                {
	                    b.GetComponentInChildren<Text>().text = "";
	                }
	            }
			}
			foreach(string str in mexpIds.Keys)
			{
				Debug.Log (str);
				if(mexpIds[str] == chosenId)
				{
	                //GameObject.FindGameObjectWithTag("ExplanationPanel").GetComponent<Image>().enabled = true;
					Text t = GameObject.FindGameObjectWithTag("Explanation").GetComponent<Text>();
                    mexplanation = str;
					t.text = str;
					t.enabled = true;
				}
			}
			GameObject.Find("TextBackGround").GetComponent<Image>().enabled = true;
			//GameController.Instance.SendMessage("AddSanity");

            if (mexplanation.Contains("young girl")) // room 2 and we are checking 
            {
                BGM.Play();
                trainSE.PlayOneShot(screamOnApprroach);
                screenSE.PlayOneShot(trainLoop);
            }
		}
	}

	void onButtonClick(string buttonName)
	{
		Debug.Log("This is causing an error: " + buttonName);
		Debug.Log("This is causing an error: " + mchoiceCost[buttonName]);
		GameController.Instance.SendMessage("AddSanity", mchoiceCost[buttonName]);
		GameController.Instance.SendMessage("changePath", mchoiceImage[buttonName]);
		PlayerMovement.Instance.SendMessage("togglePause");
        //Button[] t = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
        GameObject[] gos = GameObject.FindGameObjectsWithTag("UserChoice");
		foreach(GameObject o in gos)
		{
            Button b = o.GetComponent<Button>();
            if (b != null)
            {
                b.image.enabled = false;
                b.enabled = false;
                b.onClick.RemoveAllListeners();
                b.GetComponentInChildren<Text>().enabled = false;

                cameraAnimator.SetBool("ZoomIn", false);
            }
        }

        if (mexplanation.Contains("young girl"))
        {
            BGM.Stop();
            trainSE.PlayOneShot(lever);
            screenSE.PlayOneShot(crash);
        }

	
        //GameObject.FindGameObjectWithTag("ExplanationPanel").GetComponent<Image>().enabled = false;
		Text t = GameObject.FindGameObjectWithTag("Explanation").GetComponent<Text>();
		t.text = "";
		t.enabled = false;
		GameObject.Find("TextBackGround").GetComponent<Image>().enabled = false;
		
	}
}
