using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Niantic.Lightship.AR.ObjectDetection;
using Niantic.Lightship.AR.Subsystems.ObjectDetection;
using Niantic.Lightship.AR.XRSubsystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDetectionOutputManager : MonoBehaviour
{
    [SerializeField]
    private ARObjectDetectionManager _objectDetectionManager;

    [SerializeField]
    public TMP_Text _objectsDetectedText;

    //[SerializeField]
    //public GameObject _objectsDetectedBox;

    public string curr_fruit = "";

    private void Start()
    {
        _objectDetectionManager.enabled = true;
        _objectDetectionManager.MetadataInitialized += OnMetadataInitialized;
    }

    private void OnMetadataInitialized(ARObjectDetectionModelEventArgs args)
    {
        _objectDetectionManager.ObjectDetectionsUpdated += ObjectDetectionsUpdated;
    }

    private void ObjectDetectionsUpdated(ARObjectDetectionsUpdatedEventArgs args)
    {
        //Initialize our output string
        string resultString = curr_fruit;
        var result = args.Results;


        if (result == null)
        {
            Debug.Log("No results found.");
            return;
        }

        //Reset our results string
        resultString = curr_fruit;
        bool flag = true;

        //Iterate through our results
        for (int i = 0; i < result.Count; i++)
        {
            var detection = result[i];
            var categorizations = detection.GetConfidentCategorizations();
            if (categorizations.Count <= 0)
            {
                break;
            }

            //Sort our categorizations by highest confidence
            categorizations.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));

            var food = categorizations[0].CategoryName;
            
            if (flag)
            {
                flag = false;
                if (food == "berry")
                {
                    resultString += $"Fresa detectada\n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "32 calorias \n";
                    resultString += "7g de carbohidrato \n";
                    resultString += "0.67g de proteinas \n";
                    resultString += "0.30g de grasa \n";
                    resultString += "4.89g de azucares \n";
                    curr_fruit = resultString;
                }
                else if (food == "apple")
                {
                    resultString += $"Manzana detectada\n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "52 calorias \n";
                    resultString += "13g de carbohidrato \n";
                    resultString += "0.26g de proteinas \n";
                    resultString += "0.17g de grasa \n";
                    resultString += "10.39g de azucares \n";
                    curr_fruit = resultString;

                }
                else if (food == "banana")
                {
                    resultString = "";
                    resultString += $"Banana detectada\n";
                    resultString += "Por cada 100 gramos:\n";
                    resultString += "122 calorias \n";
                    resultString += "31.89g de carbohidrato \n";
                    resultString += "1.30g de proteinas \n";
                    resultString += "0.37g de grasa \n";
                    resultString += "15g de azucares \n";
                    curr_fruit = resultString;

                }
                else
                {
                    flag = true;
                }
            }
  
        }

        _objectsDetectedText.text = resultString;

    }

    public void clearText()
    {
        _objectsDetectedText.text = "";
        curr_fruit = "";
    }

    private void OnDestroy()
    {
        _objectDetectionManager.MetadataInitialized -= OnMetadataInitialized;
        _objectDetectionManager.ObjectDetectionsUpdated -= ObjectDetectionsUpdated;
    }
}
