using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AzureAcresData
{
    public class ItemContainer : ContentObject
    {
        public delegate void OnItemSelectedHandler(int item);
        public event OnItemSelectedHandler OnItemSelected;
        public delegate void OnItemHoveredHandler(int item);
        public event OnItemHoveredHandler OnItemHovered;

        private bool _hasFocus = false;
        [ContentSerializerIgnore]
        public bool HasFocus
        {
            get { return _hasFocus; }
            set { _hasFocus = value; }
        }

        private int _contentsWidth;
        public int ContentsWidth
        {
            get { return _contentsWidth; }
            set { _contentsWidth = value; }
        }

        private int _contentsHeight;
        public int ContentsHeight
        {
            get { return _contentsHeight; }
            set { _contentsHeight = value; }
        }

        private Vector2 _dimensions;
        public Vector2 Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private string _backgroundTextureName;
        public string BackgroundTextureName
        {
            get { return _backgroundTextureName; }
            set { _backgroundTextureName = value; }
        }
        
        private Texture2D _backgroundTexture;
        [ContentSerializerIgnore]
        public Texture2D BackgroundTexture
        {
            get { return _backgroundTexture; }
            set { _backgroundTexture = value; }
        }

        private string _selectedTextureName;
        public string SelectedTextureName
        {
            get { return _selectedTextureName; }
            set { _selectedTextureName = value; }
        }

        private Texture2D _selectedTexture;
        [ContentSerializerIgnore]
        public Texture2D SelectedTexture
        {
            get { return _selectedTexture; }
            set { _selectedTexture = value; }
        }

        private ICollectableItem[] _items;
        [ContentSerializerIgnore]
        public ICollectableItem[] Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private int _hoveredIndex = -1;
        [ContentSerializerIgnore]
        public int HoveredIndex
        {
            get { return _hoveredIndex; }
            set { _hoveredIndex = value; if (OnItemHovered != null) OnItemHovered(value); }
        }

        private int _selectedIndex = -1;
        [ContentSerializerIgnore]
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; if (OnItemSelected != null) OnItemSelected(value); }
        }

        public void AddItem(ICollectableItem item)
        {
            for (int i = 0; i < Items.Length; ++i)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    break;
                }
            }
        }
        public void RemoveInventory(ICollectableItem item)
        {
            for (int i = 0; i < Items.Length; ++i)
                if (Items[i] == item)
                    Items[i] = null;
        }
        public static void MoveItem(ItemContainer from, ItemContainer to)
        {
            ICollectableItem temp = to.Items[to.SelectedIndex];
            to.Items[to.SelectedIndex] = from.Items[from.SelectedIndex];
            from.Items[from.SelectedIndex] = temp;
        }

        private bool _isVisible;
        [ContentSerializerIgnore]
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public ItemContainer() { }

        public class ItemContainerContentReader : ContentTypeReader<ItemContainer>
        {
            protected override ItemContainer Read(ContentReader input, ItemContainer existingInstance)
            {
                ItemContainer existing = existingInstance;
                if (existing == null)
                    existing = new ItemContainer();
                existing.Name = input.ReadString();
                existing.ContentsWidth = input.ReadInt32();
                existing.ContentsHeight = input.ReadInt32();
                existing.Dimensions = input.ReadVector2();
                existing.Position = input.ReadVector2();

                existing.BackgroundTextureName = input.ReadString();
                if (!String.IsNullOrEmpty(existing.BackgroundTextureName))
                    existing.BackgroundTexture = input.ContentManager.Load<Texture2D>(existing.BackgroundTextureName);
                
                existing.SelectedTextureName = input.ReadString();
                if (!String.IsNullOrEmpty(existing.SelectedTextureName))
                    existing.SelectedTexture = input.ContentManager.Load<Texture2D>(existing.SelectedTextureName);

                existing.Items = new ICollectableItem[existing.ContentsWidth * existing.ContentsHeight];
                return existing;
            }
        }
    }
}
