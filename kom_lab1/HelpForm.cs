using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kom_lab1
{
    public partial class Справка : Form
    {
        public Справка()
        {
            InitializeComponent();
            InitializeHelpContent();
        }

        private void InitializeHelpContent()
        {
            RichTextBox helpBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = SystemColors.Window
            };

            helpBox.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
            helpBox.AppendText("Программа предоставляет следующие функции:\n");

            helpBox.SelectionBullet = true;
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Создание нового файла.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Открытие существующего файла в формате .txt.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Сохранение текущего файла.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Сохранение файла под новым именем.\n");
            helpBox.SelectionBullet = false;

            helpBox.SelectionFont = new Font("Arial", 12, FontStyle.Regular);
            helpBox.AppendText("\nРабота с текстом:\n");
            helpBox.SelectionBullet = true;
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Вставить - текст из буфера обмена вставляется в окно редактирования.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Копировать - выделенный текст копируется в буфер обмена.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Вырезать - выделенный текст вырезается и копируется в буфер обмена.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Удалить - удаление выделенного текста.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Выделить все - выделяется весь текст из окна редактирования.\n");
            helpBox.SelectionFont = new Font("Arial", 11, FontStyle.Regular);
            helpBox.AppendText("Отменить/Повторить - отмена последнего/повтор отменненого действия.\n");
            helpBox.SelectionBullet = false;

            this.Controls.Add(helpBox);
        }

    }
}
