using System;

namespace Battleship
{
    public class BattleshipBoard
    {
        #region Members

        private const int BOARD_SIZE = 10;

        private int[,] _board = new int[BOARD_SIZE, BOARD_SIZE];
        private int _totalHitTarget;
        private int _totalHits;

        public bool IsSetupComplete { get; private set; }
        public int[,] Board { get { return _board; } }

        #endregion

        #region Constructor

        public BattleshipBoard()
        {
            NewGame();
        }

        #endregion

        #region Public Methods

        public void NewGame()
        {
            _board = new int[BOARD_SIZE, BOARD_SIZE];
            _totalHitTarget = 0;
            _totalHits = 0;
            IsSetupComplete = false;
        }

        public void AddShips()
        {
            AddShip(ShipTypes.Battleship);
            // Add other ship types here

            CompleteSetup();
        }

        private void AddShip(ShipTypes shipTypes)
        {
            switch (shipTypes)
            {
                case ShipTypes.Battleship:
                    AddShipToBoard(Constants.SHIP_BATTLESHIP_SIZE);
                    break;
                // Add other ship types here
            }
        }
        
        public bool Attack(char colInput, int rowInput)
        {
            bool hit = false;
            int col = colInput - 'A';
            int row = rowInput - 1;

            if (row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE)
            {
                if (_board[row, col] > Constants.MISS_MARK)
                {
                    hit = true;
                    _totalHits++;
                }

                _board[row, col] = hit? Constants.HIT_MARK : Constants.MISS_MARK;
            }

            return hit;
        }

        public bool AllShipsSunk()
        {
            return _totalHits > 0 &&
                _totalHitTarget > 0 &&
                _totalHits == _totalHitTarget;
        }

        #endregion

        #region Private Methods

        private void AddShipToBoard(int shipSize)
        {
            bool horizontal = Utils.IsHorizontalPosition(); 

            int colMax = horizontal ? BOARD_SIZE - shipSize : BOARD_SIZE;
            int rowMax = horizontal ? BOARD_SIZE : BOARD_SIZE - shipSize;

            int rowPos = Utils.GetRandomNum(0, rowMax);
            int colPos = Utils.GetRandomNum(0, colMax);

            if (horizontal)
            {
                for (int i = colPos; i < colPos + shipSize; i++)
                {
                    _board[rowPos, i] = Constants.SHIP_MARK;
                }
            }
            else
            {
                for (int i = rowPos; i < rowPos + shipSize; i++)
                {
                    _board[i, colPos] = Constants.SHIP_MARK;
                }
            }
        }

        private void CompleteSetup()
        {
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == Constants.SHIP_MARK)
                    {
                        _totalHitTarget++;
                    }
                }
            }

            IsSetupComplete = true;
        }

        #endregion
    }
}