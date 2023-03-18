using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyStepsProceses
{
    internal class TreeNode
    {
        struct Naslednik //Структура для описания наследников узла
        {
            TreeNode NextNode; //Следующая вершина
            int NextNodePath; //Путь до следующей вершины
            int? WNextNodePath = null; //Путь до следующей вершины учитывая последующие маршруты
            public Naslednik(TreeNode NextNode, int NextNodePath)
            {
                this.NextNode = NextNode;
                this.NextNodePath = NextNodePath;
            }
        }
        public int ID_Node { get; set; } //Номер вершины
        List<TreeNode> Parents = new List<TreeNode>(); //Отцовские элементы
        List<Naslednik> Nasledniki = new List<Naslednik>(); //наследники
        Naslednik? U; //Оптимальный наследник
    }
}
