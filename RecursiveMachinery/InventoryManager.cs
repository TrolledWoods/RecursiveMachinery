using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveMachinery
{
    public class Inventory
    {
        int[] items; // First array is the scope, the second array is the item within that scope

        public Inventory()
        {
            items = new int[Resources.items.Count];
        }
        
    }

    public static class InventoryManager
    {

    }
}
