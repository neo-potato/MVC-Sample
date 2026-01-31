using CoreLogic; // Modelを参照

public class ConsoleViewModel
{
    private readonly CalculatorModel _model;

    // View(Console)に見せるためのプロパティ
    // Modelの生の値をそのまま出すか、整形するかはここで決める
    public string DisplayMessage => _model.DisplayValue.ToString();

    public ConsoleViewModel(CalculatorModel model)
    {
        _model = model;
    }

    // ユーザーのアクション（キー入力）を処理するコマンド的なメソッド
    public void HandleInput(char input)
    {
        // 数字の場合
        if (char.IsDigit(input))
        {
            int digit = int.Parse(input.ToString());
            _model.EnterDigit(digit);
            return;
        }

        // コマンドの場合
        switch (input)
        {
            case '+': _model.SetOperation(Operation.Add); break;
            case '-': _model.SetOperation(Operation.Subtract); break;
            case '*': _model.SetOperation(Operation.Multiply); break;
            case '/': _model.SetOperation(Operation.Divide); break;
            case '=': 
            case '\r': // Enterキー対応
                _model.Calculate(); 
                break;
            case 'c': 
                _model.Clear(); 
                break;
            // 未定義のキーは何もしない
        }
    }
}