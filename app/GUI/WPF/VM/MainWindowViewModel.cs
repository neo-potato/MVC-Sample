using System.ComponentModel;
using System.Windows.Input;
using Core; // Modelを参照

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly CalculatorModel _model;

    // WPFのBinding用プロパティ
    public string DisplayText => _model.DisplayValue.ToString();

    public event PropertyChangedEventHandler PropertyChanged;

    public WpfViewModel(CalculatorModel model)
    {
        _model = model;
    }

    // 画面更新通知ヘルパー
    private void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // コマンド (ボタンクリック時の処理)
    // ※本来はRelayCommandなどを実装しますが、ここでは概念として
    public void InputDigit(int digit)
    {
        _model.EnterDigit(digit);
        RaisePropertyChanged(nameof(DisplayText)); // 画面さん、値変わったよ！
    }

    public void InputOperation(Operation op)
    {
        _model.SetOperation(op);
        // 演算子押しただけでは表示変わらないかもしれないけど、念のため
        RaisePropertyChanged(nameof(DisplayText)); 
    }

    public void ExecuteCalculate()
    {
        _model.Calculate();
        RaisePropertyChanged(nameof(DisplayText)); // 結果を表示
    }

    public void ExecuteClear()
    {
        _model.Clear();
        RaisePropertyChanged(nameof(DisplayText));
    }
}
