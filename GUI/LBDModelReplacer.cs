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
        int lbdSize;
        int tmdSize;

        // Load the LBD file
        using (FileStream lbdFileStream = new FileStream(lbdFilePath, FileMode.Open))
        using (BinaryReader lbdReader = new BinaryReader(lbdFileStream))
        {
            lbdFile = new LBD(lbdReader);

            // Read LBD size from bytes 0A-0D
            lbdFileStream.Seek(0x0A, SeekOrigin.Begin);
            lbdSize = lbdReader.ReadInt32();
        }

        // Load the TMD file
        using (FileStream tmdFileStream = new FileStream(tmdFilePath, FileMode.Open))
        using (BinaryReader tmdReader = new BinaryReader(tmdFileStream))
        {
            newTMDModel = new TMD(tmdReader);

            // Read TMD size from its header (based on the TMD format specification)
            tmdFileStream.Seek(0x04, SeekOrigin.Begin);
            tmdSize = tmdReader.ReadInt32();
        }

        // Check size constraints
        if (tmdSize > lbdSize)
        {
            MessageBox.Show("The new model is larger than the original. Replacement not allowed.");
            return;
        }

        // Replace the model at the specified index
        // NOTE: You'll need to implement a way to replace the model in the LBD file.
        // The following is just a placeholder and will not compile.
        // lbdFile.Models[modelIndex] = newTMDModel;

        // Save the modified LBD file
        using (FileStream saveFileStream = new FileStream(lbdFilePath, FileMode.Create))
        using (BinaryWriter saveWriter = new BinaryWriter(saveFileStream))
        {
            lbdFile.Write(saveWriter);
        }
    }
}
