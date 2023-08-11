using System;
using System.IO;
using System.Windows.Forms;
using libLSD.Formats;

public class LBDModelReplacer
{
    private LBD lbdFile;
    private TMD newTMDModel;

    public void ReplaceModelFromTMD(string lbdFilePath, string tmdFilePath, int modelIndex)
    {
        // Load the LBD file
        using var lbdStream = new FileStream(lbdFilePath, FileMode.Open);
        lbdFile = new LBD(lbdStream);

        // Load the TMD file
        using var tmdStream = new FileStream(tmdFilePath, FileMode.Open);
        newTMDModel = new TMD(tmdStream);

        // Check size constraints
        if (newTMDModel.Size > lbdFile.Models[modelIndex].Size)
        {
            MessageBox.Show("The new model is larger than the original. Replacement not allowed.");
            return;
        }

        // Replace the model at the specified index
        lbdFile.Models[modelIndex] = newTMDModel;

        // Save the modified LBD file
        using var saveStream = new FileStream(lbdFilePath, FileMode.Create);
        lbdFile.Write(saveStream);
    }
}
