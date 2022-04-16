using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laboratory2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[,] leftMatrixTextBoxes, rightMatrixTextBoxes;
        private int lastSize;
        private Matrix<int> result;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateBtnsInteract() {
            bool state = false;
            if (leftMatrixTextBoxes.Length > 0 && rightMatrixTextBoxes.Length > 0)
            {
                state = true;
            }
            btnRand.IsEnabled = state;
            btnCalculate.IsEnabled = state;
            btnSave.IsEnabled = state;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            leftMatrixTextBoxes = UpdateGrid(LeftGrid, int.Parse(textBoxSize.Text));
            rightMatrixTextBoxes = UpdateGrid(RightGrid, int.Parse(textBoxSize.Text));
            UpdateBtnsInteract();
        }


        private void btnRand_Click(object sender, RoutedEventArgs e)
        {
            RandomGrid(leftMatrixTextBoxes);
            RandomGrid(rightMatrixTextBoxes);
        }
        private void RandomGrid(TextBox[,] textBoxes)
        {
            Random rand = new Random(DateTime.Now.Second+ DateTime.Now.Millisecond);
            foreach (TextBox item in textBoxes) {
                item.Text = rand.Next(0, 10).ToString();
            }
        }

        public string GetPathFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Result"; // Default file name
            dlg.DefaultExt = ".csv"; // Default file extension
            dlg.Filter = "CSV Table (.csv)|*.csv"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                return dlg.FileName;
            }
            else return null;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Matrix<int> matrixLeft = new Matrix<int>(lastSize, lastSize);
            Matrix<int> matrixRight = new Matrix<int>(lastSize, lastSize);
            for (int i = 0; i < lastSize; i++)
            {
                for (int j = 0; j < lastSize; j++)
                {
                    matrixLeft[i, j] = int.Parse(leftMatrixTextBoxes[i, j].Text);
                    matrixRight[i, j] = int.Parse(rightMatrixTextBoxes[i, j].Text);
                }
            }
            if (operation.SelectedIndex==0) result = matrixLeft + matrixRight;
            else result = matrixLeft * matrixRight;
            stopwatch.Stop();
            timeValue.Text = stopwatch.Elapsed.TotalSeconds.ToString();
            TextBox[,] resultFields = UpdateGrid(ResultGrid, lastSize);
            for (int i = 0; i < lastSize; i++)
                for (int j = 0; j < lastSize; j++)
                    resultFields[i, j].Text = result[i, j].ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string path = GetPathFile();
            if (path != null) result.SaveFromCSV(path);
        }

        private TextBox[,] UpdateGrid(UniformGrid uniformGrid,int size) {
            lastSize = size;
            TextBox[,] result = new TextBox[size, size];
            //Задание опций гридов и отчистка гридов
            uniformGrid.Rows = size;
            uniformGrid.Columns = size;
            uniformGrid.Children.Clear();

            for (int i = 0;i<size;i++) {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = new TextBox();
                    result[i, j].Text = "0";
                    uniformGrid.Children.Add(result[i, j]);
                }
            }
            return result;
        }

        
    }

}
