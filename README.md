# Inventory and Sales Manager

This Windows Forms application is designed to manage product inventory and sales orders, with a reporting feature to display both product and sales data. Developed as a learning exercise, the project includes functionalities to add, view, and manage product and order data, and it provides basic report generation through Crystal Reports.

## Features

### 1. Start Menu Navigation
- **File**
  - *Exit*: Close the application.
- **Manage**
  - *Products*: Opens the product management form.
  - *New Order*: Opens the order management form.
- **Reports**
  - *Product Report*: View product details.
  - *Sales Report*: View sales details.

### 2. Product Management (Add Products)
- Add new products with fields for product ID, name, and price.
- Display added products in a grid view.
- Clear fields using the **Clear** button.
- Delete products by entering the Product ID.

### 3. Order Management (New Order)
- Create orders with fields for order ID, date, customer details, product selection, quantity, and price (auto-displayed upon product selection).
- Orders display in a grid view.
- Clear fields using the **Clear** button.
- Delete orders by entering the Order ID.

### 4. Reports (Crystal Reports)
- **Product Report**: Displays all added products.
- **Sales Report**: Shows order details.
- Load data for each report through dedicated buttons.

## Getting Started

### Prerequisites
- [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework)
- [Visual Studio](https://visualstudio.microsoft.com/) (with Windows Forms workload installed)
- [Crystal Reports for Visual Studio](https://www.sap.com/community/topic/crystal-reports.html) (optional, if you want to customize reports)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/supunhg/Inventory-and-Sales-Manager.git
   ```
2. Open the project in Visual Studio.
3. Build the project to restore dependencies.

### Usage
1. Run the application in Visual Studio.
2. Navigate through the **Start Menu** to manage products, create new orders, and view reports.
3. Use the **Product Management** and **Order Management** forms to add, view, and delete records.
4. Access the **Reports** form to load product or sales reports.

## Screenshots
*(Include relevant screenshots of each form and reports here)*

## Technologies Used
- **C#** - Windows Forms
- **SQLite** - Local database management
- **Crystal Reports** - Report generation and visualization

## License
Distributed under the MIT License. See `LICENSE` for more information.
