using System;
using System.Windows.Forms;
using System.IO;

public class CustomContextMenu : ContextMenu
{
    public CustomContextMenu()
    {
        MenuItem replaceModelItem = new MenuItem("Replace Model");
        replaceModelItem.Click += ReplaceModelItem_Click;
        this.MenuItems.Add(replaceModelItem);
    }

    private void ReplaceModelItem_Click(object sender, EventArgs e)
    {
        // Assuming you have a reference to the TreeView or the selected model
        // string selectedModel = ...;

        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Select TMD file to replace";
        openFileDialog.Filter = "TMD Files|*.tmd";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string selectedFilePath = openFileDialog.FileName;
            // Here you can call the logic to replace the model in the LBD file
            // For example:
            // LBDModelReplacer replacer = new LBDModelReplacer();
            // replacer.ReplaceModelInLBD(selectedModel, selectedFilePath);
        }
    }
}
