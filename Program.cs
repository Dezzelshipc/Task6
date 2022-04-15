namespace Task6
{
    interface List<T>
    {
        void add(T element);
        void put(T element, int position);
        void remove(int position);
        int find(T element);
        T? get(int index);
        void print();
    }

    public class ArrayList<T> : List<T>
    {
        private T[] arr;
        private int capacity;
        private int size;

        public ArrayList(int n = 8)
        {
            arr = new T[n];
            capacity = n;
            size = 0;
        }

        public void add(T element)
        {
            if (size + 1 == capacity)
            {
                T[] new_arr = new T[capacity * 2];

                for (int i = 0; i < size; ++i)
                {
                    new_arr[i] = arr[i];
                }

                GC.Collect(GC.GetGeneration(arr));
                arr = new_arr;
            }
            arr[size++] = element;
        }

        public void put(T element, int position)
        {
            if (position < 0 || position > size)
                return;

            if (size + 1 == capacity)
            {
                T[] new_arr = new T[capacity * 2];

                for (int i = 0; i < position; ++i)
                {
                    new_arr[i] = arr[i];
                }

                for (int i = position; i < size; ++i)
                {
                    new_arr[i + 1] = arr[i];
                }

                GC.Collect(GC.GetGeneration(arr));
                arr = new_arr;
                capacity *= 2;
            }
            else
            {
                for (int i = size - 1; i >= position; --i)
                {
                    arr[i + 1] = arr[i];
                }
            }
            arr[position] = element;
            size++;
        }

        public int find(T element)
        {
            for (int i = 0; i < size; ++i)
            {
                if (Equals(arr[i], element))
                {
                    return i;
                }
            }
            return -1;
        }

        public T? get(int index)
        {
            if (index < 0 || index >= size)
                return default(T);
            return arr[index];
        }

        public void remove(int position)
        {
            if (position < 0 || position >= size)
                return;

            if ((capacity / 2 - 1 < size) && size > 8)
            {
                T[] new_arr = new T[capacity / 2];

                for (int i = 0; i < position; ++i)
                {
                    new_arr[i] = arr[i];
                }

                for (int i = position; i < size; ++i)
                {
                    new_arr[i] = arr[i + 1];
                }

                GC.Collect(GC.GetGeneration(arr));
                arr = new_arr;
                capacity /= 2;
            }
            else
            {
                for (int i = position; i < size; ++i)
                {
                    arr[i] = arr[i + 1];
                }
            }
            size--;
        }

        public void print()
        {
            for (int i = 0; i < size; ++i)
            {
                Console.WriteLine(i + " " + get(i));
            }
        }
    }

    public class LinkedList<T> : List<T>
    {
        private class Node
        {
            public T? element;
            public Node? next;
            public Node? prev;
        }

        private Node node;
        private int size;

        public LinkedList()
        {
            size = 0;

            node = new Node
            {
                element = default(T),
                prev = null
            };

            Node last = new Node
            {
                element = default(T),
                prev = node,
                next = null
            };

            node.next = last;
        }

        public void add(T element)
        {
            int n = ++size;
            Node prev_node = node;

            while (--n > 0)
            {
                prev_node = prev_node.next;
            }
            Node next_node = prev_node.next;

            Node new_node = new Node
            {
                element = element,
                prev = prev_node,
                next = next_node,
            };

            prev_node.next = new_node;
            next_node.prev = new_node;

        }

        public void put(T element, int position)
        {
            if (position < 0 || position > size)
                return;

            ++size;
            int n = position;
            Node prev_node = node;

            while (n-- > 0)
            {
                prev_node = prev_node.next;
            }
            Node next_node = prev_node.next;

            Node new_node = new Node
            {
                element = element,
                prev = prev_node,
                next = next_node,
            };

            prev_node.next = new_node;
            next_node.prev = new_node;
        }

        public void remove(int position)
        {
            if (position < 0 || position > size)
                return;

            int n = position;
            Node prev_node = node;

            while (n-- > 0)
            {
                prev_node = prev_node.next;
            }
            Node next_node = prev_node.next.next;

            GC.Collect(GC.GetGeneration(prev_node.next));

            prev_node.next = next_node;
            next_node.prev = prev_node;

            --size;
        }

        public int find(T element)
        {
            Node i_node = node.next;
            for (int i = 0; i <= size; ++i)
            {
                if (Equals(i_node.element, element))
                {
                    return i;
                }
                i_node = i_node.next;
            }
            return -1;
        }

        public T? get(int index)
        {
            if (index < 0 || index > size)
                return default(T);

            Node i_node = node.next;
            int n = index;

            while (n-- > 0)
            {
                i_node = i_node.next;
            }

            return i_node.element;
        }

        public void print()
        {
            for (int i = 0; i < size; ++i)
            {
                Console.WriteLine(i + " " + get(i));
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //List<int> list = new ArrayList<int>();
            
            List<int> list = new LinkedList<int>();

            list.add(10);
            list.add(20);
            list.print();
            Console.WriteLine(list.find(10));
            Console.WriteLine(list.find(20));
            list.remove(0);
            Console.WriteLine(list.find(10));
            list.put(30, 0);
            list.print();
        }

    }
}