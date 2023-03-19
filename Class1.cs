using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyStepsProceses
{
    internal class TreeGraph
    {
        TreeNode[] TreeNodes; //Узлы графа
        int[,] MatrixPaths; //Матрица путей
        public TreeGraph(int[,] MatrixPaths) //Получает матрицу на вход и формирует по ней узлы
        {
            this.MatrixPaths = MatrixPaths;
            TreeNodes = new TreeNode[MatrixPaths.GetLength(0)];
        }
        private void FormGraph() //Создаёт узлы графа
        {
            for(int i = 0; i < MatrixPaths.GetLength(0); i++)
            {
                TreeNodes[i] = new TreeNode(i+1);
            }
            for (int i = 0; i < MatrixPaths.GetLength(0); i++)
            {
                TreeNodes[i].FormRelation(TreeNodes, MatrixPaths);
            }
        }
    }
    internal class TreeNode
    {
        struct Relation //Структура для описания отношений между узлами
        {
            TreeNode Node; //Узел для создания связи
            int NodePath; //Путь до узла
            //int? WNextNodePath = null; //Путь до следующей вершины учитывая последующие маршруты
            public Relation(TreeNode NextNode, int NextNodePath)
            {
                this.Node = NextNode;
                this.NodePath = NextNodePath;
            }
        }
        public int ID_Node {get; protected set;} //Номер вершины
        List<Relation> Parents = new List<Relation>(); //Отцовские элементы
        List<Relation> Nasledniki = new List<Relation>(); //наследники
        public TreeNode(int ID_Node)
        {
            this.ID_Node = ID_Node;
        }
        public void FormRelation(TreeNode[] Graph, int[,] MatrixPath) //Функция формирует связи узла с другими узлами из вектора Graph, используя матрицу путей MatrixPath
        {
            for(int j = 0; j < MatrixPath.GetLength(1); j++) if(MatrixPath[(ID_Node-1), j] > 0)
            {
                Nasledniki.Add(new Relation(Graph[j], MatrixPath[(ID_Node - 1), j]));
                Graph[j].Parents.Add(new Relation(this, MatrixPath[(ID_Node - 1), j]));
            }
        }
        //Naslednik? U; //Оптимальный наследник
    }
}
