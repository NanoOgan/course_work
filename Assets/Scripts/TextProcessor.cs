using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class TextProcessor : MonoBehaviour {

    public Dictionary<string, string> knownCharacters;
    public string hasObject;
    public string neededObject;
    public string neededObjectData;
    public string request;
    public bool isBrought;


	// Use this for initialization
	void Start () {
        knownCharacters = new Dictionary<string, string>();
	}

    private string ProcessRequest(string request)
    {
        if (request.Contains("Where"))
        {
            var punctuation = request.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = request.Split().Select(x => x.Trim(punctuation)).ToArray();
            string requestedObject = words[3];
            if (knownCharacters.ContainsKey(requestedObject))
            {
                if (!isBrought)
                {
                    return "Bring me " + neededObject + " and I'll tell you , " + ((string.IsNullOrEmpty(neededObjectData))?string.Empty: neededObjectData + " has it");
                }
                else
                {
                    return knownCharacters[requestedObject] + " has It";
                }
            }
            else
            {
                return "I don't know";
            }
        }
        else if (request.Contains("Here is"))
        {
            isBrought = true;
            return "Thank you";
        }
        else if (request.Contains("Give me"))
        {
            var punctuation = request.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = request.Split().Select(x => x.Trim(punctuation)).ToArray();
            string requestedObject = words[3];
            if (isBrought)
            {
                if (hasObject.Equals(requestedObject))
                    return "Here it is";
                else
                    return "I Don't have it";
            }
            else
            {
                return "Bring me " + requestedObject + " and I'll give you";
            }
        }
        else
            return "You talk english? nah ...";
    }
}
