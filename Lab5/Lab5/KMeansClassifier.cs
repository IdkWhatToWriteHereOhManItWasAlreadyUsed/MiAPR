using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{

    public class KMeansClassifier
    {
        public List<Sample> ClassCenters = new List<Sample>();
        public int ClassesAmount { get; set; }

        public void SetCenters(ref List<Sample> samples)
        {
            for (int i = 0; i < ClassesAmount; i++)
            {
                var temp = samples[i];
                temp.classNum = i;
                ClassCenters.Add(temp);
            }
        }

        public void ClassifyImages(Sample[] images, out List<Sample> outa)
        {
            outa = new List<Sample>();

            // Classify non-center images
            for (int i = 0; i < images.Length; i++)
            {
                if (!ClassCenters.Any(c => c.features.SequenceEqual(images[i].features) && c.classNum == images[i].classNum))
                {
                    double minDistance = double.MaxValue;
                    for (int j = 0; j < ClassCenters.Count; j++)
                    {
                        double distance = 0;
                        // Calculate Euclidean distance for all features
                        for (int k = 0; k < images[i].features.Length; k++)
                        {
                            distance += Math.Pow(images[i].features[k] - ClassCenters[j].features[k], 2);
                        }

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            images[i].classNum = ClassCenters[j].classNum;
                        }
                    }
                }
            }
            outa = images.ToList();
        }
    }
}
