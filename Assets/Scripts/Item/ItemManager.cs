using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Item
{
    public class ItemManager: MonoBehaviour
    {
        public BaseItem testItem;
        private readonly List<BaseItem> _items = new List<BaseItem>();

        // Called from BaseItem.Init();
        public void AddItem(BaseItem item)
        {
            // if already has ALL Item then change item to fruit
            if (_items.Contains(item))
            {
                Debug.Log($"Player has {item.ItemName}, change to defaultItem ");
                return;
            }
            _items.Add(item);
        }

        public void RemoveItem(BaseItem item)
        {
            if (!_items.Contains(item))
            {
                Debug.LogError($"ItemManager: {item.name} does not exist");
                return;
            }
            
            item.Disable();
            _items.Remove(item);
        }

        public IReadOnlyList<BaseItem> GetItems()
            => _items;
        
        public IReadOnlyList<BaseItem> GetItems(ItemType itemType)
            => _items.Where(e => e.ItemType == itemType).ToList();
    }
}
