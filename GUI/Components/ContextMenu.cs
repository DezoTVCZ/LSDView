using System;
using System.Collections.Generic;
using ImGuiNET;
using libLSD.Formats; // Pøidáno pro práci s LBD a TMD

namespace LSDView.GUI.Components
{
    public class ContextMenu
    {
        public readonly Dictionary<string, Action> MenuItems;

        public ContextMenu(Dictionary<string, Action> menuItems) { MenuItems = menuItems; }

        public void Render()
        {
            if (ImGui.BeginPopupContextItem())
            {
                foreach (var item in MenuItems)
                {
                    if (ImGui.Selectable(item.Key)) item.Value();
                }

                // Pøidání položky pro nahrazení modelu
                if (ImGui.Selectable("Replace Model")) ReplaceModelMenuItem_Click();

                ImGui.EndPopup();
            }
        }

        public bool Equals(ContextMenu other) { return Equals(MenuItems, other.MenuItems); }

        public override bool Equals(object obj) { return obj is ContextMenu other && Equals(other); }

        public override int GetHashCode() { return (MenuItems != null ? MenuItems.GetHashCode() : 0); }

        // Metoda pro spuštìní funkce nahrazení modelu
        private void ReplaceModelMenuItem_Click()
        {
            // TODO: Zde bude kód pro nahrazení modelu.
            // Mùžete napøíklad vytvoøit instanci tøídy LBDModelReplacer a zavolat její metodu.
            // LBDModelReplacer replacer = new LBDModelReplacer();
            // replacer.ReplaceModel(...);
        }
    }
}
