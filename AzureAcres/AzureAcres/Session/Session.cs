using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzureAcresData;
using Microsoft.Xna.Framework;

namespace AzureAcres
{
    public static class Session
    {
        private static int _selectedInventoryItemIndex = 0;

        public static int SelectedInventoryItemIndex
        {
            get { return Session._selectedInventoryItemIndex; }
            set { Session._selectedInventoryItemIndex = value; }
        }

        private static ICollectableItem _selectedInventoryItem;
        public static ICollectableItem SelectedInventoryItem
        {
            get {
                if (TopInventory == null) return null;
                return _selectedInventoryItem; 
            }
            set { _selectedInventoryItem = value; }
        }
        private static ICollectableItem[] _inventory;
        public static ICollectableItem[] Inventory
        {
            get { if (_inventory == null) _inventory = new ICollectableItem[16]; return _inventory; }
            set { _inventory = value; }
        }
        private static ICollectableItem[] _topInventory;
        public static ICollectableItem[] TopInventory
        {
            get { if (_topInventory == null)_topInventory = new ICollectableItem[5]; return _topInventory; }
            set { _topInventory = value; }
        }
        public static bool IsSelectedItemTool
        {
            get
            {
                if (SelectedInventoryItem == null) return false;
                return SelectedInventoryItem is Tool;
            }
        }
        public static bool IsSelectedItemCrop
        {
            get
            {
                if (SelectedInventoryItem == null) return false;
                return SelectedInventoryItem is Crop;
            }
        }
        public static void SelectNextInventoryItem()
        {
            if (TopInventory.Count() > 1)
            {
                _selectedInventoryItemIndex++;
                if (_selectedInventoryItemIndex >= TopInventory.Count()) _selectedInventoryItemIndex = 0;
                SelectedInventoryItem = TopInventory[_selectedInventoryItemIndex];
            }
        }
        public static void SelectPreviousInventoryItem()
        {
            if (TopInventory.Count() > 1)
            {
                _selectedInventoryItemIndex--;
                if (_selectedInventoryItemIndex < 0) _selectedInventoryItemIndex = TopInventory.Count() - 1;
                SelectedInventoryItem = TopInventory[_selectedInventoryItemIndex];
            }
        }
        public static void AddInventory(ICollectableItem item)
        {
            bool topInventoryFull = true;
            for (int i = 0; i < TopInventory.Length; ++i)
            {
                if (TopInventory[i] == null)
                {
                    TopInventory[i] = item;
                    topInventoryFull = false;
                    break;
                }
            }

            if(topInventoryFull)
            {
                for (int i = 0; i < Inventory.Length; ++i)
                {
                    if (Inventory[i] == null)
                    {
                        Inventory[i] = item;
                        break;
                    }
                }
            }
        }
        public static void RemoveInventory(ICollectableItem item)
        {
            for (int i = 0; i < TopInventory.Length; ++i)
                if (TopInventory[i] == item)
                    TopInventory[i] = null;
        }
        public static void SwapMainInventory(int obj1, int obj2)
        {
            ICollectableItem temp = Inventory[obj1];
            Inventory[obj1] = Inventory[obj2];
            Inventory[obj2] = temp;
        }
        public static void SwapTopInventory(int obj1, int obj2)
        {
            ICollectableItem temp = TopInventory[obj1];
            TopInventory[obj1] = TopInventory[obj2];
            TopInventory[obj2] = temp;
        }
        public static void SwapMainAndTopInventory(int mainIndex, int topIndex)
        {
            ICollectableItem temp = Inventory[mainIndex];
            Inventory[mainIndex] = TopInventory[topIndex];
            TopInventory[topIndex] = temp;
        }
        private static List<Character> _npcCharacters = new List<Character>();
        public static List<Character> NPCCharacters { get { return _npcCharacters; } set { _npcCharacters = value; } }
    }
}
