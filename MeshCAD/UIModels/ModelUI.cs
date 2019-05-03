using MeshCAD.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeshCAD.UIModels
{
    public class ModelUI
    {
        public Dictionary<int, VertexUI> verticesUI;// = new Dictionary<int, VertexUI>();
        public Dictionary<int, RectangleUI> rectanglesUI = new Dictionary<int, RectangleUI>();
        public Dictionary<int, RodUI> rodsUI;// = new Dictionary<int, RodUI>();
        public Dictionary<int, TriangleUI> trianglesUI = new Dictionary<int, TriangleUI>();
        public Dictionary<int, List<BelongingUIElement>> boardsUI = new Dictionary<int, List<BelongingUIElement>>();
        public Dictionary<int, List<BelongingUIElement>> platesUI = new Dictionary<int, List<BelongingUIElement>>();
        public List<BelongingUIElement> lonelyElementsUI = new List<BelongingUIElement>();
        public List<Exception> modelExceptions = new List<Exception>();

        public ModelUI(Model model, MouseButtonEventHandler mouseButtonEventHandler)
        {
            verticesUI = model.Vertices.ToDictionary(x => x.Key, y =>
            {
                var vertexUi = new VertexUI(y.Value);
                vertexUi.MouseDown += mouseButtonEventHandler;
                return vertexUi;
            });
            rodsUI = model.Rods.ToDictionary(x => x.Key, y => 
            {
                var rodUI = new RodUI(y.Value);
                rodUI.MouseDown += mouseButtonEventHandler;
                return rodUI;
            });
            foreach (var rectangle in model.Rectangles)
            {
                try
                {
                    var rectangleUI = new RectangleUI(rectangle.Value);
                    rectangleUI.MouseDown += mouseButtonEventHandler;
                    rectanglesUI[rectangle.Key] = rectangleUI;
                    GroupToDicts(rectangleUI);
                }
                catch (Exception e)
                {
                    modelExceptions.Add(e);
                }
            }
            foreach (var triangle in model.Triangles)
            {
                var triangleUi = new TriangleUI(triangle.Value);
                triangleUi.MouseDown += mouseButtonEventHandler;
                trianglesUI[triangle.Key] = triangleUi;
                GroupToDicts(triangleUi);
            }

        }

        private void GroupToDicts(BelongingUIElement source)
        {
            BelongingElement sourceModelElement = (BelongingElement) source.ModelElement;
            if (sourceModelElement.IsLonely)
            {
                lonelyElementsUI.Add(source);
            }
            if (sourceModelElement.BoardNumber != 0)
            {
                if (!boardsUI.ContainsKey(sourceModelElement.BoardNumber))
                    boardsUI[sourceModelElement.BoardNumber] = new List<BelongingUIElement>();
                boardsUI[sourceModelElement.BoardNumber].Add(source);
            }
            if (sourceModelElement.FixPlateNumber != 0)
            {
                if (!platesUI.ContainsKey(sourceModelElement.FixPlateNumber))
                    platesUI[sourceModelElement.FixPlateNumber] = new List<BelongingUIElement>();
                platesUI[sourceModelElement.FixPlateNumber].Add(source);
            }
        }
    }
}
