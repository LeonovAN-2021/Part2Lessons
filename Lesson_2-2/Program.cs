using System;

namespace GeekBrainsTests
{
    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }
    //Требуется реализовать класс двусвязного списка и операции вставки, удаления и поиска элемента в нём в соответствии с интерфейсом.

    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList
    {
        int GetCount(); // возвращает количество элементов в списке
        void AddNode(int value);  // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
        
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        
        Node FindNode(int searchValue); // ищет элемент по его значению
        
    }

    public class NodeList: ILinkedList
    {
        public Node StartNode { get; set; }
        public Node FinishNode { get; set; }

        public void RemoveNode(int index)
        {
            if ((index >= GetCount())&&(index<0)) { Console.WriteLine("Несуществующий индекс."); return; }
            int i = 0;
            Node tmp = StartNode;
            while (i != index) {tmp = tmp.NextNode; i++; }
            
            RemoveNode(tmp);
        }

        public void RemoveNode(Node node)
        {
            if (node == null) { Console.WriteLine("Элемент не найден."); return; }
            if (node == StartNode) 
            {
                StartNode = StartNode.NextNode;
                StartNode.PrevNode = null;
                return;
            }
            if (node==FinishNode)
            {
                FinishNode = FinishNode.PrevNode;
                FinishNode.NextNode = null;
                return;
            }

            node.PrevNode.NextNode = node.NextNode;
            node.NextNode.PrevNode = node.PrevNode;
        }

        public void Display()
        {
            Console.WriteLine();
            Node tmp = StartNode;
            do { Console.Write(tmp.Value + "\t"); tmp = tmp.NextNode; } while (tmp.NextNode != null);
            Console.WriteLine(tmp.Value);
        }

        public void AddNodeAfter(Node node, int value)
        {
            if (node == null) { Console.WriteLine("Элемент не найден."); return; }
            if ((node.PrevNode==null)&&(node.NextNode==null)) { Console.WriteLine("Элемент не связан со списком."); return; }
            if (node==FinishNode) { AddNode(value); return; }
            Node NewNode = new Node();
            NewNode.PrevNode = node;
            NewNode.NextNode = node.NextNode;
            node.NextNode = NewNode;
            NewNode.NextNode.PrevNode = NewNode;
            NewNode.Value = value;
        }
        public Node FindNode(int searchValue)
        {
            Node CurrentNode = StartNode;
            while ((CurrentNode!=null)&&(CurrentNode.Value != searchValue))
            {
                CurrentNode = CurrentNode.NextNode;
            }    
            return CurrentNode;
            
        }
        public int GetCount()
        {
            int i = 1;
            Node tmp = StartNode;
            do
            {
                i++;
                tmp = tmp.NextNode;
            } while (tmp.NextNode != null);
            return i;
        }

        public void AddNode(int value)
        {
            if (StartNode==null)
            {
                StartNode = new Node();
                StartNode.Value = value;
                return;
            }
            if (FinishNode==null)
            {
                FinishNode = new Node();
                FinishNode.Value = value;
                FinishNode.PrevNode = StartNode;
                StartNode.NextNode = FinishNode;
                return;
            }
            Node NewNode = FinishNode;
            FinishNode = new Node();
            FinishNode.Value = value;
            FinishNode.PrevNode = NewNode;
            NewNode.NextNode = FinishNode;  
        }

        public NodeList(int start, int finish)
        {
            AddNode(start);
            AddNode(finish);
        }
    }

    class TestCase
    {
        public int Argument { get; set; }
        public int[] Array { get; set; }
        public int Expected { get; set; }
        public Exception ExpException { get; set; }
    }

    

    class Program
    {
        public static void TestBinSearch(TestCase test)
        {
            try
            {
                int result = BinSearch(test.Argument, test.Array);
                string expect = (test.Expected == result) ? "VALID RESULT" : "INVALID RESULT";
                Console.Write(test.Argument + ": " + result + " " + expect);
            } catch (Exception ex)
                {
                if ((test.ExpException != null) && (test.ExpException.GetType() == ex.GetType()))
                {
                    Console.Write("VALID EXCEPTION");
                }
                else { Console.Write("INVALID EXCEPTION"); }
            }
            Console.WriteLine();
        }

        public static int BinSearch(int what, int[] where )
        {
            int min = 0;
            int max = where.Length - 1;

            while (min<=max)
            {
                int center = (min + max) / 2;
                if (where[center]==what) { return center; } else if (where[center]>what) { max = center - 1; } else { min = center+1; }
            }                                                                                                                               //O(log2N)
            return -1;
            
        }
        
        static void Main(string[] args)
        {
            int[] testarray1 = { -5, -4, -2, -1, 0, 4, 5, 7, 9, 12, 13, 18, 19 };
            int[] testarray2 = new int[0];
            int[] testarray3 = { 1000, 5000, 2147483645 };
            
            TestCase[] test = new TestCase[10];
            test[0] = new TestCase(); test[0].Argument = -2; test[0].Expected = 2; test[0].Array = testarray1;
            test[1] = new TestCase(); test[1].Argument = 0; test[1].Expected = 4; test[1].Array = testarray1;
            test[2] = new TestCase(); test[2].Argument = 13; test[2].Expected = 10; test[2].Array = testarray1;
            test[3] = new TestCase(); test[3].Argument = -5; test[3].Expected = 0; test[3].Array = testarray1;
            test[4] = new TestCase(); test[4].Argument = 19; test[4].Expected = 12; test[4].Array = testarray1;
            test[5] = new TestCase(); test[5].Argument = 6; test[5].Expected = -1; test[5].Array = testarray1;
            test[6] = new TestCase(); test[6].Argument = -2; test[6].Expected = 2; test[6].Array = testarray1;
            test[7] = new TestCase(); test[7].Argument = 5000; test[7].Expected = 1; test[7].Array = testarray3; test[7].ExpException = new OverflowException();
            test[8] = new TestCase(); test[8].Argument = 10; test[8].Expected = - 1; test[8].Array = testarray2; test[8].ExpException = new IndexOutOfRangeException();
            test[9] = new TestCase(); test[9].ExpException = new NullReferenceException();
            foreach (var item in test)
            {
                TestBinSearch(item);
            }
            
            NodeList NL = new NodeList(1, 2);
            
            foreach (var item in testarray1)
            {
                NL.AddNode(item);
            }            
            NL.AddNodeAfter(NL.FindNode(5), 10);
            
            Console.WriteLine("Всего элементов: "+NL.GetCount());

            NL.Display();
            NL.RemoveNode(3);
            NL.Display();
            
            NL.RemoveNode(NL.FindNode(5));
            NL.Display();
            NL.RemoveNode(6);
            NL.Display();
           
        }
    }

}
