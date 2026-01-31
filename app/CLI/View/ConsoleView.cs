public class ConsoleView
{
    private readonly ConsoleViewModel _viewModel;

    // ViewはViewModelを知っている（が、Modelは知らない）
    public ConsoleView(ConsoleViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public void Start()
    {
        Console.WriteLine("=== MVVM Calculator (View Loop) ===");

        while (true)
        {
            Render();
            
            Console.Write("> ");
            var key = Console.ReadKey(intercept: true).KeyChar;
            Console.WriteLine();

            if (key == 'q') return; // アプリ終了

            _viewModel.HandleInput(key);
        }
    }

    private void Render()
    {
        // 画面クリアして再描画など、表示周りの汚い処理は全部ここに隠す
        Console.WriteLine($"-----------------------");
        Console.WriteLine($" Display: {_viewModel.DisplayMessage}");
        Console.WriteLine($"-----------------------");
    }
}
