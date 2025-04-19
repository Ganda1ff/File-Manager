using System.IO;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace File_Manager;
public partial class ManagerForm : Form
{
    private readonly ThemeManager _themeManager;
    public ManagerForm()
    {
        InitializeComponent();
        LoadDrivers();
        _themeManager = new ThemeManager(comboBoxThemes, this);
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
        string folderName = Microsoft.VisualBasic.Interaction.InputBox("Enter folder name: ", "New folder", "New folder");
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
            MessageBox.Show("Choose folder or file!");
            return;
        }

        string oldFullPath = listViewFiles.SelectedItems[0].Tag.ToString();
        string currentExtension = Path.GetExtension(oldFullPath);
        string newName = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter new name: ",
            "Submit",
            Path.GetFileNameWithoutExtension(oldFullPath)
            );

        if (string.IsNullOrEmpty(newName))
        {
            MessageBox.Show("Enter the name!");
            return;
        }

        try
        {
            bool isDirectory = Directory.Exists(oldFullPath);
            string parentDir = Path.GetDirectoryName(oldFullPath);
            string newFileName;

            if (isDirectory) {
                newFileName = newName;
            
            }
            else
            {
                newFileName = Path.HasExtension(newName)
                    ? newName
                    : $"{newName}{currentExtension}";
            }

            string newFullPath = Path.Combine(parentDir, newFileName);

            if (File.Exists(newFullPath) || Directory.Exists(newFullPath))
            {
                MessageBox.Show("Name alreade in use");
                return;
            }

            if (isDirectory)
            {
                Directory.Move(oldFullPath, newFullPath);
            }

            else
            {
                
                File.Move(oldFullPath, newFullPath);
            }

            LoadDirectory(parentDir);
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
                if (Directory.Exists(path))
                {
                    if (MessageBox.Show("Delete folder?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    else
                    {
                        Directory.Delete(path, true);
                    }
                }

                else {
                    if (MessageBox.Show("Delete file?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        File.Delete(path);
                    }
                }
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

    private void btnCreateTextFile_Click(object sender, EventArgs e)
    {
        string currentPath = textBoxPath.Text;

        string fileName = Microsoft.VisualBasic.Interaction.InputBox(
            "Enter file name:",
            "New text file",
            "New file.txt"
            );

        if (string.IsNullOrEmpty(currentPath)) {
            MessageBox.Show("Enter the name!");
            return;
        }
        if (!Path.HasExtension(fileName))
        {
            fileName += ".txt";
        }
        string fullPath = Path.Combine(currentPath, fileName);

        try
        {
            if (File.Exists(fullPath))
            {
                MessageBox.Show("Name already used");
                return;
            }
            File.Create(fullPath).Close();
            LoadDirectory(currentPath);
            OpenTextEditor(fullPath);
        }
        catch (Exception ex) {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void OpenTextEditor(string filePath)
    {
        TextFileForm editor = new TextFileForm(filePath);
        editor.ShowDialog();
    }
        private void listViewFiles_DoubleClick(object sender, EventArgs e)
    {
        if (listViewFiles.SelectedItems.Count == 0) return;


        string selectedPath = listViewFiles.SelectedItems[0].Tag.ToString();
        string ext = Path.GetExtension(selectedPath).ToLower();

        if (Directory.Exists(selectedPath))
        {
            
            LoadDirectory(selectedPath);
            textBoxPath.Text = selectedPath;
            return;
            
        }

        else if (File.Exists(selectedPath))
        {
            try
            {
                switch (ext)
                {
                    case ".txt":
                        OpenTextEditor(selectedPath);
                        break;
                    //case ".jpg":
                    //case ".jpeg":
                    //case ".png":
                    //    new ImagesViewForm(oldFullPath).Show();
                    //    break;
                    default:
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = selectedPath,
                            UseShellExecute = true

                        });
                        break;

                }
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

