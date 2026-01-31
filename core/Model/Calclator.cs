using System;

namespace CoreLogic
{
    // 計算機の演算タイプ
    public enum Operation
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide
    }

    // これが「生き残るModel」
    // Viewの都合（ボタンとかテキストボックス）は一切知りません。
    public class CalculatorModel
    {
        // 内部状態（State）
        private decimal _currentResult;
        private decimal _buffer; // 入力中の数値など
        private Operation _pendingOperation;
        private bool _isNewEntry; // 演算子を押した直後かどうか

        // 外部に見せるプロパティ（画面表示用だが、特定のUIには依存しない）
        public decimal DisplayValue { get; private set; }

        public CalculatorModel()
        {
            Clear();
        }

        public void Clear()
        {
            _currentResult = 0;
            _buffer = 0;
            _pendingOperation = Operation.None;
            _isNewEntry = true;
            DisplayValue = 0;
        }

        // 数字入力（0-9）
        public void EnterDigit(int digit)
        {
            if (digit < 0 || digit > 9) throw new ArgumentException("Digit must be 0-9");

            if (_isNewEntry)
            {
                DisplayValue = digit;
                _isNewEntry = false;
            }
            else
            {
                // 文字列操作ではなく数値として桁上がり処理
                // (簡易実装のためオーバーフロー等は無視)
                DisplayValue = DisplayValue * 10 + digit;
            }
        }

        // 演算子の入力 (+, -, *, /)
        public void SetOperation(Operation op)
        {
            if (_pendingOperation != Operation.None && !_isNewEntry)
            {
                // 連続計算（1 + 2 + ... と押したとき用）
                Calculate();
            }

            _currentResult = DisplayValue;
            _pendingOperation = op;
            _isNewEntry = true;
        }

        // ＝ ボタン（計算実行）
        public void Calculate()
        {
            if (_pendingOperation == Operation.None) return;

            decimal rightOperand = DisplayValue;

            switch (_pendingOperation)
            {
                case Operation.Add:
                    _currentResult += rightOperand;
                    break;
                case Operation.Subtract:
                    _currentResult -= rightOperand;
                    break;
                case Operation.Multiply:
                    _currentResult *= rightOperand;
                    break;
                case Operation.Divide:
                    if (rightOperand != 0)
                        _currentResult /= rightOperand;
                    else
                        _currentResult = 0; // エラー処理は簡易化
                    break;
            }

            DisplayValue = _currentResult;
            _pendingOperation = Operation.None;
            _isNewEntry = true;
        }
    }
}
