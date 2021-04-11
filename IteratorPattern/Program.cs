using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            PanCakeHouseMenu panCakeHouseMenu = new PanCakeHouseMenu();
            DinerMenu dinerMenu = new DinerMenu();

            Iterator panCakeHouseIterator = panCakeHouseMenu.CreateIterator();
            Iterator dinerMenuIterator = dinerMenu.CreateIterator();

            WriteMenu(panCakeHouseIterator);
            Console.WriteLine();
            WriteMenu(dinerMenuIterator);

            Console.ReadKey();
        }

        private static void WriteMenu(Iterator iterator)
        {
            while(iterator.HasNext())
            {
                MenuPosition menuPosition = (MenuPosition)iterator.Next();
                if(menuPosition != null)
                {
                    Console.WriteLine($"{menuPosition.Name}, {menuPosition.Description}, {menuPosition.IsVege}, {menuPosition.Cost}");
                }                
            }            
        }

        public class MenuPosition
        {
            public string Name { get; }
            public string Description { get; }
            public bool IsVege { get; }
            public double Cost { get; }

            public MenuPosition(string name, string description, bool isVege, double cost)
            {
                this.Name = name;
                this.Description = description;
                this.IsVege = isVege;
                this.Cost = cost;
            }
        }

        public interface Iterator
        {
            bool HasNext();
            object Next();
        }

        public class PanCakeHouseMenuIterator : Iterator
        {
            List<MenuPosition> MenuPositions;
            int Position = 0;

            public PanCakeHouseMenuIterator(List<MenuPosition> menuPositions)
            {
                this.MenuPositions= menuPositions;
            }

            public object Next()
            {
                MenuPosition menuPosition = MenuPositions.ElementAt(Position);
                Position++;
                return menuPosition;
            }

            public bool HasNext()
            {
                if (Position >= MenuPositions.Count || MenuPositions[Position] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public class DinnerMenuIterator : Iterator
        {
            MenuPosition[] MenuPositions;
            int Position = 0;

            public DinnerMenuIterator(MenuPosition[] menuPositions)
            {
                this.MenuPositions = menuPositions;
            }

            public object Next()
            {
                MenuPosition menuPosition = MenuPositions[Position];
                Position++;
                return menuPosition;
            }

            public bool HasNext()
            {
                if(Position >= MenuPositions.Length || MenuPositions[Position] == null)
                {
                    return false;                
                }
                else
                {
                    return true;
                }
            }
        }

        public class PanCakeHouseMenu
        {
            public List<MenuPosition> MenuPositions { get; }

            public PanCakeHouseMenu()
            {
                MenuPositions = new List<MenuPosition>();

                AddMenuPosition("Pancake with eggs", "Pancake with scrumbled eggs and toast", true, 2.99);
                AddMenuPosition("Normal pancake", "Pancake with sausages", false, 5.99);
            }

            public void AddMenuPosition(string name, string description, bool isVege, double cost)
            {
                MenuPositions.Add(new MenuPosition(name, description, isVege, cost));
            }            

            public Iterator CreateIterator()
            {
                return new PanCakeHouseMenuIterator(MenuPositions);
            }
        }

        public class DinerMenu
        {
            const int MAX_ITEMS = 6;
            int items = 0;
            public MenuPosition[] MenuPositions { get; }

            public DinerMenu()
            {
                MenuPositions = new MenuPosition[MAX_ITEMS];
                AddMenuPosition("Vege Sandwitch", "Sandwitch with cucumber, tomato and cheese", true, 1.99);
                AddMenuPosition("Normal Sandwitch", "Sandwitch with ham and tomato", true, 2.49);
            }

            public void AddMenuPosition(string name, string description, bool isVege, double cost)
            {
                if(items >= MAX_ITEMS)
                {
                    Console.WriteLine("Menu is full");
                }
                else
                {
                    MenuPositions[items] = new MenuPosition(name, description, isVege, cost);
                    items++;
                }                
            }

            public Iterator CreateIterator()
            {
                return new DinnerMenuIterator(MenuPositions);
            }
        }
    }
}
