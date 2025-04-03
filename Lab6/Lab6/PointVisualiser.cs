using System.Data;
using Lab6;
public class PointVisualizer
{
    public PictureBox pictureBox;
    public DataGridView dataGridView;
    private int pictureBoxWidth;
    private int pictureBoxHeight;

    public PointVisualizer(PictureBox pb, DataGridView dgv)
    {
        pictureBox = pb;
        dataGridView = dgv;
        pictureBoxWidth = pb.Width;
        pictureBoxHeight = pb.Height;
    }

    // Отрисовка точек на PictureBox
    public void DrawPoints(PointWithClass[] points)
    {
        Bitmap bmp = new Bitmap(pictureBoxWidth, pictureBoxHeight);
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.White);
            // Рисуем точки
            int pointSize = 16;
            foreach (var point in points)
            {
                // Масштабируем координаты под размер PictureBox
                int x = (int)(point.X * pictureBoxWidth / 177)  ;
                int y = pictureBoxHeight - (int)(point.Y * pictureBoxHeight / 177);

                g.FillEllipse(GetClassBrush(point.ClassNum), x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
               // g.DrawEllipse(Pens.Black, x - pointSize / 2, y - pointSize / 2, pointSize, pointSize);
            }
        }

        pictureBox.Image = bmp;
    }

    public static Brush GetClassBrush(int classNum)
    {
        if (classNum < 0) return Brushes.Black;

        // Генерация на основе золотого угла (137.5° - оптимальное распределение)
        double goldenAngle = 137.5;
        double hue = (classNum * goldenAngle) % 360;

        float saturation = 0.9f;
        float brightness = 0.9f;

        Color color = ColorFromHSV(hue, saturation, brightness);


        return new SolidBrush(color);
    }

    // Вспомогательный метод для преобразования HSV в RGB
    public static Color ColorFromHSV(double hue, float saturation, float value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        double f = hue / 60 - Math.Floor(hue / 60);

        value *= 255;
        byte v = Convert.ToByte(value);
        byte p = Convert.ToByte(value * (1 - saturation));
        byte q = Convert.ToByte(value * (1 - f * saturation));
        byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromArgb(v, t, p),
            1 => Color.FromArgb(q, v, p),
            2 => Color.FromArgb(p, v, t),
            3 => Color.FromArgb(p, q, v),
            4 => Color.FromArgb(t, p, v),
            _ => Color.FromArgb(v, p, q),
        };
    }

    // Расчет и отображение таблицы расстояний в DataGridView
    public void ShowDistanceTable(PointWithClass[] points)
    {
        // Сортируем точки по номеру класса
        var sortedPoints = points.OrderBy(p => p.ClassNum).ToArray();
        int count = sortedPoints.Length;
        double[,] distances = new double[count, count];

        // Рассчитываем расстояния между отсортированными точками
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                if (i == j)
                {
                    distances[i, j] = 0;
                }
                else
                {
                    distances[i, j] = CalculateDistance(sortedPoints[i], sortedPoints[j]);
                }
            }
        }

        // Создаем таблицу для DataGridView
       DataTable dt = new DataTable();
    dt.Columns.Add("Точка", typeof(string));
    for (int i = 0; i < count; i++)
    {
        dt.Columns.Add($"Точка {i + 1} (Кл.{sortedPoints[i].ClassNum})", typeof(double));
    }

    // Добавляем строки и сразу кэшируем цвета
    var colorCache = new Dictionary<int, Color>();
    for (int i = 0; i < count; i++)
    {
        DataRow row = dt.NewRow();
        row[0] = $"Точка {i + 1} (Кл.{sortedPoints[i].ClassNum})";
        
        for (int j = 0; j < count; j++)
        {
            row[j + 1] = Math.Round(distances[i, j], 2);
        }
        
        dt.Rows.Add(row);
        
        // Кэшируем цвет
        if (!colorCache.ContainsKey(sortedPoints[i].ClassNum))
        {
            colorCache[sortedPoints[i].ClassNum] = GetClassColor(sortedPoints[i].ClassNum);
        }
    }

    dataGridView.DataSource = dt;
    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

    // Оптимизированный обработчик
    dataGridView.CellFormatting += (sender, e) =>
    {
        if (e.RowIndex >= 0 && e.RowIndex < sortedPoints.Length)
        {
            int classNum = sortedPoints[e.RowIndex].ClassNum;
            if (colorCache.ContainsKey((int)classNum))
            {
                dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = colorCache[classNum];
            }        
            dataGridView.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
        }
    };
    }
    private double CalculateDistance(PointWithClass a, PointWithClass b)
    {
        double dx = a.X - b.X;
        double dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public static Color GetClassColor(int classNum)
    {
        if (classNum < 0) return Color.White;

        // Генерация на основе золотого угла (137.5° - оптимальное распределение)
        double goldenAngle = 137.5;
        double hue = (classNum * goldenAngle) % 360;

        // Настройки насыщенности и яркости
        float saturation = 0.9f;
        float brightness = 0.9f;
        return ColorFromHSV(hue, saturation, brightness);
    }
}