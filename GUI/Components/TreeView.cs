using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImGuiNET;

namespace LSDView.GUI.Components
{
    public class TreeView<T> : ImGuiComponent where T : TreeNode
    {
        public List<T> Nodes;
        public TreeNode Selected => _selected;
        private TreeNode _selected = null;

        public TreeView()
        {
            Nodes = new List<T>();
        }

        public void Deselect()
        {
            if (_selected == null) return;
            _selected.Flags &= ~ImGuiTreeNodeFlags.Selected;
            _selected.OnDeselect();
            _selected = null;
        }

        public void SetNode(T node)
        {
            Nodes.Clear();
            Nodes.Add(node);
            _selected = node;
            node.Flags |= ImGuiTreeNodeFlags.Selected;
        }

        protected override void renderSelf()
        {
            ImGui.BeginChild("tree");
            foreach (var node in Nodes)
            {
                node.OnSelectInChildren(Select);
                node.FlagsInChildren(ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.DefaultOpen | ImGuiTreeNodeFlags.OpenOnDoubleClick);
                node.Render();

                // Context menu for each node
                if (ImGui.BeginPopupContextItem())
                {
                    if (ImGui.MenuItem("Nahradit model"))
                    {
                        ReplaceModelMenuItem_Click();
                    }
                    ImGui.EndPopup();
                }
            }
            ImGui.EndChild();
        }

        private void Select(TreeNode node)
        {
            if (_selected != null)
            {
                _selected.Flags &= ~ImGuiTreeNodeFlags.Selected;
                _selected.OnDeselect();
            }
            _selected = node;
            node.Flags |= ImGuiTreeNodeFlags.Selected;
        }

        // Your existing method
        private void ReplaceModelMenuItem_Click()
        {
            string lbdFilePath = _selected.Tag.ToString(); // Zmìna z _selectedNode na _selected
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TMD files (*.tmd)|*.tmd";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string tmdFilePath = openFileDialog.FileName;
                LBDModelReplacer replacer = new LBDModelReplacer();
                replacer.ReplaceModelFromTMD(lbdFilePath, tmdFilePath, 0);
            }
        }




    }
}