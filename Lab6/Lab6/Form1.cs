using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab6
{
    public partial class Form1 : Form
    {
        private readonly List<Color> colors = new List<Color>
        {
            Color.Red, Color.Blue, Color.DodgerBlue, Color.DarkMagenta, Color.BlueViolet,
            Color.DeepPink,Color.Firebrick,Color.ForestGreen,Color.MidnightBlue, Color.Green,
            Color.Aqua, Color.DarkOrchid, Color.RoyalBlue, Color.DarkBlue, Color.Beige,
            Color.Salmon, Color.Sienna, Color.PowderBlue,  Color.Plum, Color.LightSalmon,
            Color.DarkOrchid, Color.Olive, Color.YellowGreen, Color.Violet,  Color.Gold,
            Color.Yellow, Color.LightCyan, Color.LightBlue, Color.Turquoise
        };
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }
        private void InitializeChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.Title = "";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LineWidth = 1;
            chart1.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 60;
            chart1.ChartAreas[0].AxisY.Maximum = 10 + 0.01;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Title = "";
            chart1.ChartAreas[0].AxisY.Interval = 1;
            chart1.ChartAreas[0].AxisY.LineWidth = 1;
        }

        private void VisualizeHierarchy()
        {
            var hierarchy = new HierarchyBuilder().BuildHierarchy(points);

            DrawDendrogram(hierarchy);
        }

        private void DrawDendrogram(ClassNode root)
        {
            chart1.Series.Clear();

            Series series = new Series("Dendrogram")
            {
                ChartType = SeriesChartType.Line,
                CustomProperties = "DrawSideBySide=False",
       
            };

            series.BorderWidth = 3;       // Толщина линии
            series.Color = Color.Black;
            // Рекурсивно добавляем элементы дендрограммы
            double height = getDendrogramHeight(root) + 10;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = points.Length;
            chart1.ChartAreas[0].AxisY.Maximum = height;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            AddDendrogramNodes(series, root, points.Length, height);

            chart1.Series.Add(series);
            chart1.Invalidate();
        }

        private double getDendrogramHeight(ClassNode root)
        {
            if (root == null)
                return 0;
            double h = 0;

            if (root.Child != null) 
            {
                h += getDendrogramHeight(root.Child);
                h += root.distanceToChild;
            }
            return h;
        }

        private void AddDendrogramNodes(Series series, ClassNode node, double xPos, double yStart)
        {
            if (node == null) return;
           
            foreach (var point in node.Points)
            {
                series.Points.AddXY(xPos, yStart);
                series.Points[series.Points.Count - 1].Color = PointVisualizer.GetClassColor(node.ClassNum);
                series.Points.AddXY(xPos, 0);
                series.Points[series.Points.Count - 1].Color = PointVisualizer.GetClassColor(node.ClassNum);
                series.Points.AddXY(xPos, yStart);
                series.Points[series.Points.Count - 1].Color = PointVisualizer.GetClassColor(node.ClassNum);
                xPos--;
            }
            double childY = 0;
            if (node.Child != null)
            {
                childY = yStart - node.distanceToChild;
                series.Points.AddXY(xPos, childY);
            }

            if (node.Child != null)
            {
                AddDendrogramNodes(series, node.Child,
                                 xPos++, childY);
            }
        }
    




        PointWithClass[] points;
        double[,] distances;
        PointVisualizer drawer;

        private void initButton_Click(object sender, EventArgs e)
        {

            drawer = new(pictureBox1, dataGridView1);
            points = GenerateRandomPoints(Convert.ToInt32(textBox1.Text), 40, 150);        
            drawer.DrawPoints(points);
            drawer.ShowDistanceTable(points);
            distances = CalculateDistanceTable(points);
            
            pictureBox1.Update();
        }

        private void ClassifyButton_Click(object sender, EventArgs e)
        {      
            if (radioButton2.Checked)
            {

            }
            ClassifyPoints();
            drawer.DrawPoints(points);
            drawer.ShowDistanceTable(points);
            pictureBox1.Update();
            VisualizeHierarchy();
        }

        private void ClassifyPoints()
        {

            while (points.Any(p => p.ClassNum == -1))
            {
                int classCounter = points.Max(p => p.ClassNum) + 1; 

                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].ClassNum != -1) continue;
                    double minDistance = double.MaxValue;
                    int nearestPointIndex = -1;

                    for (int j = 0; j < points.Length; j++)
                    {
                        if (i == j) continue;

                        double dist = distances[i, j];
                        if (dist <= minDistance)
                        {
                            minDistance = dist;
                            nearestPointIndex = j;
                        }
                    }

                    if (nearestPointIndex != -1)
                    {
                        if (points[nearestPointIndex].ClassNum == -1)
                        {
                            points[i].ClassNum = classCounter;
                            points[nearestPointIndex].ClassNum = classCounter;
                            classCounter++;
                        }
                        else
                        {
                            points[i].ClassNum = points[nearestPointIndex].ClassNum;
                        }
                    }
                    else
                    {
                        points[i].ClassNum = classCounter++;
                    }
                }
            }

            if (points.Any(p => p.ClassNum == -1))
            {
                int newClass = points.Max(p => p.ClassNum) + 1;
                foreach (var point in points.Where(p => p.ClassNum == -1))
                {
                    point.ClassNum = newClass;
                }
            }
        }

        // Генерация массива случайных точек
        public static PointWithClass[] GenerateRandomPoints(int count, int min, int max)
        {
            Random random = new Random();
            PointWithClass[] points = new PointWithClass[count];

            for (int i = 0; i < count; i++)
            {
                double x = random.NextDouble() * (max - min) + min;
                double y = random.NextDouble() * (max - min) + min;
                points[i] = new PointWithClass(x, y);
            }

            return points;
        }
        // Расчет таблицы расстояний между всеми точками
        public static double[,] CalculateDistanceTable(PointWithClass[] points)
        {
            int count = points.Length;
            double[,] distances = new double[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0; // Расстояние от точки до самой себя равно 0
                    }
                    else
                    {
                        distances[i, j] = CalculateDistance(points[i], points[j]);
                    }
                }
            }

            return distances;
        }
        // Расчет расстояния между двумя точками
        public static double CalculateDistance(PointWithClass a, PointWithClass b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
