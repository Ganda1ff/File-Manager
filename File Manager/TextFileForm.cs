using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager;

public partial class TextFileForm : Form
{
    public string FilePath { get; set; }
    public TextFileForm(string filePath)
    {
        InitializeComponent();
        FilePath = filePath;
        LoadFile();
    }

    private void LoadFile()
    {
        if (File.Exists(FilePath))
        {
            textBoxFile.Text = File.ReadAllText(FilePath);
        }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        File.WriteAllText(FilePath, textBoxFile.Text);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void textBoxFile_TextChanged(object sender, EventArgs e)
    {
        labelCounter.Text = $"Symbols: {textBoxFile.Text.Length}";
    }
}
