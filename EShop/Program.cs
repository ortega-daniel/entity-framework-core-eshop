using EShop;

EshopConsole console = new();
bool showMainMenu = true;

while (showMainMenu)
{
    showMainMenu = console.MainMenu();
}