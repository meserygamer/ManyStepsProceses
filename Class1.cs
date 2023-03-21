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
            FormGraph();
            if(FinalNode() is not null)
            {
                FinalNode().SostavMarsh();
                MakeMarsh(FinalNode().VivodPredkov());
            }
            else
            {
                Console.WriteLine("Граф не обладает конечной точкой");
            }
        }
        private void FormGraph() //Создаёт граф
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
        public TreeNode? FinalNode() //Для нахождения финального элемента
        {
            for(int i = 0; i < MatrixPaths.GetLength(0); i++)
            {
                bool k = false;
                for(int j = 0; j < MatrixPaths.GetLength(1); j++)
                {
                    if (MatrixPaths[i,j] != 0) k = true;
                }
                if (k == false) return TreeNodes[i];
            }
            return null;
        }
        public TreeNode? FirstNode() //Возвращает первый элемент
        {
            for (int j = 0; j < MatrixPaths.GetLength(1); j++)
            {
                bool k = false;
                for (int i = 0; i < MatrixPaths.GetLength(0); i++)
                {
                    if (MatrixPaths[i, j] != 0) k = true;
                }
                if (k == false) return TreeNodes[j];
            }
            return null;
        }
        public void sformMarsh(TreeNode Node, List<int> marsh) //Заполняет список marsh оптимальным маршрутом
        {
            marsh.Add(Node.ID_Node);
            if (Node.OptimalMarsh is null) return;
            sformMarsh(Node.OptimalMarsh, marsh);
        }
        private void MakeMarsh(TreeNode[] NextNode)
        {
            if (NextNode.Length != 0)
            {
                List<TreeNode> NextNodes = new List<TreeNode>();
                foreach (TreeNode Node in NextNode)
                {
                    Node.SostavMarsh();
                    foreach (var i in Node.VivodPredkov())
                    {
                        if (!NextNodes.Contains(i)) NextNodes.Add(i);
                    }
                }
                MakeMarsh(NextNodes.ToArray());
            }
            else return;
        }
    }
    internal class TreeNode
    {
        struct Relation //Структура для описания отношений между узлами
        {
            public TreeNode Node {get;private set;} //Узел для создания связи
            public int NodePath {get;private set;} //Путь до узла
            int? WNextNodePath = null; //Путь до следующей вершины учитывая последующие маршруты
            public Relation(TreeNode NextNode, int NextNodePath)
            {
                this.Node = NextNode;
                this.NodePath = NextNodePath;
            }
            public void WNextPath() //Ищет путь до следующей вершины учитывая последующие маршруты
            {
                if (Node.OptimalPath is null)
                {
                    WNextNodePath = NodePath;
                }
                else
                {
                    WNextNodePath = NodePath + Node.OptimalPath;
                }
            }
        }
        public int ID_Node {get; protected set;} //Номер вершины
        List<Relation> Parents = new List<Relation>(); //Отцовские элементы
        List<Relation> Nasledniki = new List<Relation>(); //наследники
        public int? OptimalPath = null;
        public TreeNode? OptimalMarsh = null;
        public TreeNode(int ID_Node)
        {
            this.ID_Node = ID_Node;
        }
        public void SostavMarsh() //Формирует маршруты
        {
            for(int i = 0; i < Nasledniki.Count; i++) //Ищет путь до следующей вершины учитывая последующие маршруты для каждого наследника
            {
                Nasledniki[i].WNextPath();
            }
            for (int i = 0; i < Nasledniki.Count; i++) //Ищет самый оптимальный дальнейший маршрут
            {
                if(OptimalPath is null)
                {
                    OptimalPath = Nasledniki[i].NodePath;
                    OptimalMarsh = Nasledniki[i].Node;
                }
                if(OptimalPath is not null && Nasledniki[i].NodePath < OptimalPath)
                {
                    OptimalPath = Nasledniki[i].NodePath;
                    OptimalMarsh = Nasledniki[i].Node;
                }
            }
        }
        public TreeNode[] VivodPredkov()
        {
            TreeNode[] R = new TreeNode[Parents.Count];
            for(int i = 0; i < Parents.Count; i++)
            {
                R[i] = Parents[i].Node;
            }
            return R;
        }
        public void FormRelation(TreeNode[] Graph, int[,] MatrixPath) //Функция формирует связи узла с другими узлами из вектора Graph, используя матрицу путей MatrixPath
        {
            for(int j = 0; j < MatrixPath.GetLength(1); j++) if(MatrixPath[(ID_Node-1), j] > 0)
            {
                Nasledniki.Add(new Relation(Graph[j], MatrixPath[(ID_Node - 1), j]));
                Graph[j].Parents.Add(new Relation(this, MatrixPath[(ID_Node - 1), j]));
            }
        }
    }
}
