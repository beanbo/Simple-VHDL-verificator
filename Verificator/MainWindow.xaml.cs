using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Verificator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_OpenFileDlg(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                if (sender.Equals(inputButton))
                    txtBox_FileNameInput.Text = dialog.FileName;
                else if (sender.Equals(outputButton))
                    txtBox_FileNameOutput.Text = dialog.FileName;
            }
        }

        private void Click_ValidateCurrentFile(object sender, RoutedEventArgs e)
        {
            GenerateFile();
            if (ValidateFile())
                MessageBox.Show("Результаты совпадают!");
            else
                MessageBox.Show("Результаты не совпадают!");
        }

        private void GenerateFile()
        {
            string sInputFile = System.IO.File.ReadAllText(txtBox_FileNameInput.Text);
            string sFileText = "";

            List<List<int>> matrix = new List<List<int>>();

            while (sInputFile.Length != 0)
            {
                List<int> arr = new List<int>();

                while (sInputFile.Length != 0 && sInputFile[0] != '\r')
                {
                    if (sInputFile[0] == '0')
                        arr.Add(0);
                    else if (sInputFile[0] == '1')
                        arr.Add(1);

                    sInputFile = sInputFile.Remove(0, 1);
                }

                if (sInputFile.Length != 0)
                    sInputFile = sInputFile.Remove(0, 2);

                matrix.Add(arr);
            }

            List<int> result = FindGraphComponent(matrix);

            for (int counter = 0; counter < result.Count - 1; counter++)
                sFileText += result[counter];

            sFileText += "\r\n";
            sFileText += result[result.Count - 1];
            sFileText += "\r\n";

            System.IO.File.WriteAllText("out.txt", sFileText);
        }

        private bool ValidateFile()
        {
            if (txtBox_FileNameOutput.Text.Length == 0)
                return false;

            string sMyFile = System.IO.File.ReadAllText("out.txt");
            string sVHDLFile = System.IO.File.ReadAllText(txtBox_FileNameOutput.Text);

            txtbox_result.Text = sVHDLFile;

            return sMyFile == sVHDLFile;
        }

        List<int> FindGraphComponent(List<List<int>> matrix)
        {
            int N = matrix.Count();
            List<int> max_arr = GetNullList(N), all_arr = GetNullList(N),
                arr = GetNullList(N),graph = GetNullList(N);

            int size = 0, max_size = 0;

            for (int k = 0; k < N; k++)
            {
                if (all_arr[k] == 0)
                {
                    arr = GetNullList(N);
                    size = 0;

                    for (int i = k; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (matrix[i][j] == 1)
                            {
                                arr[i] = 1;
                                all_arr[i] = 1;
                                size += 1;
                                break;
                            }
                        }

                        if (arr[i] == 1) break;
                        else if (arr[i] == 0)
                        {
                            //arr[i] = 1;
                            all_arr[i] = 1;
                        }
                    }

                    for (int b = 1; b < N; b++)
                    {
                        for (int i = 0; i < N; i++)
                        {
                            if (arr[i] == b)
                            {
                                for (int j = 0; j < N; j++)
                                {
                                    if (matrix[i][j] == 1 && arr[j] == 0 && arr[i] == b)
                                    {
                                        arr[j] = b + 1;
                                        all_arr[j] = 1;
                                        size += 1;
                                    }
                                }
                            }
                        }
                    }

                    if (size > max_size)
                    {
                        max_size = size;
                        max_arr = arr;
                    }
                }
            }

            for (int i = 0; i < N; i++)
                graph[i] = max_arr[i] != 0 ? 1 : 0;

            graph.Add(max_size);

            return graph;
        }

        List<int> GetNullList(int size)
        {
            List<int> list = new List<int>(size);

            for (int i = 0; i < size; i++)
                list.Add(0);

            return list;
        }
    }
}
