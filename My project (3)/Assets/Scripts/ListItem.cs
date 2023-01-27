/* using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ListItem
{
    public string houseName;
    public int id;
    public string clientName;

    public void SetupItem(int i, string hn, string cn)
    {
        id = i;
        houseName = hn;
        clientName = cn;
    }

    public void Print()
    {
        Debug.Log(id);
        Debug.Log(houseName);
        Debug.Log(clientName);
    }
}
public class ArchitectsList
{
    ListItem[] houseList;

    public void ManageList(string response)
    {
        char[] charsToTrim = { '[', ']' };
        response = response.Trim(charsToTrim);
        int listCount = 0;
        for (int i = 0; i < response.Length; i++)
        {
            if (response[i] == '}')
            {
                listCount++;
            }
        }

        houseList = new ListItem[listCount];

        string[] items = response.Split(',');
        for (int i = 0; i < listCount; i++)
        {
            AddItem(items[i], i);
        }
    }

    void AddItem(string str, int index)
    {
        int offset = 0;
        // get id
        string id = retrieveValue(str, 2 + offset);
        offset += id.Length;
        string name = retrieveValue(str, 16 + offset);
        offset += name.Length;
        string client = retrieveValue(str, 29 + offset);
        offset += client.Length;

        // add listitem
        houseList[index].SetupItem((int)id, name, client);
    }

    string retrieveValue(string str, int index)
    {
        string value = "";
        while (isAlphaNumeric((string)str[index]))
        {
            value += str[index];
            index++;
        }
        return value;
    }

    bool isAlphaNumeric(string str)
    {
        Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
        return rg.IsMatch(str);
    }

    public void PrintAll()
    {
        foreach (ListItem l in houseList)
        {
            l.Print();
        }
    }
}
/*
public class ListResponseReader {
    public void ReadList(string response) {
        string recentCharacters = "";
        int dataType = 0; //1 = id, 2 = name, 3 = client
        for(int i  = 0; i < response.Length; i++)
        {
            if (response[i] == "}") {
                // add object to list
            }
            // store recent alpha characters in string
            if (Regex.IsMatch(response[i], "[a-z]", RegexOptions.IgnoreCase))
            {
                recentCharacters += response[i];
            }
            else {
                recentCharacters = "";
            }
            // check if we have reached a datatype
            if (recentCharacters == "ïd")
            {
                dataType = 1;
            }
            else if (recentCharacters == "name")
            {
                dataType = 2;
            }
            else if (recentCharacters == "client") {
                dataType = 3;
            }
            switch (dataType)
            {
                case default:
                    break;
                case 1:     // id
                    
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }

        }
    }

}
*/
