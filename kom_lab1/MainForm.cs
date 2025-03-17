using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace kom_lab1
{
    public partial class Compiler : Form
    {
        private string currentFilePath = string.Empty;
        private bool isTextChanged = false;
        private Stack<string> undoStack = new Stack<string>();
        private Stack<string> redoStack = new Stack<string>();
        private bool isUndoRedoOperation = false;
        private Analyzer analyzer = new Analyzer();

        public Compiler()
        {
            InitializeComponent();
            OutputRichTextBox.ReadOnly = true;
            this.FormClosing += Compiler_FormClosing;
        }


        private void MarkTextChanged()
        {
            isTextChanged = true;
        }



        private void CheckAndSave()
        {
            if (isTextChanged)
            {
                var result = MessageBox.Show("Сохранить изменения в файле?", "Сохранение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    сохранитьToolStripMenuItem_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    throw new OperationCanceledException();
                }
            }
        }
        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunAnalyzer();
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAndSave();
            InputRichTextBox.Clear();
            currentFilePath = string.Empty;
            isTextChanged = false;
            undoStack.Clear();
            redoStack.Clear();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAndSave();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;
                    InputRichTextBox.Text = File.ReadAllText(currentFilePath);
                    isTextChanged = false;
                    undoStack.Clear();
                    redoStack.Clear();
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFilePath, InputRichTextBox.Text);
                isTextChanged = false;
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    File.WriteAllText(currentFilePath, InputRichTextBox.Text);
                    isTextChanged = false;
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CheckAndSave();
                Application.Exit();
            }
            catch (OperationCanceledException)
            {
                // Do nothing
            }
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 0)
            {
                isUndoRedoOperation = true;
                redoStack.Push(InputRichTextBox.Text);
                InputRichTextBox.Text = undoStack.Pop();
                isUndoRedoOperation = false;
            }
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                isUndoRedoOperation = true;
                undoStack.Push(InputRichTextBox.Text);
                InputRichTextBox.Text = redoStack.Pop();
                isUndoRedoOperation = false;
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputRichTextBox.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputRichTextBox.Copy();
        }


        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputRichTextBox.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(InputRichTextBox.SelectedText))
            {
                InputRichTextBox.SelectedText = string.Empty;
                isTextChanged = true;
            }
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputRichTextBox.SelectAll();
        }


        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void грамматикаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void классификацияГрамматикиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void диToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void тестовыйПримерToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void списокЛитературыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Справка helpForm = new Справка();
            helpForm.ShowDialog();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.ShowDialog();
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            создатьToolStripMenuItem_Click(sender, e);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem_Click(sender, e);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            отменитьToolStripMenuItem_Click(sender, e);
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            повторитьToolStripMenuItem_Click(sender, e);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            копироватьToolStripMenuItem_Click(sender, e);
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            вырезатьToolStripMenuItem_Click(sender, e);
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            вставитьToolStripMenuItem_Click(sender, e);
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            RunAnalyzer();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            вызовСправкиToolStripMenuItem_Click(sender, e);
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {
            оПрограммеToolStripMenuItem_Click(sender, e);
        }

        private void Compiler_Load(object sender, EventArgs e)
        {

        }

        private void InputRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isUndoRedoOperation)
            {
                undoStack.Push(InputRichTextBox.Text);
                redoStack.Clear();
            }
            MarkTextChanged();
        }

        private void RunAnalyzer()
        {
            string inputText = InputRichTextBox.Text;
            if (!string.IsNullOrWhiteSpace(inputText))
            {
                analyzer.Analyze(inputText, OutputRichTextBox);
            }
            else
            {
                MessageBox.Show("Введите текст для анализа.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void OutputRichTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void Compiler_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CheckAndSave();
            }
            catch (OperationCanceledException)
            {
                e.Cancel = true;
            }
        }
    }
}
