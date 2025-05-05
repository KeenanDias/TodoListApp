# TodoList App

## Overview

This is a simple TodoList application built using C# and WPF with Entity Framework Core for data persistence. It allows users to manage a list of todo items, adding and removing them. The application has a sticky note-like appearance for a user-friendly interface.

## Features

* **Add Items:** Users can add new todo items to the list by entering text in the text box and clicking the "Add" button.
* **Remove Items:** Users can remove items from the list by selecting an item and clicking the "Remove" button.
* **Persistent Data:** The application uses Entity Framework Core to store todo items in a SQL Server LocalDB database, ensuring that data is persisted between sessions.
* **Sticky Note UI:** The application features a custom-styled window with a yellow background, dark yellow border, and a draggable title bar, resembling a sticky note.
* **Resizable Window:** The window can be resized using a resize grip in the bottom-right corner.

## Technologies Used

* C#
* WPF (.NET Framework)
* Entity Framework Core
* SQL Server LocalDB

## Prerequisites

* Visual Studio (with WPF development tools)
* .NET Framework
* SQL Server LocalDB (usually installed with Visual Studio)

## Getting Started

1.  **Clone the Repository:** (If you have the code in a git repository)
    ```bash
    git clone <repository_url>
    ```

2.  **Open the Solution:** Open the `TodoListApp.sln` file in Visual Studio.

3.  **Set up the Database:**
    * Ensure that SQL Server LocalDB is running.
    * The application uses Entity Framework Core Code First to create and manage the database. The connection string is in the `todoContext.cs` file.
    * If the database does not exist,  open the Package Manager Console in Visual Studio and run the following commands:
        ```powershell
        PM> Add-Migration InitialCreate
        PM> Update-Database
        ```
        If you make changes to the `todoItems` class (e.g., adding or modifying properties), you'll need to create new migrations and update the database accordingly:
        ```powershell
        PM> Add-Migration <MigrationName>
        PM> Update-Database
        ```

4.  **Build and Run:** Press `Ctrl+Shift+B` to build the solution, and then press `F5` to run the application.

## Code Structure

* `MainWindow.xaml`: Defines the user interface (WPF window) with a list box, text box, and buttons.
* `MainWindow.xaml.cs`: Contains the code-behind for the main window, handling user interactions and database operations.
* `todoItems.cs`: Defines the `todoItems` entity class, representing a todo item in the database.
* `todoContext.cs`: Defines the `todoContext` class, which is the Entity Framework Core DbContext for interacting with the database.

## Notes

* The application uses a custom window style to achieve the sticky note appearance.
* The `DisplayMemberPath` property of the `ListBox` is set to "description" to display the todo item descriptions.
* Error handling is included in the `LoadTodoItems` method to display a message box if there are issues fetching data from the database.
* The `TitleBar_MouseDown` event handler in the `MainWindow.xaml.cs` file allows the user to drag the window.
