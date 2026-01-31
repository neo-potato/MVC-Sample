public class CalculatorApp
{
    public void Run(string[] args)
    {
        // 1. 共通の資産（Model）を生成
        // GUIでもCLIでも、この「コアロジック」は1つ！
        var model = new CalculatorModel();

        // 2. 起動モードの分岐
        if (args.Length > 0 && args[0] == "--gui")
        {
            // GUIモード (例えばWPFやWindows Forms)
            // var vm = new WpfViewModel(model);
            // var view = new MainWindow(vm);
            // view.ShowDialog();
            Console.WriteLine("GUI mode is under construction!");
        }
        else
        {
            // CLIモード (デフォルト)
            
            // ViewModelにModelを注入
            var vm = new ConsoleViewModel(model);
            
            // ViewにViewModelを注入
            var view = new ConsoleView(vm);

            // 起動！
            view.Start();
        }
    }
}