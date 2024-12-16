using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // Коллекция товаров
        private ObservableCollection<Product> Products { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            
            Products = new ObservableCollection<Product>
            {
                new Product { ProductName = "Ноутбук ASUS", Description = "Игровой ноутбук с RTX 3060", Manufacturer = "ASUS", Price = 129999, Stock = 15, ImagePath = "Images/laptop.png" },
                new Product { ProductName = "Смартфон Samsung", Description = "Флагманский смартфон S23 Ultra", Manufacturer = "Samsung", Price = 99999, Stock = 7, ImagePath = "Images/phone.png" },
                new Product { ProductName = "Телевизор LG", Description = "Крутой телевизор 4k", Manufacturer = "LG", Price = 99999, Stock = 2, ImagePath = "Images/tv.png" },
                new Product { ProductName = "Наушники Sony", Description = "Беспроводные наушники с шумоподавлением", Manufacturer = "Sony", Price = 7999, Stock = 30, ImagePath = "Images/headphones.png" },
                new Product { ProductName = "Мышь Logitech", Description = "Игровая мышь с подсветкой", Manufacturer = "Logitech", Price = 2499, Stock = 50, ImagePath = "Images/mouse.png" },
                new Product { ProductName = "Клавиатура Corsair", Description = "Механическая клавиатура с RGB подсветкой", Manufacturer = "Corsair", Price = 7999, Stock = 20, ImagePath = "Images/keyboard.png" },
                new Product { ProductName = "Планшет Apple", Description = "Планшет iPad Pro 12.9", Manufacturer = "Apple", Price = 84999, Stock = 12, ImagePath = "Images/ipad.png" }

            };

            
            ProductList.ItemsSource = Products;

            
            UpdateProductCount();
        }

        
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            
            var newProduct = new Product
            {
                ProductName = "Новый товар",
                Description = "Описание нового товара",
                Manufacturer = "Производитель",
                Price = 1000,
                Stock = 10,
                ImagePath = "Images/default.png"
            };

            
            Products.Add(newProduct);

            
            ProductList.ItemsSource = Products;

            
            UpdateProductCount();
        }

        
        private void UpdateProductCount()
        {
            ProductCount.Text = $"Показано товаров: {Products.Count}";
        }

        
        private void SortOptions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = SortOptions.SelectedItem as System.Windows.Controls.ComboBoxItem;

            if (selectedItem != null && selectedItem.Content != null)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "По цене":
                        Products = new ObservableCollection<Product>(Products.OrderBy(p => p.Price));
                        break;
                    case "По имени":
                        Products = new ObservableCollection<Product>(Products.OrderBy(p => p.ProductName));
                        break;
                    case "По наличию":
                        Products = new ObservableCollection<Product>(Products.OrderBy(p => p.Stock));
                        break;
                    default:
                        break;
                }

                
                ProductList.ItemsSource = Products;
                UpdateProductCount();
            }
        }

        
        private void ManufacturerFilter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedManufacturer = ManufacturerFilter.SelectedItem as System.Windows.Controls.ComboBoxItem;

            if (selectedManufacturer != null && selectedManufacturer.Content != null)
            {
                
                if (selectedManufacturer.Content.ToString() == "Все производители")
                {
                    ProductList.ItemsSource = Products; 
                }
                else
                {
                    
                    var filteredProducts = Products.Where(p => p.Manufacturer == selectedManufacturer.Content.ToString()).ToList();
                    ProductList.ItemsSource = new ObservableCollection<Product>(filteredProducts);
                }

                UpdateProductCount();
            }
        }

       
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    
    public class Product
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
    }
}
