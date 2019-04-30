﻿using MeshCAD.Elements;
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
            List<Vertex> coords = new List<Vertex>(coordSize);
            for (int i = 0; i < coordSize; i++)
            {
                string coordNumberStr = modelFile.ReadLine();
                int coordNumber = int.Parse(coordNumberStr.Substring(coordNumberStr.LastIndexOf(' ')));
                var coordsStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');
                coords.Add(new Vertex(coordNumber,
                    double.Parse(coordsStr[0], CultureInfo.InvariantCulture),
                    double.Parse(coordsStr[1], CultureInfo.InvariantCulture),
                    double.Parse(coordsStr[2], CultureInfo.InvariantCulture),
                    (int) double.Parse(coordsStr[2], CultureInfo.InvariantCulture),
                    (int) double.Parse(coordsStr[2], CultureInfo.InvariantCulture)));
            }

            modelFile.ReadLine();
            
            var rectSizeStr = modelFile.ReadLine();
            int rectSize = int.Parse(rectSizeStr
                .Substring(rectSizeStr
                .LastIndexOf('=') + 1));
            List<Rectangle> rectangles = new List<Rectangle>(rectSize);
            for (int i = 0; i < rectSize; i++)
            {
                string rectNumberStr = modelFile.ReadLine();
                int rectNumber = int.Parse(rectNumberStr.Substring(rectNumberStr.LastIndexOf(' ')));
                var rectStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                rectangles.Add(new Rectangle(
                    rectNumber,
                    new Vertex[] {
                        coords[int.Parse(rectStr[0])-1],
                        coords[int.Parse(rectStr[1])-1],
                        coords[int.Parse(rectStr[2])-1],
                        coords[int.Parse(rectStr[3])-1]
                    },
                    int.Parse(rectStr[4]),
                    int.Parse(rectStr[5]),
                    int.Parse(rectStr[6])
                    )
                    );
            }

            modelFile.ReadLine();

            var triangleSizeStr = modelFile.ReadLine();
            int trianglesSize = int.Parse(triangleSizeStr
                .Substring(triangleSizeStr
                .LastIndexOf('=') + 1));
            List<Triangle> triangles = new List<Triangle>(trianglesSize);
            for (int i = 0; i < trianglesSize; i++)
            {
                string triangleNumberStr = modelFile.ReadLine();
                int triangleNumber = int.Parse(triangleNumberStr.Substring(triangleNumberStr.LastIndexOf(' ')));
                var triangleStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                triangles.Add(new Triangle(
                    triangleNumber,
                    new Vertex[] {
                        coords[int.Parse(triangleStr[0])-1],
                        coords[int.Parse(triangleStr[1])-1],
                        coords[int.Parse(triangleStr[2])-1],
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
            List<Rod> rods = new List<Rod>(rodSize);
            for (int i = 0; i < rodSize; i++)
            {
                string rodNumberStr = modelFile.ReadLine();
                int rodNumber = int.Parse(rodNumberStr.Substring(rodNumberStr.LastIndexOf(' ')));
                var rodStr = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ');

                rods.Add(new Rod(
                    rodNumber,
                    new Vertex[] {
                        coords[int.Parse(rodStr[0])-1],
                        coords[int.Parse(rodStr[1])-1],
                    },
                    coords[int.Parse(rodStr[2]) - 1],
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

            //read LSM array
            modelFile.ReadLine();
            var LSMSizeStr = modelFile.ReadLine();
            int LSMSize = int.Parse(LSMSizeStr
                .Substring(LSMSizeStr
                .LastIndexOf('=') + 1));
            var LSMVertexNumbers = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => int.Parse(x));

            //read MSM array
            modelFile.ReadLine();
            var MSMSizeStr = modelFile.ReadLine();
            int MSMSize = int.Parse(MSMSizeStr
                .Substring(MSMSizeStr
                .LastIndexOf('=') + 1));
            var MSMVertexValues = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => double.Parse(x, CultureInfo.InvariantCulture));

            //read NUK array
            modelFile.ReadLine();
            var NUKSizeStr = modelFile.ReadLine();
            int NUKSize = int.Parse(NUKSizeStr
                .Substring(NUKSizeStr
                .LastIndexOf('=') + 1));
            var NUKVertexNumbers = Regex.Replace(modelFile.ReadLine().Trim(), @"\s+", " ").Split(' ').Select(x => int.Parse(x));
            return new Model(coords, triangles, rectangles, rods);
        }

    }
}
