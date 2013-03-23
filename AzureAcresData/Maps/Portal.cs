using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace AzureAcresData
{
    public class Portal
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _toMapName;
        public string ToMapName
        {
            get { return _toMapName; }
            set { _toMapName = value; }
        }
        private string _toPortalName;
        public string ToPortalName
        {
            get { return _toPortalName; }
            set { _toPortalName = value; }
        }
        private Point _coordinates;
        public Point Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
        private string _direction;
        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private bool _isActive;
        [ContentSerializerIgnore]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public Portal()
        { }
        public class PortalReader : ContentTypeReader<Portal>
        {
            protected override Portal Read(ContentReader input, Portal existingInstance)
            {
                Portal existing = existingInstance;
                if (existing == null)
                    existing = new Portal();
                existing.Name = input.ReadString();
                existing.ToMapName = input.ReadString();
                existing.ToPortalName = input.ReadString();
                existing.Coordinates = input.ReadObject<Point>();
                existing.Direction = input.ReadString();
                return existing;
            }
        }
    }
}
