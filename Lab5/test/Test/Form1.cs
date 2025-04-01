using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        private List<PointData> points = new List<PointData>
        {
            new PointData { X = -1, Y = 0, Class = 0 },
            new PointData { X = 1, Y = 1, Class = 0 },
            new PointData { X = 2, Y = 0, Class = 1 },
            new PointData { X = 1, Y = -2, Class = 1 }
        };

        private double[] result = new double[4] { 0, 0, 0, 0 };
        private Bitmap graphBitmap;
        private const int Step = 50;
        private const int CenterX = 400;
        private const int CenterY = 300;

        public Form1()
        {
            InitializeComponent();
            InitializeGraph();
            CalculateSeparatingFunction();
            DrawGraph();
        }

        private void InitializeGraph()
        {
            graphBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = graphBitmap;
        }

        private void CalculateSeparatingFunction()
        {
            double correction = 1;
            int iterationsCount = 0;
            bool ok = true;

            while (ok && iterationsCount++ < 1000)
            {
                ok = false;
                for (int i = 0; i < 4; i++)
                {
                    result = SumArrays(result, MultiplyArrayByScalar(PotentialFunction(points[i]), correction));
                    var nextPoint = points[i != 3 ? i + 1 : 0];
                    var functionValue = GetFunctionValue(result, nextPoint);
                    correction = 0;

                    if (functionValue < 0 && (i < 1 || i > 2))
                    {
                        correction = 1;
                    }
                    if (functionValue > 0 && i > 0 && i < 3)
                    {
                        correction = -1;
                    }
                    if (correction != 0) ok = true;
                }
            }

            lblFunction.Text = "Separating function: " + ResultToString(result);
        }

        private void DrawGraph()
        {
            using (Graphics g = Graphics.FromImage(graphBitmap))
            {
                // g.Clear(Color.White);

                Pen pen = new Pen(Color.White, 2);
                // Draw axes
                g.DrawLine(pen, CenterX, 0, CenterX, pictureBox.Height); // Y axis
                g.DrawLine(pen, 0, CenterY, pictureBox.Width, CenterY);   // X axis

                // Draw separating function
                List<PointF> functionPoints = new List<PointF>();
                for (float x = -20; x < 20; x += 0.01f)
                {
                    float y = (float)GetY(result, x);
                    functionPoints.Add(new PointF(CenterX + x * Step, CenterY - y * Step));
                }
                pen = new Pen(Color.Red, 3);
                if (functionPoints.Count > 1)
                {
                    g.DrawLines(pen, functionPoints.ToArray());
                }

                // Draw points
                foreach (var point in points)
                {
                    Color pointColor = point.Class == 0 ? Color.Blue : Color.Green;
                    int pointSize = 12;
                    g.FillEllipse(new SolidBrush(pointColor),
                        CenterX + (float)point.X * Step - pointSize / 2,
                        CenterY - (float)point.Y * Step - pointSize / 2,
                        pointSize, pointSize);
                }

                pictureBox.Invalidate();
            }
        }

        private static string ResultToString(double[] res)
        {
            if (res[2] != 0)
            {
                return $"y=({-res[0]}*x{(-res[3] < 0 ? "" : "+")}{-res[3]})/({res[2]}*x{(res[1] < 0 ? "" : "+")}{res[1]})";
            }
            if (res[1] != 0)
            {
                return $"y={-res[0] / res[1]}*x{(-res[3] / res[1] < 0 ? "" : "+")}{-res[3] / res[1]}";
            }
            return $"x={-res[3] / res[0]}";
        }

        private static double GetY(double[] func, double x)
        {
            return -(func[0] * x + func[3]) / (func[2] * x + func[1]);
        }

        private static double[] PotentialFunction(PointData point)
        {
            return
            [
                4 * point.X,
                4 * point.Y,
                16 * point.X * point.Y,
                1
            ];
        }

        private static double GetFunctionValue(double[] func, PointData point)
        {
            return func[0] * point.X + func[1] * point.Y + func[2] * point.X * point.Y + func[3];
        }

        private static double[] SumArrays(double[] a, double[] b)
        {
            int length = Math.Max(a.Length, b.Length);
            double[] temp = new double[length];
            for (int i = 0; i < length; i++)
            {
                double aVal = i < a.Length ? a[i] : 0;
                double bVal = i < b.Length ? b[i] : 0;
                temp[i] = aVal + bVal;
            }
            return temp;
        }

        private static double[] MultiplyArrayByScalar(double[] array, double scalar)
        {
            double[] result = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i] * scalar;
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtX1.Text, out double x) && double.TryParse(txtY1.Text, out double y))
            {
                var testPoint = new PointData { X = x, Y = y };
                var functionValue = GetFunctionValue(result, testPoint);
                int pointClass = functionValue >= 0 ? 0 : 1;

                lblResult.Text = $"Belongs to class {pointClass + 1}";

                // Draw the new point
                using (Graphics g = Graphics.FromImage(graphBitmap))
                {
                    Color pointColor = pointClass == 0 ? Color.Blue : Color.Green;
                    int pointSize = 10;
                    g.FillEllipse(new SolidBrush(pointColor),
                        CenterX + (float)x * Step - pointSize / 2,
                        CenterY - (float)y * Step - pointSize / 2,
                        pointSize, pointSize);
                    pictureBox.Invalidate();
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for coordinates.");
            }
        }

        private static List<PointData> GenerateRandomPoints(int count)
        {
            List<PointData> randomPoints = new List<PointData>();
            Random random = new();

            for (int i = 0; i < count; i++)
            {
                randomPoints.Add(new PointData
                {
                    X = random.Next(-40, 40),  
                    Y = random.Next(-40, 40),
                    Class = 0   
                });
            }

            return randomPoints;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int pointSize = 10;
            List<PointData> points = GenerateRandomPoints(250);
            foreach (PointData point in points)
            {
                var correctPoint = point;
                correctPoint.X = point.X / 4;
                correctPoint.Y = point.Y / 4;

                var functionValue = GetFunctionValue(result, correctPoint);

                int pointClass = functionValue >= 0 ? 0 : 1;
                using (Graphics g = Graphics.FromImage(graphBitmap))
                {
                    Color pointColor = pointClass == 0 ? Color.Blue : Color.Green;
                    
                    g.FillEllipse(new SolidBrush(pointColor),
                        CenterX + (float)point.X * Step  - pointSize / 2,
                        CenterY - (float)point.Y * Step - pointSize / 2,
                        pointSize, pointSize);
                    //pictureBox.Invalidate();
                }
                DrawGraph();
            }
        }
    }

    public class PointData
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Class { get; set; }
    }
}