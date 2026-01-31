public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 1. Model生成 (CLIと同じもの！)
        var model = new CalculatorModel();

        // 2. VM生成 (WPF専用)
        var vm = new WpfViewModel(model);

        // 3. View生成
        var window = new MainWindow();
        window.DataContext = vm; // ここでBinding成立

        window.Show();
    }
}
