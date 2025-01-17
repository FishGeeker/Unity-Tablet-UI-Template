﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;	
using System.IO;
using UnityEngine.UI;

public class CallUserDataManager : MonoBehaviour
{
	public static CallUserDataManager ins;
	public ContactDatabase contactDB;
	public InputField nameInput, numberInput;

	void Awake()
	{
		ins = this;
	}

//	public void SaveContacts()
//	{
//		XmlSerializer ser = new XmlSerializer(typeof(ContactDatabase));
//		FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/callContact_data.xml", FileMode.Create);
//		ser.Serialize(stream, contactDB);
//		stream.Close();
//	}

	public void AddContacts()
	{
		string xmlPath = Application.dataPath + "/StreamingFiles/callContact_data.xml";

		if (File.Exists(Application.dataPath + "/StreamingFiles/callContact_data.xml"))
		{
			XmlDocument XDoc = new XmlDocument();
			XDoc.Load(xmlPath);
			XmlElement Node = (XmlElement)XDoc.GetElementsByTagName("list") [0];

			if (Node != null)
			{
				bool contactExist = false;
				foreach (XmlNode xNode in Node)
				{
					XmlNode nameParent = xNode.FirstChild;
					XmlNode numberParent = xNode.LastChild;
					if (nameParent.InnerText == nameInput.text && numberParent.InnerText == numberInput.text)
					{
						Debug.Log("User exists");
						contactExist = true;
					} 	
				}

				if (!contactExist)
				{
					XmlElement contactNode = XDoc.CreateElement("Contact");
					Node.AppendChild(contactNode);
					
					XmlElement nameNode = XDoc.CreateElement("name");
					XmlElement numberNode = XDoc.CreateElement("number");
					
					nameNode.InnerText = nameInput.text;
					numberNode.InnerText = numberInput.text;
					contactNode.AppendChild(nameNode);
					contactNode.AppendChild(numberNode);
				}
			}

			XDoc.Save(xmlPath);
		}
	}

	public void LoadContacts()
	{
		XmlSerializer ser = new XmlSerializer(typeof(ContactDatabase));
		FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/callContact_data.xml", FileMode.Open);
		contactDB = ser.Deserialize(stream) as ContactDatabase;

		stream.Close();
	}
}

[System.Serializable]
public class Contact
{
	public string name;
	public string number;
}

[System.Serializable]
public class ContactDatabase
{
	public List<Contact> list = new List<Contact>();
}