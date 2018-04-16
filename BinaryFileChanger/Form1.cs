using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.Media;
using ZXing;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private byte[] buffer = new byte[128 * 1024];
        private int bytesread = 0;
        string path;
        string pathOfExcel;
        int lastRow = 0;

        private string[] prevAdrType = new string[10];
        private string[] prevDataType = new string[10];


        private object[] adressTypes = new object[] { "HEX", "DEC" };
        private object[] dataTypes = new object[] { "ASCII", "HEX", "DEC" };

        Button[] delButt = new Button[10];
        TextBox[] addressText = new TextBox[10];
        ComboBox[] addressType = new ComboBox[10];
        TextBox[] lenText = new TextBox[10];
        TextBox[] dataText = new TextBox[10];
        ComboBox[] dataType = new ComboBox[10];


        public Form1()
        {
            InitializeComponent();

            path = Environment.CurrentDirectory;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new_delBut(0);
            new_addrText(0);
            new_addrType(0);
            new_lenText(0);
            new_dataText(0);
            new_dataType(0);

            Panel1.Controls.Add(delButt[0], 0, 0);
            Panel1.Controls.Add(addressText[0], 1, 0);
            Panel1.Controls.Add(addressType[0], 2, 0);
            Panel1.Controls.Add(lenText[0], 3, 0);
            Panel1.Controls.Add(dataText[0], 4, 0);
            Panel1.Controls.Add(dataType[0], 5, 0);
        }

        // new elements creation functions

        private void new_delBut(int row)
        {
            delButt[row] = new Button();

            delButt[row].Name = "delbut" + row.ToString();
            delButt[row].Text = "-";
            delButt[row].Dock = DockStyle.Top;
            delButt[row].Click += new EventHandler(delButton_Click);
        }

        private void new_addrText(int row)
        {
            addressText[row] = new TextBox();

            addressText[row].Name = "addressText" + row.ToString();
            addressText[row].Dock = DockStyle.Top;
            addressText[row].TextAlign = HorizontalAlignment.Right;
            addressText[row].TextChanged += new EventHandler(addressTxtBox_TextChanged);
        }

        private void new_addrType(int row)
        {
            addressType[row] = new ComboBox();
            prevAdrType[row] = "HEX";

            addressType[row].Name = "addressType" + row.ToString();
            addressType[row].Items.AddRange(adressTypes);
            addressType[row].SelectedIndex = 0;
            addressType[row].DropDownStyle = ComboBoxStyle.DropDownList;
            addressType[row].Dock = DockStyle.Top;
            addressType[row].SelectedIndexChanged += new EventHandler(addressComboBox_SelectedIndexChanged);
        }

        private void new_lenText(int row)
        {
            lenText[row] = new TextBox();

            lenText[row].Name = "lenText" + row.ToString();
            lenText[row].Text = "0";
            lenText[row].Dock = DockStyle.Top;
            lenText[row].TextAlign = HorizontalAlignment.Right;
            lenText[row].TextChanged += new EventHandler(lenTxtBox_TextChanged);
        }

        private void new_dataText(int row)
        {
            dataText[row] = new TextBox();

            dataText[row].Name = "dataText" + row.ToString();
            dataText[row].Dock = DockStyle.Top;
            dataText[row].TextAlign = HorizontalAlignment.Right;
            dataText[row].TextChanged += new EventHandler(dataTxtBox_TextChanged);
        }

        private void new_dataType(int row)
        {
            dataType[row] = new ComboBox();
            prevAdrType[row] = "ASCII";

            dataType[row].Name = "dataType" + row.ToString();
            dataType[row].Items.AddRange(dataTypes);
            dataType[row].SelectedIndex = 0;
            dataType[row].DropDownStyle = ComboBoxStyle.DropDownList;
            dataType[row].Dock = DockStyle.Top;
            dataType[row].SelectedIndexChanged += new EventHandler(dataComboBox_SelectedIndexChanged);


        }

        // Event Handler functions

        private void browserButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Stream myStream = null;


            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Binary files (*.bin)|*.bin";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    if ((myStream = File.Open(browserText.Text = openFileDialog1.FileName, FileMode.Open, FileAccess.Read)) != null)
                    {
                        using (myStream)
                        {
                            bytesread = myStream.Read(buffer, 0, buffer.Length);
                        }
                    }

                    myStream.Close();

                    if (buffer.Count() > 0)
                        browserText.Text = openFileDialog1.FileName;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void browserText_TextChanged(object sender, EventArgs e)
        {
            String Filename = browserText.Text;


            if (File.Exists(Filename))
            {
                if (Path.GetExtension(Filename) == ".bin")
                {
                    SaveButton.Enabled = true;
                }
                else
                {
                    SaveButton.Enabled = false;
                }
            }
            else
            {
                SaveButton.Enabled = false;
            }
        }

        private void addressTxtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;
            String name = text.Name;

            if (text == null)
                return;

            int lineNum = GetRow(text.Name);
            int cursorIndex = text.SelectionStart;
            int textLong = text.Text.Count();

            if (name.Contains("addressText") & text.Text != "")
            {
                try
                {
                    switch (addressType[lineNum].SelectedItem)
                    {
                        case "HEX":
                            text.Text = Regex.Replace(text.Text, @"[^A-Fa-f0-9]", new MatchEvaluator(WrongValue));
                            uint.Parse(text.Text, System.Globalization.NumberStyles.HexNumber);
                            break;

                        case "DEC":
                            text.Text = Regex.Replace(text.Text, @"[^\d]", new MatchEvaluator(WrongValue));
                            uint.Parse(text.Text);
                            break;
                    }
                    SaveButton.Enabled = true;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Túl nagy értéket adtál meg a " + (lineNum + 1) + ". sorban a címnél");
                    SaveButton.Enabled = false;
                }
            }

            text.SelectionLength = 0;

            int nextCursor = cursorIndex - (textLong - text.Text.Count());

            if (nextCursor < 0)
                text.SelectionStart = 0;
            else
                text.SelectionStart = nextCursor;

        }

        private void addressComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (combo == null)
                return;

            int lineNum = GetRow(combo.Name);

            if (combo.Name.Contains("addressType") & addressText[lineNum].Text != "")
            {
                try
                {
                    if ("HEX" == combo.SelectedItem)
                    {
                        if (prevAdrType[lineNum] != "HEX")
                        {
                            addressText[lineNum].Text = dec2Hex(addressText[lineNum].Text);
                        }
                    }
                    else
                    {
                        if ("DEC" == combo.SelectedItem)
                        {
                            if (prevAdrType[lineNum] != "DEC")
                            {
                                addressText[lineNum].Text = hex2Dec(addressText[lineNum].Text);
                            }
                        }

                    }
                    prevAdrType[lineNum] = combo.SelectedItem.ToString();
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Túl nagy értéket adtál meg a " + (lineNum + 1) + ". sorban a címnél");
                    combo.SelectedItem = prevAdrType[lineNum];
                }
            }
        }

        private void lenTxtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;
            String name = text.Name;

            if (text == null)
                return;

            int lineNum = GetRow(text.Name);
            int cursorIndex = text.SelectionStart;
            int textLong = text.Text.Count();

            if (name.Contains("lenText"))
            {
                text.Text = Regex.Replace(text.Text, @"[^\d]", new MatchEvaluator(WrongValue));
            }

            text.SelectionLength = 0;
            text.SelectionStart = cursorIndex - (textLong - text.Text.Count());

            if (string.IsNullOrEmpty(text.Text))
                text.Text = "0";

        }

        private void dataTxtBox_TextChanged(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;
            String name = text.Name;

            if (text == null)
                return;

            int lineNum = GetRow(text.Name);
            int cursorIndex = text.SelectionStart;
            int textLong = text.Text.Count();

            if (name.Contains("dataText") & text.Text != "")
            {
                try
                {
                    switch (dataType[lineNum].SelectedItem)
                    {
                        case "HEX":
                            text.Text = Regex.Replace(text.Text, @"[^A-Fa-f0-9]", new MatchEvaluator(WrongValue));
                            lenText[lineNum].Text = (text.Text.Count() / 2 + text.Text.Count() % 2).ToString();
                            uint.Parse(text.Text, System.Globalization.NumberStyles.HexNumber);
                            break;

                        case "DEC":
                            text.Text = Regex.Replace(text.Text, @"[^\d]", new MatchEvaluator(WrongValue));
                            lenText[lineNum].Text = "4";
                            uint.Parse(text.Text);
                            break;

                        case "ASCII":
                            text.Text = Regex.Replace(text.Text, @"[^A-Za-z]", new MatchEvaluator(WrongValue));
                            lenText[lineNum].Text = text.Text.Count().ToString();
                            break;
                    }
                    SaveButton.Enabled = true;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Túl nagy értéket adtál meg a " + (lineNum + 1) + ". sorban a címnél");
                    SaveButton.Enabled = false;
                }
            }
            else
            {
                SaveButton.Enabled = true;

                if (text.Text == "")
                    lenText[lineNum].Text = "0";
            }

            int nextCursor = cursorIndex - (textLong - text.Text.Count());

            if (nextCursor < 0)
                text.SelectionStart = 0;
            else
                text.SelectionStart = nextCursor;
        }

        private void dataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (combo == null)
                return;

            int lineNum = GetRow(combo.Name);

            if (combo.Name.Contains("dataType") & dataText[lineNum].Text != "")
            {
                try
                {
                    switch (combo.SelectedItem)
                    {
                        case "ASCII":
                            if (prevDataType[lineNum] != "ASCII")
                            {
                                if (prevDataType[lineNum] == "HEX")
                                {
                                    dataText[lineNum].Text = hex2ASCII(dataText[lineNum].Text);
                                }
                                else
                                {
                                    dataText[lineNum].Text = dec2ASCII(dataText[lineNum].Text);
                                }
                            }
                            break;

                        case "HEX":
                            if (prevDataType[lineNum] != "HEX")
                            {
                                if (prevDataType[lineNum] == "DEC")
                                {
                                    dataText[lineNum].Text = dec2Hex(dataText[lineNum].Text);
                                }
                                else
                                {
                                    dataText[lineNum].Text = ASCII2Hex(dataText[lineNum].Text);
                                }
                            }
                            break;

                        case "DEC":
                            if (prevDataType[lineNum] != "DEC")
                            {
                                if (prevDataType[lineNum] == "HEX")
                                {
                                    dataText[lineNum].Text = hex2Dec(dataText[lineNum].Text);
                                }
                                else
                                {
                                    dataText[lineNum].Text = ASCII2Dec(dataText[lineNum].Text);
                                }
                            }
                            break;
                    }
                    prevDataType[lineNum] = combo.SelectedItem.ToString();
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Túl nagy értéket adtál meg a " + (lineNum + 1) + ". sorban az adatnál, ezt már nem lehet átváltani " + combo.SelectedItem + "-be.");
                    combo.SelectedItem = prevDataType[lineNum];
                }
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String name = button.Name;

            if (button == null)
                return;

            if (name.Contains("delbut"))
            {
                int lineNum = GetRow(button.Name);

                deleteRow(lineNum);
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String name = button.Name;

            if (button == null)
                return;

            if (name.Contains("add"))
                addRow();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String name = button.Name;

            if (button == null)
                return;

            if (name.Contains("save"))
            {
                if (fileName.Text == "")
                {
                    MessageBox.Show("Nincs megadva fájlnév!");
                    return;
                }

                try
                {
                    if (!Directory.Exists(path + "\\binary\\"))
                    {
                        Directory.CreateDirectory(path + "\\binary\\");
                    }


                    FileStream fs = File.Create(path + "\\binary\\" + fileName.Text + ".bin", 2048, FileOptions.None);
                    BinaryWriter bw = new BinaryWriter(fs);

                    changeFile();
                    bw.Write(buffer);

                    bw.Close();
                    fs.Close();

                    MessageBox.Show("Sikerült a fájl megírása.");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write the file to disk. Original error: " + ex.Message);

                }
            }

        }

        private void excel_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String name = button.Name;

            if (button == null)
                return;

            if (name.Contains("excel"))
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "excel documentum (.xls)|*.xls|excel documentum (.xlsx)|*.xlsx";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    openExcel(openFileDialog1.FileName);
                }
            }
        }

        private void readNextExcel_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            String name = button.Name;

            if (button == null)
                return;

            if (name.Contains("NextExcel"))
            {
                while (Panel1.RowCount > 1)
                    deleteRow(Panel1.RowCount - 1);

                openExcel(pathOfExcel);
            }
        }

        private void writeQR_Click(object sender, EventArgs e)
        {

        }
        // Other functions

        private void openExcel(string address)
        {
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(address);
                if (xlWorkbook != null)
                {
                    

                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    if(address == pathOfExcel)
                        readExcel(xlRange, lastRow);
                    else
                        readExcel(xlRange, 0);                   

                    pathOfExcel = address;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Marshal.ReleaseComObject(xlRange);
                    Marshal.ReleaseComObject(xlWorksheet);

                    xlWorkbook.Close();
                    Marshal.ReleaseComObject(xlWorkbook);

                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("Beolvasás sikeres");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);

            }
        }

        private void readExcel(Excel.Range xlRange, int delta )
        {
            int i = delta;
            int j = 0;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            if (colCount > 6 & xlRange.Cells[i + 1, 1] == null & xlRange.Cells[i + 1, 1].Value2 == null)
                return;

            fileName.Text = xlRange.Cells[i + 1, 1].Value.ToString();

            do
            {
                i++;
                MessageBox.Show(i.ToString());
                if (Panel1.RowCount < j + 1)
                    addRow();

                if (xlRange.Cells[i, 3] != null & xlRange.Cells[i, 3].Value != null)
                    addressType[j].SelectedItem = xlRange.Cells[i, 3].Value.ToString();
                else
                    addressType[j].SelectedItem = "HEX";

                if (xlRange.Cells[i, 2] != null & xlRange.Cells[i, 2].Value != null)
                    addressText[j].Text = xlRange.Cells[i, 2].Value.ToString();
                else
                    continue;               

                if (xlRange.Cells[i, 5] != null & xlRange.Cells[i, 5].Value != null)
                    dataType[j].SelectedItem = xlRange.Cells[i, 5].Value.ToString();
                else
                    dataType[j].SelectedItem = "HEX";

                if (xlRange.Cells[i, 4] != null & xlRange.Cells[i, 4].Value != null)
                    dataText[j].Text = xlRange.Cells[i, 4].Value.ToString(); 

                if (xlRange.Cells[i, 6] != null & xlRange.Cells[i, 6].Value != null)
                    lenText[j].Text = xlRange.Cells[i, 6].Value.ToString();

                j++;
            } while (i < rowCount & j < 10 & xlRange.Cells[i + 1, 1].Value == null);

            lastRow = i;
        }

        private string WrongValue(Match m)
        {
            SystemSounds.Beep.Play();
            return "";
        }

        private void deleteRow(int row)
        {
            int panelSize = Panel1.RowCount;


            for (int i = 0; i < panelSize - 1; i++)
            {
                if (i >= row)
                {
                    addressText[i].Text = addressText[i + 1].Text;
                    addressType[i].SelectedItem = addressType[i + 1].SelectedItem;
                    lenText[i].Text = lenText[i + 1].Text;
                    dataText[i].Text = dataText[i + 1].Text;
                    dataType[i].SelectedItem = dataType[i + 1].SelectedItem;
                }

            }

            if (panelSize != 1)
            {

                Panel1.Controls.RemoveByKey("delbut" + (panelSize - 1).ToString());
                Panel1.Controls.RemoveByKey("addressText" + (panelSize - 1).ToString());
                Panel1.Controls.RemoveByKey("addressType" + (panelSize - 1).ToString());
                Panel1.Controls.RemoveByKey("lenText" + (panelSize - 1).ToString());
                Panel1.Controls.RemoveByKey("dataText" + (panelSize - 1).ToString());
                Panel1.Controls.RemoveByKey("dataType" + (panelSize - 1).ToString());

                changeRow('-');
            }
            else
            {
                addressText[0].Text = "";
                addressType[0].SelectedItem = 0;
                lenText[0].Text = "";
                dataText[0].Text = "";
                dataType[0].SelectedItem = 0;
            }
        }

        private void changeRow(char direction)
        {
            int directionSign = 0;
            if (direction == '+')
                directionSign = 1;
            else
            {
                if (direction == '-')
                    directionSign = -1;
            }

            this.Height += 32 * directionSign;
            SaveButton.Location = new Point(SaveButton.Location.X, SaveButton.Location.Y + 32 * directionSign);
            fileName.Location = new Point(fileName.Location.X, fileName.Location.Y + 32 * directionSign);
            label7.Location = new Point(label7.Location.X, label7.Location.Y + 32 * directionSign);

            Panel1.RowCount += 1 * directionSign;
            Panel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31));
            Panel1.Size = new Size(Panel1.Size.Width, Panel1.Size.Height + 32 * directionSign);
        }

        private void addRow()
        {
            if (Panel1.RowCount != 10)
            {

                int rowNum = Panel1.RowCount;

                changeRow('+');

                new_delBut(rowNum);
                new_addrText(rowNum);
                new_addrType(rowNum);
                new_lenText(rowNum);
                new_dataText(rowNum);
                new_dataType(rowNum);

                Panel1.Controls.Add(delButt[rowNum], 0, rowNum);
                Panel1.Controls.Add(addressText[rowNum], 1, rowNum);
                Panel1.Controls.Add(addressType[rowNum], 2, rowNum);
                Panel1.Controls.Add(lenText[rowNum], 3, rowNum);
                Panel1.Controls.Add(dataText[rowNum], 4, rowNum);
                Panel1.Controls.Add(dataType[rowNum], 5, rowNum);
            }
        }

        private void changeFile()
        {
            int panelSize = Panel1.RowCount;

            for (int i = 0; i < panelSize; i++)
            {
                uint changeAddress = 0
                    ;
                if (addressText[i].Text != "")
                {
                    changeAddress = getAddress(i);
                }

                if (lenText[i].Text != "")
                {
                    int lenght = int.Parse(lenText[i].Text);
                    List<byte> data = new List<byte>();

                    data.AddRange(getData(i));

                    for (int j = 0; j < lenght; j++)
                    {
                        if (lenght - j > data.Count)
                        {
                            buffer[changeAddress + j] = 0;
                        }
                        else
                        {
                            buffer[changeAddress + j] = data[j - (lenght - data.Count())];
                        }
                    }
                }
            }
        }

        private int GetRow(string Name)
        {
            String num = Regex.Match(Name, @"\d+").Value;
            return Int32.Parse(num);
        }

        private uint getAddress(int row)
        {
            if ("HEX" == addressType[row].SelectedItem)
                return uint.Parse(addressText[row].Text, System.Globalization.NumberStyles.HexNumber);
            else
                return uint.Parse(addressText[row].Text);
        }

        private List<byte> getData(int row)
        {
            switch (dataType[row].SelectedItem)
            {
                case "HEX":
                    int num1 = int.Parse(dataText[row].Text, System.Globalization.NumberStyles.HexNumber);
                    return BitConverter.GetBytes(num1).Reverse().ToList();

                case "DEC":
                    int num2 = int.Parse(dataText[row].Text);
                    return BitConverter.GetBytes(num2).Reverse().ToList();

                case "ASCII":
                    return Encoding.ASCII.GetBytes(dataText[row].Text).ToList();

                default:
                    return null;
            }
        }

        // Converter functions between types

        private string hex2Dec(string HexText)
        {
            string hexValue = HexText;
            uint decNum = uint.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            return decNum.ToString();
        }

        private string dec2Hex(string DecText)
        {
            string decValue = DecText;
            uint decNum = uint.Parse(decValue);
            return decNum.ToString("X");
        }

        private string hex2ASCII(string hexText)
        {
            string ASCIIdata = "";

            if (hexText.Length % 2 == 1)
                hexText = "0" + hexText;

            for (int i = 0; i < hexText.Length; i = i + 2)
            {
                string sub = hexText.Substring(i, 2);

                uint hexData = uint.Parse(sub, System.Globalization.NumberStyles.HexNumber);

                ASCIIdata += Convert.ToChar(hexData).ToString();
            }
            return ASCIIdata;
        }

        private string dec2ASCII(string DecText)
        {
            uint decData = uint.Parse(DecText);
            string asciiData = "";

            while (decData != 0)
            {
                uint character = decData % 256;
                asciiData = Convert.ToChar(character).ToString() + asciiData;

                decData /= 256;
            }

            return asciiData;
        }

        private string ASCII2Dec(string ASCIIData)
        {
            uint decData = 0;

            foreach (char _eachChar in ASCIIData)
            {
                uint value = Convert.ToUInt32(_eachChar);
                decData = decData * 256 + value;
            }

            return decData.ToString();
        }

        private string ASCII2Hex(string ASCIIData)
        {
            string hexData = "";

            foreach (char _eachChar in ASCIIData)
            {
                uint value = Convert.ToUInt32(_eachChar);
                hexData += value.ToString("X");
            }
            return hexData;
        }  
    }
}