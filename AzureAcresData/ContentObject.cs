using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace AzureAcresData
{
    public abstract class ContentObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public ContentObject()
        {
            Name = GetHashCode().ToString();
        }
    }
}
