using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Node
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public Node parent { get; set; }

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Node(Node other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public Node()
        {
            this.x = 0;
            this.y = 0;
        }

        public override string ToString()
        {
            return "( " + x + " , " + y + ")";
        }
    }
}
