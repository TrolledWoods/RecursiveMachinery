using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveMachinery
{
    public struct Inventory
    {
        Scope BaseScope;
        int[][] Items; // First array is the scope, the second array is the item within that scope
    }

    /// <summary>
    /// Use this for recipe management as a recipe only needs the current scope and all the parent scopes
    /// </summary>
    public struct ParentScopeInventory
    {
        Scope BaseScope;
        int[][] Items;
    }

    public static class InventoryManager
    {
        
    }
}
