using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Globalization.NumberStyles;

namespace MeshCAD
{
    class DatParser
    {

        public void Parse(StreamReader modelFile)
        {
            modelFile.ReadLine();
            string verticesCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string boardCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string plateEvenStepCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string plateFixedStepCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string rodCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string triangleCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string rectangleCount = modelFile.ReadLine();
            modelFile.ReadLine();
            string etazherkaCount = modelFile.ReadLine();

            var model = new Model(int.Parse(verticesCount),
                int.Parse(boardCount),
                int.Parse(plateEvenStepCount),
                int.Parse(plateFixedStepCount),
                int.Parse(rodCount),
                int.Parse(triangleCount),
                int.Parse(rectangleCount),
                int.Parse(etazherkaCount));

            // Parse "Kolitshestvo dopolnitelnyh uzlov orientacii stergnei:"
            if (model.RodCount > 0)
            {
                modelFile.ReadLine();
                string additionalRodVerticesCount = modelFile.ReadLine();
                model.AdditionalRodVerticesCount = int.Parse(additionalRodVerticesCount);
            }

            //Parse "Kolitshestvo tipov mater. plat i plastin:"
            if (model.BoardCount 
                + model.PlateEvenStepCount
                + model.PlateFixedStepCount
                + model.TriangleCount
                + model.RectangleCount
                + model.EtazherkaCount > 0)
            {
                modelFile.ReadLine();
                string materialsCount = modelFile.ReadLine();
                model.MaterialsCount = int.Parse(materialsCount);

                model = ParseMaterials(modelFile, model);
            }

            // Parse "Kolishestvo fiksir. linii setki po osiam X,Y,Z:"
            if (model.PlateFixedStepCount 
                + model.EtazherkaCount > 0)
            {
                //not finished
                modelFile.ReadLine();
                model.FixedLinesCountX = int.Parse(modelFile.ReadLine());
                model.FixedLinesCountY = int.Parse(modelFile.ReadLine());
                model.FixedLinesCountZ = int.Parse(modelFile.ReadLine());

                model = ParseFixedLinesCoords(modelFile, model);
                if (model.EtazherkaCount > 0)
                {
                    //todo add parse etazherki additional info      
                }

                model = ParsePlatesFixedStep(modelFile, model);

            }

            if (model.BoardCount 
                + model.PlateEvenStepCount > 0)
            {
                model = ParsePlatesEvenStepOrBoards(modelFile, model);
            }

            model.BasicVertices = ParseVertices(modelFile, model.VerticesCount);
            model.Rectangles = ParseRectangles(modelFile, model.RectangleCount);

        }

        private List<Vertice> ParseVertices(StreamReader modelFile, int verticesCount)
        {
            List<Vertice> vertices = new List<Vertice>();
            if (verticesCount == 0)
                return vertices;
            //Koordinaty uzlov modeli bez plat i plastin X:   Y:   Z:  
            modelFile.ReadLine();
            for (int i = 0; i < verticesCount; i++)
            {
                modelFile.ReadLine();
                Vertice vertice = new Vertice(
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture));

                vertices.Add(vertice);
            }
            return vertices;
        }

        private List<Rectangle> ParseRectangles(StreamReader modelFile, int rectangleCount)
        {
            List<Rectangle> rectangles= new List<Rectangle>();
            if (rectangleCount == 0)
                return rectangles;
            //Nomera uzlov vershin priamoug.,tip materiala:
            modelFile.ReadLine();
            for (int i = 0; i < rectangleCount; i++)
            {
                modelFile.ReadLine();
                Rectangle rectangle = new Rectangle(
                    int.Parse(modelFile.ReadLine()),
                    int.Parse(modelFile.ReadLine()),
                    int.Parse(modelFile.ReadLine()),
                    int.Parse(modelFile.ReadLine()),
                    int.Parse(modelFile.ReadLine()));

                rectangles.Add(rectangle);
            }
            return rectangles;
        }

        private Model ParsePlatesEvenStepOrBoards(StreamReader modelFile, Model model)
        {
            List<PlateEvenStepOrBoard> platesEvenStepOrBoards = new List<PlateEvenStepOrBoard>();
            for (int i = 0; i < model.BoardCount; i++)
            {
                PlateEvenStepOrBoard plateEvenStepOrBoard = new PlateEvenStepOrBoard();
                //Plata (plastina s ravnom.shagom):
                modelFile.ReadLine();
                //Nomer nashalnogo uzla plati:
                modelFile.ReadLine();
                plateEvenStepOrBoard.FirstVerticeNumber = int.Parse(modelFile.ReadLine());
                //Nomer nashalnogo pramougolnika plati:
                modelFile.ReadLine();
                plateEvenStepOrBoard.FirstRectangleNumber = int.Parse(modelFile.ReadLine());

                //Koordinati bazovyh uzlov plati
                modelFile.ReadLine();
                plateEvenStepOrBoard.BaseVertices = new List<Vertice>();
                //Uzel:1
                modelFile.ReadLine();
                string[] coordinatesLine = modelFile.ReadLine().Trim().Split(' ');
                plateEvenStepOrBoard
                    .BaseVertices
                    .Add(new Vertice(
                        double.Parse(coordinatesLine[0], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[1], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[2], CultureInfo.InvariantCulture)
                        ));
                //Uzel:2
                modelFile.ReadLine();
                coordinatesLine = modelFile.ReadLine().Trim().Split(' ');
                plateEvenStepOrBoard
                    .BaseVertices
                    .Add(new Vertice(
                        double.Parse(coordinatesLine[0], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[1], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[2], CultureInfo.InvariantCulture)
                        ));
                //Uzel:3
                modelFile.ReadLine();
                coordinatesLine = modelFile.ReadLine().Trim().Split(' ');
                plateEvenStepOrBoard
                    .BaseVertices
                    .Add(new Vertice(
                        double.Parse(coordinatesLine[0], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[1], CultureInfo.InvariantCulture),
                        double.Parse(coordinatesLine[2], CultureInfo.InvariantCulture)
                        ));

                //Kolish linii plati ot 1 k 3-mu baz. uzlu:
                modelFile.ReadLine();
                plateEvenStepOrBoard.Lines1To3VerticeCount = int.Parse(modelFile.ReadLine());
                //Kolish linii plati ot 1 k 2-mu baz.uzlu
                modelFile.ReadLine();
                plateEvenStepOrBoard.Lines1To2VerticeCount = int.Parse(modelFile.ReadLine());

                //Nomer tipa materiala  plati:
                modelFile.ReadLine();
                plateEvenStepOrBoard.MaterialTypeNumber = int.Parse(modelFile.ReadLine());

                //Kolish obobshennih elem na plate:
                modelFile.ReadLine();
                plateEvenStepOrBoard.GeneralizedElementsCount = int.Parse(modelFile.ReadLine());

                plateEvenStepOrBoard = ParseGeneralizedElements(modelFile, plateEvenStepOrBoard);

                platesEvenStepOrBoards.Add(plateEvenStepOrBoard);
            }

            model.PlatesEvenStepOrBoards = platesEvenStepOrBoards;

            return model;
        }

        private PlateEvenStepOrBoard ParseGeneralizedElements(StreamReader modelFile, PlateEvenStepOrBoard plateEvenStepOrBoard)
        {
            List<GeneralizedElement> generalizedElements = new List<GeneralizedElement>();
            for (int i = 0; i < plateEvenStepOrBoard.GeneralizedElementsCount; i++)
            {
                GeneralizedElement generalizedElement = new GeneralizedElement();
                //Obobshen elem:
                modelFile.ReadLine();

                //Nomer levoi vert linii:
                modelFile.ReadLine();
                generalizedElement.LeftVerticalLineNumber = int.Parse(modelFile.ReadLine());

                //Nomer pravoi vert linii(bolshaia):
                modelFile.ReadLine();
                generalizedElement.RightVerticalLineNumber = int.Parse(modelFile.ReadLine());

                //Nomer nijnei goriz linii:
                modelFile.ReadLine();
                generalizedElement.BottomHorizontalLineNumber = int.Parse(modelFile.ReadLine());

                //Nomer verhnei goriz linii:
                modelFile.ReadLine();
                generalizedElement.TopHorizontalLineNumber = int.Parse(modelFile.ReadLine());

                //Massa obobsh elem [kg](dla virezov=0:
                modelFile.ReadLine();
                generalizedElement.Mass = double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture);

                //Koeffic zapolnenia:
                modelFile.ReadLine();
                generalizedElement.CoeffZapoln = double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture);

                generalizedElements.Add(generalizedElement);
            }

            plateEvenStepOrBoard.GeneralizedElements = generalizedElements;

            return plateEvenStepOrBoard;
        }

        private Model ParsePlatesFixedStep(StreamReader modelFile, Model model)
        {
            List<PlateFixedStep> platesFixedStep = new List<PlateFixedStep>();
            for (int i = 0; i < model.PlateFixedStepCount; i++)
            {
                modelFile.ReadLine();

                var plate = new PlateFixedStep();
                modelFile.ReadLine();
                plate.FirstVerticeNumber = int.Parse(modelFile.ReadLine());
                modelFile.ReadLine();
                plate.FirstPlateRectangleNumber = int.Parse(modelFile.ReadLine());

                if (model.EtazherkaCount > 0)
                {
                    //todo
                }

                modelFile.ReadLine();
                plate.StartLineXNumber = int.Parse(modelFile.ReadLine());
                plate.EndLineXNumber = int.Parse(modelFile.ReadLine());

                modelFile.ReadLine();
                plate.StartLineYNumber = int.Parse(modelFile.ReadLine());
                plate.EndLineYNumber = int.Parse(modelFile.ReadLine());

                modelFile.ReadLine();
                plate.StartLineZNumber = int.Parse(modelFile.ReadLine());
                plate.EndLineZNumber = int.Parse(modelFile.ReadLine());

                modelFile.ReadLine();
                plate.MaterialTypeNumber = int.Parse(modelFile.ReadLine());

                platesFixedStep.Add(plate);
            }

            model.PlatesFixedStep = platesFixedStep;

            return model;
        }

        private Model ParseMaterials(StreamReader modelFile, Model model)
        {
            List<Material> materials = new List<Material>(); 
            for (int i = 0; i < model.MaterialsCount; i++)
            {
                modelFile.ReadLine();
                var material = new Material(double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture),
                    double.Parse(modelFile.ReadLine(), CultureInfo.InvariantCulture));
                materials.Add(material);
                
            }

            model.Materials = materials;
            return model;
        }

        private Model ParseFixedLinesCoords(StreamReader modelFile, Model model)
        {
            modelFile.ReadLine();

            List<double> fixedLinesX = new List<double>();
            for (int i = 0; i < model.FixedLinesCountX; i++)
            {
                modelFile.ReadLine();
                fixedLinesX.Add(double.Parse(modelFile.ReadLine(),
                    CultureInfo.InvariantCulture));
            }
            model.FixedLinesX = fixedLinesX;

            List<double> fixedLinesY = new List<double>();
            for (int i = 0; i < model.FixedLinesCountY; i++)
            {
                modelFile.ReadLine();
                fixedLinesY.Add(double.Parse(modelFile.ReadLine(),
                    CultureInfo.InvariantCulture));
            }
            model.FixedLinesY = fixedLinesY;

            List<double> fixedLinesZ = new List<double>();
            for (int i = 0; i < model.FixedLinesCountZ; i++)
            {
                modelFile.ReadLine();
                fixedLinesZ.Add(double.Parse(modelFile.ReadLine(),
                    CultureInfo.InvariantCulture));
            }
            model.FixedLinesZ = fixedLinesZ;

            return model;
        }
    }
}
