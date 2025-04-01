namespace Lab6
{
    public class PointWithClass(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;
        public int ClassNum { get; set; } = -1;
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PointWithClass[] points;
        double[,] distances;
        PointVisualizer drawer;

        private void initButton_Click(object sender, EventArgs e)
        {
            drawer = new(pictureBox1, dataGridView1);
            points = GenerateRandomPoints(200, 40, 320);        
            drawer.DrawPoints(points);
            drawer.ShowDistanceTable(points);
            distances = CalculateDistanceTable(points);
            pictureBox1.Update();
        }

        private void ClassifyButton_Click(object sender, EventArgs e)
        {
            
            ClassifyPoints();
            drawer.DrawPoints(points);
            drawer.ShowDistanceTable(points);
            pictureBox1.Update();
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

        // √енераци€ массива случайных точек
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
        // –асчет таблицы рассто€ний между всеми точками
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
                        distances[i, j] = 0; // –ассто€ние от точки до самой себ€ равно 0
                    }
                    else
                    {
                        distances[i, j] = CalculateDistance(points[i], points[j]);
                    }
                }
            }

            return distances;
        }
        // –асчет рассто€ни€ между двум€ точками
        public static double CalculateDistance(PointWithClass a, PointWithClass b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
