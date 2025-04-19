using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Reflection.Metadata;

namespace File_Manager;

public partial class TextFileForm : Form
{

    public string FilePath { get; set; }
    private SearchForm _searchForm;
    public TextFileForm(string filePath)
    {
        InitializeComponent();
        FilePath = filePath;
        LabelFileName();
        LoadFile();

    }

    private void LoadFile()
    {
        if (File.Exists(FilePath))
        {
            string content = File.ReadAllText(FilePath, Encoding.UTF8);

            richTextBoxFile.TextChanged -= richTextBoxFile_TextChanged;

            richTextBoxFile.Text = content;

            richTextBoxFile.TextChanged += richTextBoxFile_TextChanged;

            UpdateSymbolCounter();

            richTextBoxFile.Refresh();
            Application.DoEvents();
        }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        File.WriteAllText(FilePath, richTextBoxFile.Text);
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void UpdateSymbolCounter()
    {
        labelCounter.Text = $"Symbols: {richTextBoxFile.Text.Length}";
    }
    private void richTextBoxFile_TextChanged(object sender, EventArgs e)
    {
        UpdateSymbolCounter();
    }
    private void LabelFileName()
    {
        labelFileName.Text = $"File: {Path.GetFileName(FilePath)}";
    }

    private void findToolStripMenuItem_Click(Object sender, EventArgs e)
    {
        if (_searchForm == null || _searchForm.IsDisposed)
        {
            _searchForm = new SearchForm(richTextBoxFile);
            _searchForm.FormClosed += (s, args) => { _searchForm = null; };
        }
        _searchForm.Show();
        _searchForm.BringToFront();
    }
}
