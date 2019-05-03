using MeshCAD.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MeshCAD
{
    public class DarParser
    {

        public Model Parse(StreamReader modelFile)
        {
            var coordSizeStr = modelFile.ReadLine();
            int coordSize = int.Parse(coordSizeStr
                .Substring(coordSizeStr
                .LastIndexOf('=') + 1));
            var coords = new Dictionary<int, Vertex>(coordSize);
            for (int i = 0; i < coordSize; i++)
            {
                string coordNumberStr = modelFile.ReadLine();
                int coordNumber = int.Parse(coordNumberStr.Substring(coordNumberStr.LastIndexOf(' ')));
                var coordsStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');
                coords[coordNumber] = new Vertex(coordNumber,
                    double.Parse(coordsStr[0], CultureInfo.InvariantCulture),
                    double.Parse(coordsStr[1], CultureInfo.InvariantCulture),
                    double.Parse(coordsStr[2], CultureInfo.InvariantCulture),
                    (int) double.Parse(coordsStr[4], CultureInfo.InvariantCulture),
                    (int) double.Parse(coordsStr[3], CultureInfo.InvariantCulture));
            }

            modelFile.ReadLine();
            
            var rectSizeStr = modelFile.ReadLine();
            int rectSize = int.Parse(rectSizeStr
                .Substring(rectSizeStr
                .LastIndexOf('=') + 1));
            var rectangles = new Dictionary<int, Rectangle>(rectSize);
            for (int i = 0; i < rectSize; i++)
            {
                string rectNumberStr = modelFile.ReadLine();
                int rectNumber = int.Parse(rectNumberStr.Substring(rectNumberStr.LastIndexOf(' ')));
                var rectStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                rectangles[rectNumber] = new Rectangle(
                    rectNumber,
                    new Vertex[] {
                        coords[int.Parse(rectStr[0])],
                        coords[int.Parse(rectStr[1])],
                        coords[int.Parse(rectStr[2])],
                        coords[int.Parse(rectStr[3])]
                    },
                    int.Parse(rectStr[4]),
                    int.Parse(rectStr[5]),
                    int.Parse(rectStr[6])
                    );
            }

            modelFile.ReadLine();

            var triangleSizeStr = modelFile.ReadLine();
            int trianglesSize = int.Parse(triangleSizeStr
                .Substring(triangleSizeStr
                .LastIndexOf('=') + 1));
            var triangles = new Dictionary<int, Triangle>(trianglesSize);
            for (int i = 0; i < trianglesSize; i++)
            {
                string triangleNumberStr = modelFile.ReadLine();
                int triangleNumber = int.Parse(triangleNumberStr.Substring(triangleNumberStr.LastIndexOf(' ')));
                var triangleStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                triangles[triangleNumber] = (new Triangle(
                    triangleNumber,
                    new Vertex[] {
                        coords[int.Parse(triangleStr[0])],
                        coords[int.Parse(triangleStr[1])],
                        coords[int.Parse(triangleStr[2])],
                    },
                    int.Parse(triangleStr[3]),
                    int.Parse(triangleStr[4]),
                    int.Parse(triangleStr[5])
                    )
                    );
            }

            modelFile.ReadLine();

            var rodSizeStr = modelFile.ReadLine();
            int rodSize = int.Parse(rodSizeStr
                .Substring(rodSizeStr
                .LastIndexOf('=') + 1));
            var rods = new Dictionary<int, Rod>(rodSize);
            for (int i = 0; i < rodSize; i++)
            {
                string rodNumberStr = modelFile.ReadLine();
                int rodNumber = int.Parse(rodNumberStr.Substring(rodNumberStr.LastIndexOf(' ')));
                var rodStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                rods[rodNumber] = (new Rod(
                    rodNumber,
                    new Vertex[] {
                        coords[int.Parse(rodStr[0])],
                        coords[int.Parse(rodStr[1])],
                    },
                    coords[int.Parse(rodStr[2])],
                    int.Parse(rodStr[3])
                    )
                    );
            }

            //read LKR array
            modelFile.ReadLine();
            var LKRSizeStr = modelFile.ReadLine();
            int LKRSize = int.Parse(LKRSizeStr
                .Substring(LKRSizeStr
                .LastIndexOf('=') + 1));
            var LKRVertexNumbers = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => int.Parse(x));
            foreach (var LKRVertex in LKRVertexNumbers)
                coords[LKRVertex].IsAnchor = true;

            //read LSM array
            modelFile.ReadLine();
            var LSMSizeStr = modelFile.ReadLine();
            int LSMSize = int.Parse(LSMSizeStr
                .Substring(LSMSizeStr
                .LastIndexOf('=') + 1));
            var LSMVertexNumbers = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => int.Parse(x)).ToArray();

            //read MSM array
            modelFile.ReadLine();
            var MSMSizeStr = modelFile.ReadLine();
            int MSMSize = int.Parse(MSMSizeStr
                .Substring(MSMSizeStr
                .LastIndexOf('=') + 1));
            var MSMVertexValues = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();
            for (int i = 0; i < LSMVertexNumbers.Length; i++)
                coords[LSMVertexNumbers[i]] = new MassVertex(coords[LSMVertexNumbers[i]], MSMVertexValues[i]);

            //read NUK array
            modelFile.ReadLine();
            var NUKSizeStr = modelFile.ReadLine();
            int NUKSize = int.Parse(NUKSizeStr
                .Substring(NUKSizeStr
                .LastIndexOf('=') + 1));
            var NUKVertexNumbers = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => int.Parse(x));
            foreach (var NUKVertex in NUKVertexNumbers)
                coords[NUKVertex].IsControl = true;

            return new Model(coords, triangles, rectangles, rods);
        }

    }
}
