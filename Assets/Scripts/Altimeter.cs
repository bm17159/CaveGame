using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Altimeter : MonoBehaviour
{
	[FormerlySerializedAs("Altitude")]
	[Header("Debug")]
	[SerializeField] private Transform Player;
	[SerializeField] private int Altitude;
	[SerializeField] private TextMeshPro TmeshPro;
	
	// find position between points of cave segments and + or - altitude
	
	
    // Start is called before the first frame update
    void Start()
    {
	    Debug.Log(TmeshPro.text);
    }

    // Update is called once per frame
    void Update()
    {
		// Set text to altitude
		Altitude = Mathf.RoundToInt(Player.position.y);
		TmeshPro.text = Altitude + "ft";
    }
}
