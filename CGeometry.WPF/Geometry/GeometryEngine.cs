using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CGeometry.WPF.Graphics
{
    public class GeometryEngine
    {
        public static int Id { get; set; } = 0;
        public GCanvas GCanvas { get; set; }

        public Dictionary<string, List<GShape>> Shapes { get; set; }
        public GeometryEngine()
        {
            Shapes = new Dictionary<string, List<GShape>>()
            {
                {"Points",new List<GShape>()},
               {"Lines",new List<GShape>()},
            };

            GCanvas = new GCanvas();
        }

        public void Render(string listName)
        {
            var gShapes = Shapes[listName];
            //
            Render(gShapes);
        }
        public void Render(List<GShape> gShapes)
        {
            gShapes.ForEach(e => e.Render());
        }
        public void RenderAll()
        {
            foreach (var shapeList in Shapes)
            {
                shapeList.Value.ForEach(e=>e.Render());
            }
        }
        public void RemoveAll()
        {
           
            foreach (var shapeList in Shapes)
            {
                shapeList.Value.ForEach(e => e.Remove());
                shapeList.Value.Clear();
            }
           

        }
        public void Remove(string listName)
        {
            //get shapes list based on string key and remove them from th Gcanvas
            var shapes = Shapes[listName];
            shapes.ForEach(e => e.Remove());
            //clear Dictionary List
            Shapes[listName].Clear();
        }
     
        //Shapes.ForEach(e => e.Remove());
        public void HideAll()
        {
            foreach (var shapeList in Shapes)
            {
                shapeList.Value.ForEach(e => e.Hide());
            }
        }
        public void Hide(string listName)
        {
            Hide(Shapes[listName]);
        }
        public void Hide(List<GShape> gShapes)
        {
            gShapes.ForEach(e => e.Hide());
        }

        #region Helper Functions

        #endregion
    }
}
