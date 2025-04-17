using System.IO;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace File_Manager;
public partial class ManagerForm : Form
{
    public ManagerForm()
    {
        InitializeComponent();
        InitializeThemes();
        LoadDrivers();
        LoadThemes();
        buttonPrev.Enabled = false;
        buttonForward.Enabled = false;
    }

    private void LoadDrivers()
    {
        treeViewFolders.Nodes.Clear();
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                TreeNode node = new TreeNode($"{drive.Name} ({drive.VolumeLabel})");

                node.Tag = drive.Name;
                node.Nodes.Add("dummy");
                treeViewFolders.Nodes.Add(node);
            }
        }
    }

    private void ListViewFiles_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            ListViewItem item = listViewFiles.GetItemAt(e.X, e.Y);
            if (item != null)
            {

                item.Selected = true;
                contextMenuStrip.Show(listViewFiles, e.Location);
            }
        }
    }

    private void treeViewFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        e.Node.Nodes.Clear();
        string path = e.Node.Tag.ToString();

        try
        {
            foreach (string dir in Directory.GetDirectories(path))
            {
                TreeNode node = new TreeNode(Path.GetFileName(dir));
                node.Tag = dir;
                node.Nodes.Add("dummy");
                e.Node.Nodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }

    }

    private Stack<string> _prevHistory = new Stack<string>();
    private Stack<string> _forwardHistory = new Stack<string>();
    private void LoadDirectory(string path, bool isNavigation = false)
    {
        if (!isNavigation)
        {
            _prevHistory.Push(textBoxPath.Text);
            _forwardHistory.Clear();
        }

        

        try
        {
            listViewFiles.Items.Clear();
            textBoxPath.Text = path;

            foreach (string dir in Directory.GetDirectories(path))
            {
                ListViewItem file = new ListViewItem(Path.GetFileName(dir));
                file.SubItems.Add("Folder");
                file.SubItems.Add("");
                file.Tag = dir;
                listViewFiles.Items.Add(file);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo fi = new FileInfo(file);
                ListViewItem item = new ListViewItem(fi.Name);
                item.SubItems.Add("File");
                item.SubItems.Add($"{fi.Length / 1024} KB");
                item.Tag = file;
                listViewFiles.Items.Add(item);
            }

            buttonPrev.Enabled = _prevHistory.Count > 0;
            buttonForward.Enabled = _forwardHistory.Count > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void treeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
    {
        LoadDirectory(e.Node.Tag.ToString());
    }

    private void btnCreateFolder_Click(object sender, EventArgs e)
    {
        string folderName = Microsoft.VisualBasic.Interaction.InputBox("Enter folder name: ", "New folder");
        if (!string.IsNullOrEmpty(folderName))
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(textBoxPath.Text,
                    folderName));
                LoadDirectory(textBoxPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

    private void btnRename_Click(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count == 0)
        {
            MessageBox.Show("Choose folder!");
            return;
        }

        string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter new name: ", "Submit");

        if (string.IsNullOrEmpty(newName))
        {
            MessageBox.Show("Enter the name!");
            return;
        }

        string currenPath = textBoxPath.Text;
        string oldFullPath = listViewFiles.SelectedItems[0].Tag.ToString();
        string newFullPath = Path.Combine(currenPath, newName);
        try
        {
            if (File.Exists(newFullPath) || Directory.Exists(newFullPath))
            {
                MessageBox.Show("Name alreade in use");
                return;
            }

            if (Directory.Exists(oldFullPath))
            {
                Directory.Move(oldFullPath, newFullPath);
            }

            else if (File.Exists(oldFullPath))
            {
                File.Move(oldFullPath, newFullPath);
            }

            LoadDirectory(currenPath);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }



    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count > 0)
        {
            string path = listViewFiles.SelectedItems[0].Tag.ToString();
            try
            {
                if (Directory.Exists(path)) Directory.Delete(path, true);

                else File.Delete(path);
                LoadDirectory(textBoxPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count > 0)
        {
            string source = listViewFiles.SelectedItems[0].Tag.ToString();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string dest = Path.Combine(dialog.SelectedPath,
                        Path.GetFileName(source));

                    if (Directory.Exists(source)) Directory.Move(source, dest);
                    else File.Copy(source, dest);
                    LoadDirectory(textBoxPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
    private void listViewFiles_DoubleClick(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count == 0) return;


        string currentPath = textBoxPath.Text;
        string oldFullPath = listViewFiles.SelectedItems[0].Tag.ToString();
        string newFullPath = Path.Combine(currentPath, oldFullPath);

        if (Directory.Exists(oldFullPath))
        {
            
            LoadDirectory(oldFullPath);
            textBoxPath.Text = newFullPath;
            return;
            
        }

        else if (File.Exists(oldFullPath))
        {
            try
            {
                Process.Start(oldFullPath);
                textBoxPath.Text = newFullPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }

    private void btnProperties_Click(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count == 0)
        {
            MessageBox.Show("Choose the element!");
            return;
        }

        string path = listViewFiles.SelectedItems[0].Tag.ToString();
        ShowProperties(path);
    }

    private void ShowProperties(string path)
    {
        try
        {
            string properties = "";
            if (Directory.Exists(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                properties = $"Type: Folder\n" +
                    $"Name: {dirInfo.Name}\n" +
                    $"Path: {dirInfo.FullName}\n" +
                    $"Creation Time: {dirInfo.CreationTime}\n" +
                    $"Change Date: {dirInfo.LastWriteTime}\n";

            }
            else if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                properties = $"Type: File\n" +
                    $"Name: {fileInfo.Name}\n" +
                    $"Size: {fileInfo.Length} bytes\n" +
                    $"Path: {fileInfo.FullName}\n" +
                    $"Creation Time: {fileInfo.CreationTime}\n" +
                    $"Change Date: {fileInfo.LastWriteTime}\n";
            }
            else
            {
                MessageBox.Show("Element not found");
            }

            MessageBox.Show(properties, "Properties");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void textBoxPath_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            ProcessPathInput();
        }

    }

    private void ProcessPathInput()
    {

        string inputPath = textBoxPath.Text.Trim();

        if (string.IsNullOrEmpty(inputPath))
        {
            MessageBox.Show("Enter the path!");
            return;
        }



        try
        {
            string normalizedPath = Path.GetFullPath(inputPath);
            if (Directory.Exists(normalizedPath))
            {

                LoadDirectory(normalizedPath);
                treeViewFolders.SelectedNode = FindTreeNodeByPath(normalizedPath);
            }

            else if (File.Exists(normalizedPath))
            {
                Process.Start(normalizedPath);
            }
            else
            {
                MessageBox.Show("Path does not exist!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private TreeNode FindTreeNodeByPath(string path)
    {
        foreach (TreeNode node in treeViewFolders.Nodes)
        {
            if (node.Tag.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return node;
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Tag != null && childNode.Tag.ToString().Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    return childNode;
                }
            }
        }
        return null;
    }


    //Themes
    private void InitializeThemes()
    {
        comboBoxThemes.Items.AddRange(new object[] { "Light Theme", "Dark Theme" });
        comboBoxThemes.SelectedIndexChanged += comboBoxThemes_SelectedIndexChanged;
    }

    private void LoadThemes()
    {
        string savedTheme = Properties.Settings.Default.Theme;
        comboBoxThemes.SelectedItem = string.IsNullOrEmpty(savedTheme)
            ? "Light Theme" : savedTheme;
    }

    private void comboBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
    {
        var theme = comboBoxThemes.SelectedItem.ToString();
        ApplyTheme(theme);
        SaveTheme(theme);
    }

    private void ApplyTheme(string themeName)
    {
        var theme = GetThemeColors(themeName);
        ApplyToControls(this, theme);
    }

    private ThemeColors GetThemeColors(string themeName)
    {
        return themeName switch
        {
            "Dark Theme" => new ThemeColors
            {
                Background = Color.FromArgb(32, 32, 32),
                Foreground = Color.WhiteSmoke,
                ControlBackground = Color.FromArgb(50, 50, 50),
                Highlight = Color.FromArgb(64, 64, 64),
                Border = Color.FromArgb(0, 0, 0)
            },
            _ => new ThemeColors
            {
                Background = SystemColors.Control,
                Foreground = SystemColors.ControlText,
                ControlBackground = SystemColors.Window,
                Highlight = SystemColors.Highlight,
                Border = SystemColors.ActiveBorder
            }
        };
    }

    private void ApplyToControls(Control parent, ThemeColors theme)
    {
        foreach (Control control in parent.Controls)
        {
            ApplyToControls(control, theme);

            switch (control)
            {
                case ListView lv:
                    lv.BackColor = theme.ControlBackground;
                    lv.ForeColor = theme.Foreground;
                    break;
                case TreeView tv:
                    tv.BackColor = theme.ControlBackground;
                    tv.ForeColor = theme.Foreground;
                    break;
                case ContextMenuStrip cms:
                    cms.BackColor = theme.ControlBackground;
                    cms.ForeColor = theme.Foreground;
                    break;
                case TextBox txt:
                    txt.BackColor = theme.ControlBackground;
                    txt.ForeColor = theme.Foreground;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    break;
            }
        }
        parent.BackColor = theme.Background;
        parent.ForeColor = theme.Foreground;
    }

    private void SaveTheme(string themeName)
    {
        Properties.Settings.Default.Theme = themeName;
        Properties.Settings.Default.Save();
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
        if (_prevHistory.Count > 0) 
        {
            string previousPath = _prevHistory.Pop();
            _forwardHistory.Push(textBoxPath.Text);
            LoadDirectory(previousPath, true);
        }

    }

    private void btnForward_Click(object sender, EventArgs e)
    {
        if (_forwardHistory.Count > 0)
        {
            string nextPath = _forwardHistory.Pop();
            _prevHistory.Push(textBoxPath.Text);
            LoadDirectory(nextPath, true);
        }
    }
}

