using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data.Linq;
using AzureAcresData;
using System.Reflection;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace AzureAcresMapEditor
{
    public class Map
    {
        private AzureAcresData.Map _contentMap;
        public AzureAcresData.Map ContentMap
        {
            get { return _contentMap; }
            set { _contentMap = value; }
        }
        private string _contentBasePath;
        public string ContentBasePath
        {
            get { return _contentBasePath; }
            set { _contentBasePath = value; }
        }
        public string TexturePath { get { return Path.Combine(ContentBasePath, ContentMap.TextureName + ".png"); } }
        private Image _texture;
        public Image Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Map() { }

        public XmlDocument SaveMap()
        {
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", ""));
            XmlElement xnaElement = document.CreateElement("XnaContent", "", new XmlAttribute[0]);
            XmlElement assetElement = document.CreateElement("Asset", "", new XmlAttribute[] { document.CreateAttribute2("Type", "AzureAcresData.Map") });
            xnaElement.AppendChild(assetElement);

            foreach (PropertyInfo prop in MapProperties)
            {
                if (prop == null) break;
                if (assetElement.GetElementsByTagName(prop.Name).Count != 0) continue;

                XmlElement node = document.CreateElement(prop.Name);
                if (prop.GetCustomAttributesData().Count() > 0 &&
                    prop.GetCustomAttributesData()[0].ToString() == "[Microsoft.Xna.Framework.Content.ContentSerializerIgnoreAttribute()]") continue;

                switch (prop.PropertyType.Name)
                {
                    case "String":
                        node.InnerText = (string)prop.GetValue(ContentMap, null);
                        break;
                    case "Int32[]":
                        int[] vals = (int[])prop.GetValue(ContentMap, null);
                        StringBuilder strVal = new StringBuilder();
                        foreach (int val in vals)
                            strVal.AppendFormat("{0}    ", val);
                        node.InnerText = strVal.ToString();
                        break;
                    case "List`1":
                        if (prop.Name == "Portals")
                            SavePortals(ref document, ref node);
                        break;
                }
                assetElement.AppendChild(node);
            }
            document.AppendChild(xnaElement);
            return document;
        }

        private PropertyInfo[] MapProperties
        {
            get
            {
                Assembly assembly = Assembly.Load("AzureAcresData");
                Type mapType = assembly.GetType("AzureAcresData.Map");
                PropertyInfo[] properties = new PropertyInfo[mapType.BaseType.GetProperties().Length + mapType.GetProperties().Length];
                Array.Copy(mapType.BaseType.GetProperties(), properties, mapType.BaseType.GetProperties().Length);
                Array.Copy(mapType.GetProperties(), 0, properties, mapType.BaseType.GetProperties().Length, mapType.GetProperties().Length);
                return properties;
            }
        }

        private PropertyInfo[] GetProperties(string assemblyName, string type)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            Type mapType = assembly.GetType(type);
            PropertyInfo[] properties = new PropertyInfo[mapType.BaseType.GetProperties().Length + mapType.GetProperties().Length];
            Array.Copy(mapType.GetProperties(), 0, properties, mapType.BaseType.GetProperties().Length, mapType.GetProperties().Length);
            return properties;
        }

        public void LoadMap(Stream mapFileStream)
        {
            ContentMap = new AzureAcresData.Map();
            ContentMap.Portals = new List<Portal>();
            StreamReader reader = new StreamReader(mapFileStream);
            XmlDocument document = new XmlDocument();
            document.LoadXml(reader.ReadToEnd());
            XmlNode node = document.GetElementsByTagName("Asset")[0];

            foreach (PropertyInfo prop in MapProperties)
            {
                if (prop == null) break;

                XmlNode childNode = FindChildNode(node, prop.Name);
                if (childNode != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            prop.SetValue(ContentMap, childNode.InnerText, null);
                            break;
                        case "Int32[]":
                            string[] vals = childNode.InnerText.Trim().Split().Where(p => !String.IsNullOrEmpty(p)).ToArray<string>();
                            int[] val = new int[vals.Length];
                            for (int i = 0; i < val.Length; ++i)
                                val[i] = Int32.Parse(vals[i]);
                            prop.SetValue(ContentMap, val, null);
                            break;
                        case "List`1":
                            if (prop.Name == "Portals")
                                LoadPortals(childNode.ChildNodes);
                            break;
                    }
                }
            }
        }

        private void SavePortals(ref XmlDocument doc, ref XmlElement node)
        {
            PropertyInfo[] properties = GetProperties("AzureAcresData", "AzureAcresData.Portal");
            foreach (Portal portal in ContentMap.Portals)
            {
                XmlElement itemElement = doc.CreateElement("Item");
                XmlElement el = doc.CreateElement("Name", portal.Name, new XmlAttribute[0]);
                itemElement.AppendChild(el);
                el = doc.CreateElement("ToMapName", portal.ToMapName, new XmlAttribute[0]);
                itemElement.AppendChild(el);
                el = doc.CreateElement("ToPortalName", portal.ToPortalName, new XmlAttribute[0]);
                itemElement.AppendChild(el);
                el = doc.CreateElement("Coordinates", portal.Coordinates.X + " " + portal.Coordinates.Y, new XmlAttribute[0]);
                itemElement.AppendChild(el);
                el = doc.CreateElement("Direction", portal.Direction.ToString(), new XmlAttribute[0]);
                itemElement.AppendChild(el);
                node.AppendChild(itemElement);
            }
        }

        private void LoadPortals(XmlNodeList items)
        {
            PropertyInfo[] properties = GetProperties("AzureAcresData", "AzureAcresData.Portal");
            foreach (XmlElement item in items)
            {
                Portal portal = new Portal();

                foreach (PropertyInfo prop in properties)
                {
                    XmlNodeList propElements = item.GetElementsByTagName(prop.Name);
                    if (propElements.Count == 0)
                        continue;
                    XmlNode propElement = propElements[0];
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            prop.SetValue(portal, propElement.InnerText, null);
                            break;
                        case "Int32[]":
                            string[] vals = propElement.InnerText.Trim().Split().Where(p => !String.IsNullOrEmpty(p)).ToArray<string>();
                            int[] val = new int[vals.Length];
                            for (int i = 0; i < val.Length; ++i)
                                val[i] = Int32.Parse(vals[i]);
                            prop.SetValue(portal, val, null);
                            break;
                        case "Point":
                            string[] strVal = propElement.InnerText.Trim().Split();
                            prop.SetValue(portal, new Microsoft.Xna.Framework.Point(Int32.Parse(strVal[0]), Int32.Parse(strVal[1])), null);
                            break;
                    }
                }
                ContentMap.Portals.Add(portal);
            }
        }

        public void LoadTexture(string filename)
        {
            _texture = Image.FromFile(filename);
        }

        private XmlNode FindChildNode(XmlNode node, string nodeName)
        {
            XmlNode childNode = null;
            foreach (XmlNode n in node.ChildNodes)
            {
                if (n.Name == nodeName)
                {
                    childNode = n;
                    break;
                }
            }
            return childNode;
        }
    }

    public static class Extensions
    {
        public static XmlElement CreateElement(this XmlDocument document, string name, string innerText, XmlAttribute[] attrs = null)
        {
            XmlElement el = document.CreateElement(name);
            el.InnerText = innerText;
            if (attrs != null)
                foreach (XmlAttribute a in attrs)
                    el.Attributes.Append(a);
            return el;
        }
        public static XmlAttribute CreateAttribute2(this XmlDocument document, string name, string value)
        {
            XmlAttribute attr = document.CreateAttribute(name);
            attr.Value = value;
            return attr;
        }
    }
}
