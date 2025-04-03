namespace Lab6
{
    public class ClassNode
    {
        public int ClassNum { get; }
        public List<PointWithClass> Points { get; }
        public ClassNode Child { get; set; }
        public double distanceToChild { get; set; }

        public ClassNode(int classId, IEnumerable<PointWithClass> points, double DistanceToChild)
        {
            ClassNum = classId;
            Points = points.Where(p => p.ClassNum == classId).ToList();
            Child = null;
            distanceToChild = DistanceToChild;
        }

        public override string ToString()
        {
            return $"Class {ClassNum} ({Points.Count} points)" + (Child != null ? $" -> {Child}" : "");
        }
    }

    public class HierarchyBuilder
    {
        private readonly int _distanceMode; // 0 - максимальное расстояние, 1 - минимальное расстояние

        public HierarchyBuilder(int distanceMode = 1)
        {
            _distanceMode = distanceMode;
        }

        public ClassNode BuildHierarchy(PointWithClass[] allPoints)
        {
            if (allPoints == null || !allPoints.Any())
                return null;

            var root = new ClassNode(0, allPoints, 0);
            BuildHierarchyRecursive(root, allPoints, new HashSet<int> { 0 });
            return root;
        }

        private void BuildHierarchyRecursive(ClassNode current, PointWithClass[] allPoints, HashSet<int> processedClasses)
        {
            int? nearestClass = FindNearestClass(current, allPoints, processedClasses);

            if (nearestClass.HasValue)
            {
                processedClasses.Add(nearestClass.Value);
                current.distanceToChild = FindMinDistanceBetweenClasses(allPoints, current.ClassNum, nearestClass);
                current.Child = new ClassNode(nearestClass.Value, allPoints, 0);
                BuildHierarchyRecursive(current.Child, allPoints, processedClasses);
            }
        }

        public double FindMinDistanceBetweenClasses(PointWithClass[] points, int class1, int? class2)
        {
            if (class1 == class2)
                return 0;
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            var pointsList = points.ToList();
            double extremeDistance = _distanceMode == 0 ? double.MinValue : double.MaxValue;

            var class1Points = pointsList.Where(p => p.ClassNum == class1).ToList();
            var class2Points = pointsList.Where(p => p.ClassNum == class2).ToList();

            if (class1Points.Count == 0 || class2Points.Count == 0)
            {
                throw new ArgumentException("Один из классов не содержит точек");
            }

            foreach (var p1 in class1Points)
            {
                foreach (var p2 in class2Points)
                {
                    double distance = p1.DistanceTo(p2);
                    if ((_distanceMode == 1 && distance < extremeDistance) ||
                        (_distanceMode == 0 && distance > extremeDistance))
                    {
                        extremeDistance = distance;
                    }
                }
            }

            return extremeDistance;
        }

        private int? FindNearestClass(ClassNode current, PointWithClass[] allPoints, HashSet<int> processedClasses)
        {
            var remainingClasses = allPoints
                .Select(p => p.ClassNum)
                .Distinct()
                .Where(c => !processedClasses.Contains(c))
                .ToList();

            if (!remainingClasses.Any())
                return null;

            double extremeDistance = _distanceMode == 0 ? double.MinValue : double.MaxValue;
            int extremeClass = -1;

            foreach (var classId in remainingClasses)
            {
                double classDistance = CalculateMinDistanceBetweenClasses(
                    current.Points,
                    allPoints.Where(p => p.ClassNum == classId));

                if ((_distanceMode == 1 && classDistance < extremeDistance) ||
                    (_distanceMode == 0 && classDistance > extremeDistance))
                {
                    extremeDistance = classDistance;
                    extremeClass = classId;
                }
            }

            return extremeClass;
        }

        private double CalculateMinDistanceBetweenClasses(IEnumerable<PointWithClass> class1, IEnumerable<PointWithClass> class2)
        {
            double extremeDistance = _distanceMode == 0 ? double.MinValue : double.MaxValue;

            foreach (var p1 in class1)
            {
                foreach (var p2 in class2)
                {
                    double distance = p1.DistanceTo(p2);
                    if ((_distanceMode == 1 && distance < extremeDistance) ||
                        (_distanceMode == 0 && distance > extremeDistance))
                    {
                        extremeDistance = distance;
                    }
                }
            }

            return extremeDistance;
        }
    }
}