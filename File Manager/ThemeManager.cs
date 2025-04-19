using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager;

public class ThemeManager : ThemeColors
{
    private readonly ComboBox _comboBoxThemes;
    private readonly Control _rootControl;

    public ThemeManager(ComboBox comboBox, Control rootControl)
    {
        _comboBoxThemes = comboBox;
        _rootControl = rootControl;
        InitializeThemes();
    }
    private void InitializeThemes()
    {
        _comboBoxThemes.Items.AddRange(new object[] { "Light Theme", "Dark Theme" });
        _comboBoxThemes.SelectedIndexChanged += comboBoxThemes_SelectedIndexChanged;
        LoadThemes();
    }

    private void LoadThemes()
    {
        string savedTheme = Properties.Settings.Default.Theme;
        _comboBoxThemes.SelectedItem = string.IsNullOrEmpty(savedTheme)
            ? "Light Theme" : savedTheme;
    }

    private void comboBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
    {
        var theme = _comboBoxThemes.SelectedItem.ToString();
        ApplyTheme(theme);
        SaveTheme(theme);
    }

    private void ApplyTheme(string themeName)
    {
        var theme = GetThemeColors(themeName);
        ApplyToControls(_rootControl, theme);
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
}
