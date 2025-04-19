using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager;

public partial class SearchForm : Form
{
    private RichTextBox _richTextBox;
    private bool _searchForward = true;
    public SearchForm(RichTextBox richTextBox)
    {
        InitializeComponent();
        this.TopMost = true;
        _richTextBox = richTextBox;
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        _searchForward = true;
        SearchText();
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
        _searchForward = false;
        SearchText();
    }

    private void SearchText()
    {
        if (string.IsNullOrEmpty(textBoxSearch.Text)) return;

        RichTextBoxFinds options = RichTextBoxFinds.None;
        if (checkBoxMatchCase.Checked) options |= RichTextBoxFinds.MatchCase;

        int startIndex;
        int endIndex;
        bool isWrapSearch = false; 

        if (_searchForward)
        {
            startIndex = _richTextBox.SelectionStart + _richTextBox.SelectionLength;
            endIndex = _richTextBox.TextLength;
        }
        else
        {
            options |= RichTextBoxFinds.Reverse;
            startIndex = _richTextBox.SelectionStart - 1;
            endIndex = 0;

            if (startIndex < 0)
            {
                startIndex = _richTextBox.TextLength - 1;
                isWrapSearch = true; 
            }
        }

        startIndex = Math.Clamp(startIndex, 0, _richTextBox.TextLength - 1);
        endIndex = Math.Clamp(endIndex, 0, _richTextBox.TextLength);

        int index = -1;
        try
        {
            index = _richTextBox.Find(
                textBoxSearch.Text,
                startIndex,
                endIndex,
                options
            );
        }
        catch (ArgumentException)
        {
            if (!_searchForward)
            {
                (startIndex, endIndex) = (endIndex, startIndex);
                index = _richTextBox.Find(textBoxSearch.Text, startIndex, endIndex, options);
            }
        }

        if (index == -1)
        {
            if (_searchForward)
            {
                MessageBox.Show("End of document reached.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                index = _richTextBox.Find(textBoxSearch.Text, 0, options); 
            }
            else
            {
                MessageBox.Show("Start of document reached.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                index = _richTextBox.Find(textBoxSearch.Text, _richTextBox.TextLength - 1, options | RichTextBoxFinds.Reverse); 
            }
        }

        if (index != -1)
        {
            _richTextBox.SelectionStart = index;
            _richTextBox.SelectionLength = textBoxSearch.Text.Length;
            _richTextBox.ScrollToCaret();
            _richTextBox.Focus();
        }
        else
        {
            MessageBox.Show("Text not found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    private void SearchForm_Load(object sender, EventArgs e)
    {
        this.ControlBox = false;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        this.Hide(); 
    }
}
