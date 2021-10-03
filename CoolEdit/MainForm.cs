using System;
using System.IO;
using System.Windows.Forms;

namespace CoolEdit
{
    public partial class MainForm : Form
    {
        public bool IsDirty { get; set; }
       
        public MainForm()
        {
            InitializeComponent();
            new SplashForm().ShowDialog();
        }

        #region Event code
        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void uppercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void lowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void insertDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertDate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void aboutCooleditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About();
        }
        #endregion

        #region Functionality   

        private void OpenFile()
        {
            if (IsDirty && MessageBox.Show("Do you want to open a new file without saving your current changes?",
                "You may lose unsaved changes", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) { return; }
                //opens the filedialog and returns how the user closed the dialog.
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                //If the user clicked OK
                try
                {
                    txtContent.Text = File.ReadAllText(filePath);
                    IsDirty = false;
                    saveFileDialog1.FileName = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening the file '{filePath}'. " +
                        $"Error was '{ex.Message}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFile()
        {
            //opens the filedialog and returns how the user closed the dialog.

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //If the user clicked 'ok'
                string filePath = saveFileDialog1.FileName;

                //if the file doesn't already exist
                //or it exists, but the user says 'OK' to overwrite it
                if (!File.Exists(filePath) || (File.Exists(filePath) &&
                    MessageBox.Show($"File '{filePath}' exists... overwrite?") == DialogResult.OK))
                {
                    try
                    {
                        File.WriteAllText(filePath, txtContent.Text);
                        IsDirty = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving to the file '{filePath}'. " +
                            $"Error was '{ex.Message}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CloseFile()
        {
            if (IsDirty && MessageBox.Show("Do you want to close the file without saving your changes?", "You may lose unsaved changes", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                txtContent.Text = "";
                IsDirty = false;
            }
        }
        private void Exit()
        {
            if (!IsDirty || (IsDirty && MessageBox.Show("Do you want to exit without saving changes?", "You may lose unsaved changes", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK))
            {
                Application.Exit();
            }
        }
        private void Cut()
        {
            string selectedText = txtContent.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                Clipboard.SetText(selectedText);
            }
            txtContent.SelectedText = "";

        }
        private void Copy()
        {
            string selectedText = txtContent.SelectedText;
            if (!string.IsNullOrEmpty(selectedText))
            {
                Clipboard.SetText(selectedText);
            }

        }
        private void Paste()
        {
            txtContent.SelectedText = Clipboard.GetText();
        }
        private void Uppercase()
        {
            txtContent.SelectedText = (txtContent.SelectedText + "").ToUpper();
        }
        private void Lowercase()
        {
            txtContent.SelectedText = (txtContent.SelectedText + "").ToLower();
        }
        private void InsertDate()
        {
            txtContent.SelectedText = DateTime.Now.ToShortDateString();
        }
        private void About()
        {
            new AboutForm().ShowDialog();
        }
        #endregion
    }
}