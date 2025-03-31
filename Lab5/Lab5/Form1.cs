using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Lab4
{
    public struct Sample
    {
        public int[] features;
        public int classNum;

        public Sample(int[] features, int classNum)
        {
            this.features = features;
            this.classNum = classNum;
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SolvingFuncMaker solvingFuncMaker;
        private int featuresCount = 2; // Default to 2 features

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ыыыы");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get number of features from input
            if (!int.TryParse(FeaturesCountTextBox.Text, out featuresCount) || featuresCount < 2)
            {
                MessageBox.Show("Please enter a valid number of features (>= 2)");
                return;
            }

            var random = new Random();
            solvingFuncMaker = new SolvingFuncMaker(featuresCount);
            solvingFuncMaker.classesAmount = Convert.ToInt32(ClassesAmountTextBox.Text);

            List<Sample> trainingSet = new List<Sample>(Convert.ToInt32(OgjectsAmountTextBox.Text));

            for (int i = 0; i < Convert.ToInt32(OgjectsAmountTextBox.Text); i++)
            {
                int[] features = new int[featuresCount];
                for (int j = 0; j < featuresCount; j++)
                {
                    features[j] = random.Next(700);
                }
                trainingSet.Add(new Sample(features, 0));
            }


            KMeansClassifier classifier = new KMeansClassifier();
            classifier.ClassesAmount = Convert.ToInt32(ClassesAmountTextBox.Text);  
            classifier.SetCenters(ref trainingSet);
            classifier.ClassifyImages(trainingSet.ToArray(),out trainingSet);


            solvingFuncMaker.functions = new List<int[]>(solvingFuncMaker.classesAmount);
            for (int i = 0; i < solvingFuncMaker.classesAmount; i++)
            {
                int[] function = new int[featuresCount + 1]; // +1 for the constant term
                for (int j = 0; j < featuresCount + 1; j++)
                {
                    function[j] = 0;
                }
                solvingFuncMaker.functions.Add(function);
            }

            int a = 0;
            bool editsHappened = false;
        a:
            foreach (var sample in trainingSet)
            {
                if (solvingFuncMaker.editFunctions(sample))
                    editsHappened = true;
            }
            if (editsHappened)
            {
                a++;
                editsHappened = false;
                goto a;
            }

            printFunctions(solvingFuncMaker.functions, dataGridView1);
            DrawSamplesOnPictureBox(trainingSet, pictureBox1);
            MessageBox.Show(Convert.ToString(a));
        }

        public void DrawSamplesOnPictureBox(List<Sample> samples, PictureBox pictureBox)
        {
            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                // Очищаем PictureBox
                g.Clear(Color.Black);

                // Настраиваем качество рисования
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Создаем кисти для разных классов
                Color[] classColors =
                [
                    Color.Red,
                    Color.Blue
                ];

                // Рисуем каждый sample
                foreach (var sample in samples)
                {
                    if (sample.features.Length < 2)
                        continue;

                    int x = sample.features[0];
                    int y = sample.features[1];
                    int classNum = sample.classNum % classColors.Length;

                    // Рисуем кружок для точки
                    using (Brush brush = new SolidBrush(classColors[classNum]))
                    {
                        int pointSize = 7;
                        g.FillEllipse(brush, x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
                    }
                }
            }

            pictureBox.Refresh();
        }

        void printFunctions(List<int[]> functionsList, DataGridView dataGridView)
        {
            if (functionsList == null || dataGridView == null)
                throw new ArgumentNullException("Входные параметры не могут быть null");

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            if (dataGridView.Columns.Count == 0)
            {
                dataGridView.Columns.Add("FunctionColumn", "Функция");
                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            for (int i = 0; i < functionsList.Count; i++)
            {
                int[] function = functionsList[i];
                StringBuilder functionString = new StringBuilder($"f({i}) = ");

                for (int j = 0; j < function.Length; j++)
                {
                    int coefficient = function[j];

                    // Пропускаем нулевые коэффициенты (кроме свободного члена)
                    if (coefficient == 0 && j < function.Length - 1)
                        continue;

                    // Обработка знака
                    if (j > 0 && coefficient != 0)
                    {
                        functionString.Append(coefficient > 0 ? " + " : " - ");
                    }
                    else if (coefficient < 0)
                    {
                        functionString.Append('-');
                    }

                    // Добавляем значение (по модулю для отрицательных)
                    int absValue = Math.Abs(coefficient);
                    if (absValue != 1 || j == function.Length - 1)
                    {
                        functionString.Append(absValue);
                    }

                    // Добавляем переменную (кроме свободного члена)
                    if (j < function.Length - 1)
                    {
                        functionString.Append($"x{j + 1}");
                    }
                }

                dataGridView.Rows.Add(functionString.ToString());
            }
        }

       private void button2_Click(object sender, EventArgs e)
        {
            string[] featureValues = textBox1.Text.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (featureValues.Length != featuresCount)
            {
                MessageBox.Show($"Please enter exactly {featuresCount} feature values separated by spaces");
                return;
            }

            int[] features = new int[featuresCount];
            for (int i = 0; i < featuresCount; i++)
            {
                if (!int.TryParse(featureValues[i], out features[i]))
                {
                    MessageBox.Show("Please enter valid integer values for all features");
                    return;
                }
            }

            int classifiedClass = solvingFuncMaker.classify(features);
            MessageBox.Show($"Объект с признаками {textBox1.Text} относится к классу: {classifiedClass}");
        }

        void printSamples(List<Sample> samples, DataGridView dataGridView)
        {
            if (samples == null || dataGridView == null)
                throw new ArgumentNullException("Input parameters cannot be null");

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            // Add columns for each feature
            for (int i = 0; i < featuresCount; i++)
            {
                dataGridView.Columns.Add($"Feature{i + 1}", $"Feature {i + 1}");
            }
            dataGridView.Columns.Add("Class", "Class");

            // Configure column widths
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // Add rows with sample data
            foreach (var sample in samples)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView);

                // Add feature values
                for (int i = 0; i < featuresCount; i++)
                {
                    row.Cells[i].Value = sample.features[i];
                }

                // Add class number
                row.Cells[featuresCount].Value = sample.classNum;

                dataGridView.Rows.Add(row);
            }
        }
    }
}
