using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace kom_lab1
{
    internal class Analyzer
    {
        private readonly Dictionary<string, int> KeyWords = new Dictionary<string, int>
        {
            { "final", 1 },
            { "String", 2 }
        };

        public DataTable DataTable { get; private set; } = new DataTable();
        public List<string> Errors { get; private set; } = new List<string>();

        public Analyzer()
        {
            InitializeTable();
        }

        private void InitializeTable()
        {
            DataTable.Columns.Add("Статус", typeof(int));
            DataTable.Columns.Add("Тип", typeof(string));
            DataTable.Columns.Add("Значение", typeof(string));
            DataTable.Columns.Add("Позиция", typeof(string));
        }

        public void Analyze(string input, RichTextBox output)
        {
            string text = input.Replace("\t", "").Replace("\n", "");
            int status = 0;
            int position = 0;

            while (position < text.Length)
            {
                switch (status)
                {
                    case 0:
                        if (char.IsLetter(text[position]))
                        {
                            status = 1;
                        }
                        else if (char.IsWhiteSpace(text[position]))
                        {
                            status = 2;
                        }
                        else if (text[position] == '=')
                        {
                            status = 3;
                        }
                        else if (text[position] == '"')
                        {
                            status = 4;
                        }
                        else if (text[position] == ';')
                        {
                            status = 5;
                        }
                        else
                        {
                            Errors.Add(text[position].ToString());
                            status = 6;
                        }
                        break;

                    case 1:
                        string word = "";
                        int beginIndex = position;
                        while (position < text.Length && (char.IsLetterOrDigit(text[position]) || text[position] == '_'))
                        {
                            word += text[position];
                            position++;
                        }
                        if (KeyWords.ContainsKey(word))
                        {
                            DataTable.Rows.Add(KeyWords[word], "ключевое слово", word, $"с {beginIndex + 1} по {position} символ");
                        }
                        else
                        {
                            DataTable.Rows.Add(3, "идентификатор", word, $"с {beginIndex + 1} по {position} символ");
                        }
                        status = 0;
                        break;

                    case 2:
                        DataTable.Rows.Add(4, "разделитель", "пробел", $"с {position + 1} по {position + 1} символ");
                        position++;
                        status = 0;
                        break;

                    case 3:
                        DataTable.Rows.Add(5, "оператор присваивания", "=", $"с {position + 1} по {position + 1} символ");
                        position++;
                        status = 0;
                        break;

                    case 4:
                        string str = "\"";
                        int strStart = position;
                        position++;
                        while (position < text.Length && text[position] != '"')
                        {
                            str += text[position];
                            position++;
                        }
                        if (position < text.Length && text[position] == '"')
                        {
                            str += '"';
                            position++;
                            DataTable.Rows.Add(6, "строка", str, $"с {strStart + 1} по {position} символ");
                        }
                        else
                        {
                            Errors.Add("Unterminated string literal");
                        }
                        status = 0;
                        break;

                    case 5:
                        DataTable.Rows.Add(7, "конец оператора", ";", $"с {position + 1} по {position + 1} символ");
                        position++;
                        status = 0;
                        break;

                    case 6:
                        DataTable.Rows.Add(8, "ERROR", text[position].ToString(), $"с {position + 1} по {position + 1} символ");
                        position++;
                        status = 0;
                        break;
                }
            }

            output.Clear();
            foreach (DataRow row in DataTable.Rows)
            {
                output.AppendText($"{row["Статус"]}\t{row["Тип"]}\t{row["Значение"]}\t{row["Позиция"]}\n");
            }

            foreach (var error in Errors)
            {
                output.AppendText($"ERROR: {error}\n");
            }
        }
    }
}
