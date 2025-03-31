
namespace Lab4
{
    class SolvingFuncMaker
    {
        private const int c = 1;
        public int classesAmount;
        public List<int[]> functions = new List<int[]>();
        private int featuresCount;

        public SolvingFuncMaker(int featuresCount)
        {
            this.featuresCount = featuresCount;
        }

        public bool editFunctions(Sample sample)
        {
            int funcValueForThisSample = CalculateFunctionValue(functions[sample.classNum], sample.features);
            int maxFuncIndex = sample.classNum;

            for (int i = 0; i < functions.Count; i++)
            {
                if (i == sample.classNum) continue;

                int curr = CalculateFunctionValue(functions[i], sample.features);
                if (curr >= funcValueForThisSample)
                {
                    maxFuncIndex = i;
                    break;
                }
            }

            if (maxFuncIndex == sample.classNum)
                return false;

            for (int i = 0; i < functions.Count; i++)
            {
                if (i == sample.classNum)
                {
                    // Increase the function for the correct class
                    for (int j = 0; j < featuresCount; j++)
                    {
                        functions[i][j] += sample.features[j];
                    }
                    functions[i][featuresCount] += c; // constant term
                }
                else if (CalculateFunctionValue(functions[i], sample.features) >= funcValueForThisSample)
                {
                    // Decrease the function for incorrect classes
                    for (int j = 0; j < featuresCount; j++)
                    {
                        functions[i][j] -= sample.features[j];
                    }
                    functions[i][featuresCount] -= c; // constant term
                }
            }
            return true;
        }

        public int classify(int[] features)
        {
            int classNum = 0;
            int max = int.MinValue;

            for (int i = 0; i < functions.Count; i++)
            {
                int curr = CalculateFunctionValue(functions[i], features);
                if (curr > max)
                {
                    max = curr;
                    classNum = i;
                }
            }
            return classNum;
        }

        private int CalculateFunctionValue(int[] function, int[] features)
        {
            int sum = function[featuresCount]; // Start with constant term
            for (int i = 0; i < featuresCount; i++)
            {
                sum += function[i] * features[i];
            }
            return sum;
        }
    }
}