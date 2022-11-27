using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace IOTools
{
    public class ConsoleTable
    {
        // Table line in format ┌──...──┐
        private string _startTableLine;

        // Table line in format ├──...──┤
        private string _midTableLine;

        // Table line in format └──...──┘
        private string _endTableLine;

        // Table line in format │ Name1 │ ... │ NameK │
        private string _columnsNamesLine;
        
        private readonly int _columnsCount;
        private readonly string[] _columnsNames;
        private int[] _columnsWidth;
        private List<string[]> _tableData;

        /// <summary>
        /// Constructor for table from columns names.
        /// </summary>
        /// <param name="columnsNames">Columns names.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ConsoleTable(params string[] columnsNames)
        {
            if (columnsNames == null)
            {
                throw new ArgumentNullException($"{nameof(columnsNames)} was not initialized");
            }
            if (columnsNames.Length == 0)
            {
                throw new ArgumentException("No columns names were provided");
            }
            _columnsCount = columnsNames.Length;
            _columnsNames = new string[_columnsCount];
            Array.Copy(columnsNames, _columnsNames, _columnsCount);
            _tableData = new List<string[]>(1);
            _columnsWidth = new int[_columnsCount];
            RecalculateTableSettings();
        }

        /// <summary>
        /// Method to add array of objects to the table.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="sep"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void AddRows(object[] rows, char sep = ' ')
        {
            foreach(object row in rows)
            {
                string[] cells = row.ToString().Split(sep);
                if (cells.Length != _columnsCount)
                {
                    throw new IndexOutOfRangeException();
                }
                _tableData.Add(cells);
            }
            RecalculateTableSettings();
        }

        /// <summary>
        /// Method to form table data in console 
        /// text table format with headers.
        /// </summary>
        /// <returns>String representaion of the table data.</returns>
        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder(_tableData.Count);

            // Form the header of the text table.
            strBuilder.AppendLine(_startTableLine);
            strBuilder.AppendLine(_columnsNamesLine);
            strBuilder.AppendLine(_midTableLine);
            if (_tableData.Count > 50)
            {// Add only 40 rows.
                foreach (string[] row in _tableData.Take(20))
                {// Add first 20 rows.
                    strBuilder.Append(BuildLine(row)); 
                }
                for (int j = 0; j != 3; ++j)
                {// Add "..." to each column to show skipped rows.
                    strBuilder.Append(BuildLine(".")); 
                }
                foreach (string[] row in _tableData.TakeLast(20))
                {// Add last 20 rows.
                    strBuilder.Append(BuildLine(row));
                }
            }
            else
            {// Add all rows.
                foreach (string[] row in _tableData)
                { 
                    strBuilder.Append(BuildLine(row)); 
                }                
            }
            strBuilder.AppendLine(_endTableLine);

            return strBuilder.ToString();
        }

        private void RecalculateTableSettings()
        {
            for (int i = 0; i < _columnsCount; ++i)
            {
                if (_columnsNames[i].Length > _columnsWidth[i])
                {
                    _columnsWidth[i] = _columnsNames[i].Length;
                }
            }
            for (int i = 0; i < _tableData.Count; ++i)
            {
                for (int j = 0; j < _columnsCount; ++j)
                {
                    if (_tableData[i][j].Length > _columnsWidth[j])
                    {
                        _columnsWidth[j] = _tableData[i][j].Length;
                    }
                }
            }
            _startTableLine = BuildStartLine();
            _midTableLine = BuildMidLine();
            _endTableLine = BuildEndLine();
            _columnsNamesLine = BuildColumnNamesLine();
        }

        /// <summary>
        /// Function to build first line of text
        /// table representation in the console.
        /// </summary>
        /// <returns>Table line in format ┌──...──┐</returns>
        private string BuildStartLine()
        {
            // Init StringBuilder with ┌ symbol.
            StringBuilder strBuilder = new StringBuilder("\u250C");

            for (int i = 0; i < _columnsWidth.Length - 1; ++i)
            {// Add ───...───┬.
                strBuilder.Append(new string('\u2500', _columnsWidth[i]));
                strBuilder.Append('\u252C');
            }

            // Add ───...───┐.
            strBuilder.Append(new string('\u2500', _columnsWidth[^1]));
            strBuilder.Append('\u2510');

            return strBuilder.ToString();
        }

        /// <summary>
        /// Function to build middle line of text
        /// table representation in the console
        /// needed for separating data.
        /// </summary>
        /// <returns>Table line in format ├──...──┤</returns>
        private string BuildMidLine()
        { 
            // Init StringBuilder with ├ symbol.
            StringBuilder strBuilder = new StringBuilder("\u251C");

            for (int i = 0; i < _columnsWidth.Length - 1; ++i)
            {// Add ───...───┼.
                strBuilder.Append(new string('\u2500', _columnsWidth[i]));
                strBuilder.Append('\u253C');
            }

            // Add ───...───┤.
            strBuilder.Append(new string('\u2500', _columnsWidth[^1]));
            strBuilder.Append('\u2524');

            return strBuilder.ToString();
        }

        /// <summary>
        /// Function to build end line of text
        /// table representation in the console.
        /// </summary>
        /// <returns>Table line in format └──...──┘</returns>
        private string BuildEndLine()
        {
            // Init StringBuilder with └ symbol.
            StringBuilder strBuilder = new StringBuilder("\u2514");
            for (int i = 0; i < _columnsWidth.Length - 1; ++i)
            {// Add ───...───┴.
                strBuilder.Append(new string('\u2500', _columnsWidth[i]));
                strBuilder.Append('\u2534');
            }
            // Add ───...───┘.
            strBuilder.Append(new string('\u2500', _columnsWidth[^1]));
            strBuilder.Append('\u2518');

            return strBuilder.ToString();
        }

        /// <summary>
        /// Function to build line with column names.
        /// </summary>
        /// <returns>Table line in format │ Name1 │ ... │ NameK │</returns>
        private string BuildColumnNamesLine()
        {
            // Init StringBuilder with │ symbol
            StringBuilder strBuilder = new StringBuilder("\u2502");
            for (int i = 0; i < _columnsCount; ++i)
            {// Add    NameK   │
                strBuilder.Append(_columnsNames[i].PadRight(_columnsWidth[i], ' ') + "\u2502");
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Function to build line in format │ str │ ... │ str │
        /// </summary>
        /// <param name="str">String to insert between │ separators</param>
        /// <returns>Line in format │ str │ ... │ str │</returns>
        private string BuildLine(string str)
        {
            // Init StringBuilder with │ symbol.
            StringBuilder strBuilder = new StringBuilder("\u2502");
            for (int i = 0; i < _columnsCount; ++i)
            {// Add    str   │
                strBuilder.Append(str.PadRight(_columnsWidth[i], ' ') + "\u2502");
            }
            // Add line terminator
            strBuilder.AppendLine();
            return strBuilder.ToString();
        }

        /// <summary>
        /// Function to build line in format │ elem1 │ ... │ elemK │
        /// </summary>
        /// <param name="row">Row of the table.</param>
        /// <returns>Line in format │ elem1 │ ... │ elemK │</returns>
        private string BuildLine(string[] row)
        {
            StringBuilder strBuilder = new StringBuilder("\u2502");
            for (int i = 0; i < _columnsCount; ++i)
            {
                strBuilder.Append(row[i].PadRight(_columnsWidth[i], ' ') + "\u2502");
            }
            strBuilder.AppendLine();
            return strBuilder.ToString();
        }
    }
}
